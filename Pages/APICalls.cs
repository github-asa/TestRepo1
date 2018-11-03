using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Helpers;
using J2BIOverseasOps.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using OpenQA.Selenium;
using static J2BIOverseasOps.Models.AvailableAssigneesApi;
using static J2BIOverseasOps.Models.BookingsApi;
using static J2BIOverseasOps.Models.CaseAutoAssigneesApi;
using static J2BIOverseasOps.Models.CaseOverviewApi;
using static J2BIOverseasOps.Models.CaseSubjectsApi;
using static J2BIOverseasOps.Models.PropertiesApi;
using static J2BIOverseasOps.Models.UserMgmtApi;

namespace J2BIOverseasOps.Pages
{
    public class ApiCalls
    {
        private static IRunData _runData;
        private readonly Roles _role = new Roles();
        private readonly RoleToPermissionMap _roleToPermission = new RoleToPermissionMap();
        private readonly RoleToUserMap _roleToUser = new RoleToUserMap();

        public ApiCalls(IRunData runData)
        {
            _runData = runData;
        }

        /// <summary>
        /// Gets list of destinationsToAssign from the properties API
        /// </summary>
        /// <returns></returns>
        public List<Destination> GetListOfDestinations()
        {
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/destination";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "destination?userid=3",
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The destinationsToAssign call was unsuccessful");
            var dest = JsonConvert.DeserializeObject<List<Destination>>(content);
            return dest;
        }


        /// <summary>
        /// Gets Destination Id for the given destination name
        /// </summary>
        /// <returns></returns>
        public int GetDestinationId(string destinationCode)
        {
            var listOfDestinations = GetListOfDestinations();
            var destinationId = 0;
            foreach (var destination in listOfDestinations)
            {
                // if destination iata code matches
                if (string.Equals(destination.iataCode, destinationCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    destinationId = destination.id;
                    break;
                }
            }
            return destinationId;
        }

        /// <summary>
        /// Gets Destination IataCode for the given destination name
        /// </summary>
        /// <returns></returns>
        public List<string> GetDestinationIATACode(List<int> destinationId)
        {
            var listOfDestinations = GetListOfDestinations();
            var destinationsIataCode = new List<string>();
            foreach (var i in destinationId)
            {
                foreach (var destination in listOfDestinations)
                {
                    if (destination.id == i)
                    {
                        destinationsIataCode.Add(destination.iataCode);
                    }
                }

            }
            return destinationsIataCode;
        }

        /// <summary>
        /// Gets Property Id for the given destination name
        /// </summary>
        /// <returns></returns>
        public int GetPropertyId(int destinationId, string propertyName)
        {
            var listOfProperties = GetPropertiesByDestination(destinationId);
            var propertyId = 0;
            foreach (var property in listOfProperties)
            {
                // if property name
                if (String.Equals(property.Name, propertyName, StringComparison.CurrentCultureIgnoreCase))
                {
                    propertyId = property.Id;
                    break;
                }
            }

            return propertyId;
        }


        /// <summary>
        /// Gets list of resorts from the properties API for the given destination
        /// </summary>
        /// <returns></returns>
        public List<Resort> GetResortsByDestination(int destinationId)
        {
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/destination/{destinationId}/resort";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "resort?userid=3",
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The resorts call was unsuccessful");
            var reso = JsonConvert.DeserializeObject<List<Resort>>(content);
            return reso;
        }

        /// <summary>
        /// Gets list of Properties from the properties API for the given destination
        /// </summary>
        /// <returns></returns>
        public List<Property> GetPropertiesByDestination(int destinationId)
        {
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/destination/{destinationId}/property";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "property?userid=3",
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Property>>(content);
        }


        /// <summary>
        /// Gets list of All the Categories
        /// </summary>
        /// <returns></returns>
        public List<CategoryApi.Category> GetListOfAllCategories()
        {
            var endPoint = $"{_runData.BaseApiUrl}/category/allcategories";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "allcategories?userid=3",
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute<CategoryApi.Category>(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The categories call was unsuccessful");
            return response.Data;
        }

        /// <summary>
        /// Gets an overview of the case from the CaseOverview API for a given guid
        /// </summary>
        /// <returns></returns>
        public GetCaseOverviewResponse GetCaseOverviewByGuid(string guid, string token)
        {
            var endPoint = $"{_runData.BaseApiUrl}/cases/getcaseoverview/{guid}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = $"{guid}?userid=3",
                RequestFormat = DataFormat.Json

            };

            //Add the angular cleint authorization token to the header
            request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The caseoverview call was unsuccessful");
            return JsonConvert.DeserializeObject<GetCaseOverviewResponse>(content);
        }

        /// <summary>
        /// Gets list Of Bookings from given resorts within the date range
        /// </summary>
        /// <returns></returns>
        public BookingsRoot GetListOfBookings(int userId, DateTime dateFrom, DateTime dateTo, int propertyId,
            string customerName = "", string reference = "")
        {

            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var bookingsUri = $"{ConfigurationManager.AppSettings["get_bookings"]}";
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            var uri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{bookingsUri}");
            var endPoint = $"getbyproperties?userid={userId}&customerName={customerName}&reference={reference}&inResortFrom={dateFrom:yyyy-MM-dd}&inResortTo={dateTo:yyyy-MM-dd}&propertyIds={propertyId}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute<List<BookingsRoot>>(request, uri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Get Bookings call was unsuccessful");
            var content = response.Content.FixApiResponseString();
            var bookings = JsonConvert.DeserializeObject<BookingsRoot>(content);
            return bookings;
        }

        /// <summary>
        /// Gets the auto assignees of the case from the CaseAutoAssignees API for a given guid
        /// </summary>
        /// <returns></returns>
        public GetUsersAutoAssignedToCaseResponse GetCaseAutoAssigneesByGuid(string guid, string token)
        {
            var endPoint = $"{_runData.BaseApiUrl}/cases/getCaseAutoAssignees/{guid}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = $"{guid}?userid=3",
                RequestFormat = DataFormat.Json,

            };
            request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The caseautoassignees call was unsuccessful");
            return JsonConvert.DeserializeObject<GetUsersAutoAssignedToCaseResponse>(content);
        }

        /// <summary>
        /// Gets available assignees for the case from the AvailableAssignees API
        /// </summary>
        /// <returns></returns>
        public List<CaseAssignee> GetAvailableAssignees(string token)
        {
            var endPoint = $"{_runData.BaseApiUrl}/casesData/getAvailableAssignees";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "getAvailableAssignees?userid=3",
                RequestFormat = DataFormat.Json,

            };
            request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The availableassignees call was unsuccessful");
            return JsonConvert.DeserializeObject<List<CaseAssignee>>(content);
        }


        /// <summary>
        /// Gets the list of restaurants from given destination and property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<Restaurant> GetListOfRestaurants(string destination, string property)
        {
            var destinationId = GetDestinationId(destination);
            var propertyId = GetPropertyId(destinationId, property);
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/{propertyId}/Restaurant";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Restaurant>>(content);
        }

        /// <summary>
        /// Gets the list of pools from given destination and property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<Pool> GetListOfPools(string destination, string property)
        {
            var destinationId = GetDestinationId(destination);
            var propertyId = GetPropertyId(destinationId, property);
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/{propertyId}/pool";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Pool>>(content);
        }

        /// <summary>
        /// Gets the list of bars from given destination and property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<Bar> GetListOfBars(string destination, string property)
        {
            var destinationId = GetDestinationId(destination);
            var propertyId = GetPropertyId(destinationId, property);
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/{propertyId}/bar";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Bar>>(content);

        }

        /// <summary>
        /// Gets the list of other advertised facilities from given destination and property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<OtherAdvertisedFacility> GetListOfOtherFacilities(string destination, string property)
        {
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/otheradvertisedfacilities";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<OtherAdvertisedFacility>>(content);
        }

        public List<Property> GetPropertiesByResort(int resortId)
        {
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/resort/{resortId}/property";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = "property?userid=3",
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Property>>(content);
        }

        /// <summary>
        /// Gets the list of rooms from given destination and property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<Rooms> GetListOfTypesOfRooms(string destination, string property)
        {
            var destinationId = GetDestinationId(destination);
            var propertyId = GetPropertyId(destinationId, property);
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/{propertyId}/Rooms";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Rooms>>(content);
        }


        /// <summary>
        /// Gets the list of Commitments for the given property
        /// </summary>
        /// <param name="destination">destination name</param>
        /// <param name="property"> property name</param>
        /// <returns></returns>
        public List<Commitments> GetListOfCommitmentsForProperty(string destination, string property)
        {
            var destinationId = GetDestinationId(destination);
            var propertyId = GetPropertyId(destinationId, property);
            var endPoint = $"{_runData.BaseApiUrl}/properties/api/property/{propertyId}/Commitments";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json
            };
            var response = RestHelpers.Execute(request, new Uri(_runData.BaseApiUrl));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The property call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Commitments>>(content);
        }

        /// <summary>
        /// Gets the subjects of a case from the CaseSubjects API for a given guid
        /// </summary>
        /// <returns></returns>
        public GetCaseSubjectsResponse GetCaseSubjectsByGuid(string guid, string token)
        {
            var endPoint = $"{_runData.BaseApiUrl}/cases/getcasesubjects/{guid}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RootElement = $"{guid}?userid=3",
                RequestFormat = DataFormat.Json,
            };
            request.AddParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            var content = response.Content.FixApiResponseString();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The casesubjects call was unsuccessful");
            return JsonConvert.DeserializeObject<GetCaseSubjectsResponse>(content);
        }


        /// <summary>
        /// Gets the list of all the users in current environment 
        /// </summary>
        /// <returns></returns>
        public List<Users> GetListOfAllUsers()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var dataApi = ConfigurationManager.AppSettings["data_api"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{dataApi}/Employee/users";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Get list of employees call was unsuccessful");
            return JsonConvert.DeserializeObject<List<Users>>(response.Content);
        }

        /// <summary>
        /// Gets the detail of a user by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Users GetUserDetails(string username) {
            return this.GetListOfAllUsers().FirstOrDefault(x => x.username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the detail of a user by the userId
        /// </summary>
        /// <param name="uid">user id to get the details for</param>
        /// <returns></returns>
        public Users GetUserDetails(int uid)
        {
            return this.GetListOfAllUsers().FirstOrDefault(x => x.userId.Equals(uid));
        }

        /// <summary>
        /// Get the userId of the given user with username
        /// </summary>
        /// <param name="username">username to search the id for</param>
        /// <returns></returns>
        public int GetUserId(string username)
        {
            var listOfUsers = GetListOfAllUsers();
            var userId = 0;
            var userFound = false;
            foreach (var user in listOfUsers)
            {
                if (String.Equals(user.username, username, StringComparison.CurrentCultureIgnoreCase))
                {
                    userId = user.userId;
                    userFound = true;
                    break;
                }
            }
            if (!userFound)
            {
                Assert.Fail($"Could not find userid for the username {username}");
            }
            return userId;
        }


        /// <summary>
        /// Gets users with user full names as input. e.g. Test Automation1 will return a list of users 
        /// </summary>
        /// <param name="userFullNames">List of usernames to get the Id's for</param>
        /// <returns></returns>
        public List<Users> GetUsersWithGivenFullName(List<string> userFullNames)
        {
            // check if user exists
            var allUsers = GetListOfAllUsers();
            var users = new List<Users>();

            foreach (var u in userFullNames)
            {
                foreach (var userdetail in allUsers)
                {
                    var uFullName = $"{userdetail.forename} {userdetail.surname}";
                    if (uFullName == u)
                    {
                        users.Add(userdetail);
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Removes any Roles assigned to a user
        /// </summary>
        /// <param name="userFullNames">Users full name to remove the assigned role</param>
        /// <returns></returns>
        public void RemoveRolesAssignedToUsers(List<string> userFullNames)
        {
            // check if user exists
            var listOfUsers = GetUsersWithGivenFullName(userFullNames);
            foreach (var user in listOfUsers)
            {
                var roleAssignedToUser = GetRoleAssignedToAUsername(user.username);
                var roleName = roleAssignedToUser.Rolename;
                if (roleName!=null)
                {
                    UnmapUserFromRole(GetRoleId(roleName), user.userId);
                }
            }
        }

        /// <summary>
        /// Gets the list of all the users in current environment 
        /// </summary>
        /// <returns></returns>
        public UserPermissionsInfo GetRoleAssignedToAUsername(string loginName)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}/user/user?loginName={loginName}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Get list of employees call was unsuccessful");
            return JsonConvert.DeserializeObject<UserPermissionsInfo>(response.Content);
        }

        /// <summary>
        /// Gets the user info for the user passed
        /// </summary>
        /// <returns></returns>
        public UserPermissionsInfo GetRoleAssignedToUserFullName(string fullname)
        {
            var listOfAllUsers = GetListOfAllUsers();
            var username = "";
            foreach (var user in listOfAllUsers)
            {
                if ($"{user.forename} {user.surname}" == fullname)
                {
                    username = user.username;
                }
            }
            return GetRoleAssignedToAUsername(username);
        }

        /// <summary>
        /// Gets the List of Roles Assigned to a User
        /// </summary>
        /// <returns></returns>
        public List<int> GetRolesAssignedToUser(int userId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}/role/usertorolemap/roles/{userId}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Get list of employees call was unsuccessful");
            return JsonConvert.DeserializeObject<List<int>>(response.Content);
        }

        /// <summary>
        /// Gets the role assigned to the majority of ranks
        /// </summary>
        /// <param name="rank">Rank/JobTitle of the user</param>
        public string GetRoleAssignedToRanks(string rank)
        {
            var e = GetListOfAllUsers();
            var listOfUsersWithGivenRank= new List<Users>();
            foreach (var employee in e)
            {
                if (employee.title==rank)
                {
                    listOfUsersWithGivenRank.Add(employee);
                }
            }

            var listOfRoles= new List<string>();

            foreach (var employee in listOfUsersWithGivenRank)
            {
                listOfRoles.Add(GetRoleAssignedToAUsername(employee.username).Rolename);
            }
            return listOfRoles.GetMostFrequestStringInList();
        }

        /// <summary>
        /// Deletes all the test roles . Uses regex matching [a-z]*[_]*[a-z]*[0-9]{17}
        /// </summary>
        public void DeleteTestRoles()
        {
            var listOfRoles = GetListOfRoles();
            const string regex = "[a-z]*[_]*[a-z]*[0-9]{17}";
            foreach (var role in listOfRoles)
            {
                var regEx = new Regex(regex, RegexOptions.IgnoreCase);
                var m = regEx.Match(role.Name);
                if (m.Success)
                {
                    DeleteARole(role.Id);
                }
            }
        }

        /// <summary>
        /// Deletes all the Forms used for Test data. Matches the RegEx AUTO[0-9]{17}
        /// </summary>
        public void DeleteTestForms()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var sysAdminBaseUri = ConfigurationManager.AppSettings["systemadmin_baseurl"];
            var formMgmtPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            // e.g. AUTO01234567891012345
            const string regex = "AUTO[0-9]{17}";
            var resource = ConfigurationManager.AppSettings["get_all_forms"];

            var getFormsRequest = new RestRequest(Method.GET)
            {
                Resource = resource
            };

            var baseUri = new Uri($"{baseApiUri}{formMgmtPort}{sysAdminBaseUri}");
            Console.WriteLine($"Getting list of forms uri {baseUri}{resource}");
            var _allFormsResponseData = RestHelpers.Execute<GetForms>(getFormsRequest, baseUri);
            Console.WriteLine($"Get list of forms status code : {_allFormsResponseData.ResponseStatus.ToString()}");
            Console.WriteLine($"Forms response content  : {_allFormsResponseData.Content}");
            Console.WriteLine($"Forms response error message  : {_allFormsResponseData.ErrorMessage}");
            if (_allFormsResponseData.StatusCode.Equals(HttpStatusCode.OK))
            {
                Console.WriteLine($"Get list of forms status code was OK");
                foreach (var form in _allFormsResponseData.Data)
                {
                    var formName = form.Name;
                    var regEx = new Regex(regex, RegexOptions.IgnoreCase);
                    var m = regEx.Match(formName);
                    if (m.Success)
                    {
                        var formId = form.Id;
                        var deleteRequest = new RestRequest(Method.DELETE)
                        {
                            Resource = ConfigurationManager.AppSettings["delete_form"].Replace("form_id", formId)
                        };
                        var deleteFormResponse = RestHelpers.Execute(deleteRequest, baseUri);
                        Console.WriteLine($"Delete form status code : {deleteFormResponse.StatusCode}");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of all Roles in current environment from the data feed api
        /// </summary>
        /// <returns></returns>
        public void DeleteARole(int roleId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var sysAdminBaseUri = ConfigurationManager.AppSettings["systemadmin_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var resource = $"/role/{roleId}";
            var sysAdminUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{sysAdminBaseUri}");

            var deleteARole = new RestRequest(Method.DELETE)
            {
                Resource = resource
            };
            var response = RestHelpers.Execute(deleteARole, sysAdminUri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"The Delet a Role API call was unsuccessful.  Status code :{response.StatusCode}");
        }


        /// <summary>
        /// Gets the list of all Roles in current environment from the data feed api
        /// </summary>
        /// <returns></returns>
        public List<Roles> GetListOfRoles()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var resource = ConfigurationManager.AppSettings["get_all_roles"];
            var getRolesRequest = new RestRequest(Method.GET)
            {
                Resource = resource
            };

            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            Console.WriteLine($"Executing list of roles URI {usrMgmtUri}{resource}");
            var allRolesResponseData = RestHelpers.Execute<Roles>(getRolesRequest, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, allRolesResponseData.StatusCode, $"The Get list of Roles API call was unsuccessful.  Status code :{allRolesResponseData.StatusCode}");
            return JsonConvert.DeserializeObject<List<Roles>>(allRolesResponseData.Content);
        }


        /// <summary>
        /// Gets the list of all Roles in current environment from the data feed api
        /// </summary>
        /// <returns></returns>
        public int GetRoleId(string roleName)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var resource = ConfigurationManager.AppSettings["get_all_roles"];
            var getRolesRequest = new RestRequest(Method.GET)
            {
                Resource = resource
            };

            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            Console.WriteLine($"Executing list of roles URI {usrMgmtUri}{resource}");
            var allRolesResponseData = RestHelpers.Execute<Roles>(getRolesRequest, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, allRolesResponseData.StatusCode,
                $"The Get list of Roles API call was unsuccessful.  Status code :{allRolesResponseData.StatusCode}");
            var listOfRoles = JsonConvert.DeserializeObject<List<Roles>>(allRolesResponseData.Content);
            var roleId = 0;
            foreach (var role in listOfRoles)
            {
                if (role.Name == roleName)
                {
                    roleId = role.Id;
                    break;
                }
            }
            return roleId;
        }

        /// <summary>
        /// Gets number of permissions for a role
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        public int GetNumberOfPermissionsforRole(int id)
        {
            var listOfRoles = GetListOfRoles();
            var numbOfPermissions = 0;
            var found = false;
            foreach (var role in listOfRoles)
            {
                if (role.Id == id)
                {
                    found = true;
                    numbOfPermissions= role.NumOfPermissions;
                }
            }

            if (!found)
            {
                Assert.Fail($"Unable to find role with Id:  {id}");
            }

            return numbOfPermissions;
        }

        /// <summary>
        /// Only creates a Role if not exists already
        /// </summary>
        /// <returns></returns>
        public int CreateRoleIfNotCreated(string name, bool isActive = true)
        {
            var listOfRoles = GetListOfRoles();
            foreach (var role in listOfRoles)
            {
                if (role.Name == name)
                {
                    return role.Id;
                }
            }
            {
                var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
                var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
                var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
                var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
                const string resource = "/role";
                _role.Name = name;
                _role.IsActive = isActive;
                var jsonObject = JsonConvert.SerializeObject(_role);
                var request = new RestRequest(Method.POST)
                {
                    Resource = resource
                };
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
                var response = RestHelpers.Execute<Roles>(request, usrMgmtUri);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                    $"The Get list of Roles API call was unsuccessful.  Status code :{response.StatusCode}");

                var responseContent = JsonConvert.DeserializeObject<Roles>(response.Content);
                return responseContent.Id;
            }
        }

        /// <summary>
        /// Gets the list of all the Permissions present within the system
        /// </summary>
        /// <returns></returns>
        public List<Permissions> GetAllPermissions()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            const string resource = "/role/permission/summary";
            var getPermissionsRequest = new RestRequest(Method.GET)
            {
                Resource = resource
            };
            Console.WriteLine($"Executing list of roles URI {usrMgmtUri}{resource}");
            var allPermissions = RestHelpers.Execute<Permissions>(getPermissionsRequest, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, allPermissions.StatusCode, $"The Get list of Roles API call was unsuccessful.  Status code :{allPermissions.StatusCode}");
            return JsonConvert.DeserializeObject<List<Permissions>>(allPermissions.Content);
        }


        /// <summary>
        /// Assigns a permission to a role
        /// </summary>
        /// <returns></returns>
        public void AssignAPermissionToRole(int permissionId, int roleId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            const string resource = "/role/roletopermissionmap/add";
            _roleToPermission.RoleId = roleId;
            _roleToPermission.PermissionId = permissionId;
            var jsonObject = JsonConvert.SerializeObject(_roleToPermission);
            var request = new RestRequest(Method.POST)
            {
                Resource = resource
            };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<RoleToPermissionMap>(request, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                $"The Role to Permissions Map failed.  Status code :{response.StatusCode}");
        }


        /// <summary>
        /// Removes all the Permission from a Role
        /// </summary>
        /// <param name="role">role name</param>
        public void UnmapAllPermissionsFromRole(string role)
        {
            var roleId = GetRoleId(role);
            var listOfPermissions=GetAllPermissionsAssignedToaRole(roleId);

            var allPermissions = GetAllPermissions();
            if (listOfPermissions.Count != 0)
            {
                foreach (var permission in allPermissions)
                {
                    if (listOfPermissions.Contains(permission.Id))
                    {
                        UnmapAPermissionsFromRole(permission.Name, role);
                    }
                }
            }
        }

     
        /// <summary>
        /// Get List of Id's for the permissions that are assigned to a role
        /// </summary>
        /// <param name="roleId">Role Id to get permissions for</param>
        /// <returns></returns>
        public List<int> GetAllPermissionsAssignedToaRole(int roleId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var resource = $"/role/roletopermissionmap/permissions/{roleId}";
            var getPermAssignedToRole = new RestRequest(Method.GET)
            {
                Resource = resource
            };

            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");

            var responseData = RestHelpers.Execute(getPermAssignedToRole, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, responseData.StatusCode, $"Status code :{responseData.StatusCode}");
            return JsonConvert.DeserializeObject<List<int>>(responseData.Content);
        }


        /// <summary>
        /// Removes a Permission from a Role
        /// </summary>
        /// <param name="permission">permission name</param>
        /// <param name="role">role name</param>
        public void UnmapAPermissionsFromRole(string permission, string role)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            const string resource = "/role/roletopermissionmap/remove";
            _roleToPermission.RoleId = GetRoleId(role);
            _roleToPermission.PermissionId = GetPermissionId(permission);
            var jsonObject = JsonConvert.SerializeObject(_roleToPermission);
            var request = new RestRequest(Method.POST)
            {
                Resource = resource
            };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<RoleToPermissionMap>(request, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                $"The Role to Permissions Remove failed.  Status code :{response.StatusCode}");
        }

        /// <summary>
        /// Assigns a permissions to a role.
        /// </summary>
        /// <param name="permission">Permission to assign the role to</param>
        /// <param name="role">Role to assign the permission to</param>
        public void MapAPermissionToRole(string permission, string role)
        {
            var permId = GetPermissionId(permission);
            var roleId = GetRoleId(role);
            AssignAPermissionToRole(permId, roleId);
        }


        /// <summary>
        /// Get the permission Id for the given permission
        /// </summary>
        /// <param name="permission">permission to get the Id for</param>
        /// <returns></returns>
        public int GetPermissionId(string permission)
        {
            var listOfAllPermissions = GetAllPermissions();
            var permissionId = 0;
            var permFound = false;
            foreach (var perm in listOfAllPermissions)
            {
                if (perm.Name == permission)
                {
                    permissionId= perm.Id;
                    permFound = true;
                    break;
                }
            }

            if (!permFound)
            {
                Assert.Fail($"Could not find Id for the permission {permission}");
            }
            return permissionId;
        }

        /// <summary>
        /// Assigns a User to a role
        /// </summary>
        /// <returns></returns>
        public void  MapRoleToUserById(int roleId, int userId)
        {
            AddRoleToOverrideTable(roleId, userId);
//            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
//            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
//            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
//            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
//            const string resource = "/role/usertorolemap/add";
//
//            _roleToUser.RoleId = roleId;
//            _roleToUser.UserId = userId;
//            var jsonObject = JsonConvert.SerializeObject(_roleToUser);
//            var request = new RestRequest(Method.POST)
//            {
//                Resource = resource
//            };
//            request.AddHeader("Accept", "application/json");
//            request.Parameters.Clear();
//            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
//            var response = RestHelpers.Execute<RoleToUserMap>(request, usrMgmtUri);
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
//                $"The Role to User Map failed.  Status code :{response.StatusCode}");
        }

        private void AddRoleToOverrideTable(int roleId,int userId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            const string resource = "role/usertorolemap/override/add";
            _roleToUser.RoleId = roleId;
            _roleToUser.UserId = userId;
            var jsonObject = JsonConvert.SerializeObject(_roleToUser);
            var request = new RestRequest(Method.POST)
            {
                Resource = resource
            };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<RoleToUserMap>(request, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                $"Add To Override Table failed.  Status code :{response.StatusCode}");
        }

        /// <summary>
        /// Assigns a Role To a User with rolename and username
        /// </summary>
        /// <param name="roleName">e.g. all_permissions</param>
        /// <param name="userName">e.g. DgTest\\username</param>
        public void MapRoleToUser(string roleName, string userName)
        {
            var roleId = GetRoleId(roleName);
            var userId = GetUserId(userName);
            var roles= GetRolesAssignedToUser(userId);
            if (!roles.Contains(roleId))
            {
                foreach (var role in roles)
                {
                  //  RemoveRoleFromOverrideTable(role, userId);
                    UnmapUserFromRole(role,userId);
                }

                MapRoleToUserById(roleId, userId);
            }
        }

        /// <summary>
        /// Assigns a User to a role
        /// </summary>
        /// <returns></returns>
        public void MapRoleToUserByFullName(int roleId, string fullName)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            const string resource = "/role/usertorolemap/add";

            var users = new List<string> { fullName };
            var getUserDetails = GetUsersWithGivenFullName(users);
            _roleToUser.RoleId = roleId;
            _roleToUser.UserId = getUserDetails[0].userId;

            var jsonObject = JsonConvert.SerializeObject(_roleToUser);
            var request = new RestRequest(Method.POST)
            {
                Resource = resource
            };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<RoleToUserMap>(request, usrMgmtUri);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                $"The Role to User Map failed.  Status code :{response.StatusCode}");
        }

        /// <summary>
        /// Unmaps a User from a role
        /// </summary>
        /// <param name="roleId">role id to unmap from</param>
        /// <param name="userId">user id to unmap from</param>
        public void UnmapUserFromRole(int roleId, int userId)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var sysAdminBaseUri = ConfigurationManager.AppSettings["systemadmin_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var sysAdminUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{sysAdminBaseUri}");
            var resource = "/usertorolemap/remove";
            _roleToUser.RoleId = roleId;
            _roleToUser.UserId = userId;
            var jsonObject = JsonConvert.SerializeObject(_roleToUser);
            var request = new RestRequest(Method.POST)
            {
                Resource = resource
            };
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<RoleToUserMap>(request, sysAdminUri);

            // if it fails then try it in override table
            if (response.StatusCode!= HttpStatusCode.OK)
            {
                 resource = "/usertorolemap/override/remove";
                 request = new RestRequest(Method.POST)
                {
                    Resource = resource
                };
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
                response = RestHelpers.Execute<RoleToUserMap>(request, sysAdminUri);
            }

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                $"The Remove Role from User Map failed.  Status code :{response.StatusCode}");
        }

        public IRestResponse<List<DataFeedEmployees>> GetListOfEmployeesDataFeed()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var dataFeedApiEndPoint = ConfigurationManager.AppSettings["dataFeedApi_baseurl"];
            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{dataFeedApiEndPoint}/Employees";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute<DataFeedEmployees>(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Get list of employees call was unsuccessful");
            return response;
        }

        /// <summary>
        /// Deserializes the DataFeed Employyes response
        /// </summary>
        /// <param name="dataFeedEmployees">input</param>
        /// <returns></returns>
        public List<Employee> DeserializeEmployeeDataFeed(IRestResponse<List<DataFeedEmployees>> dataFeedEmployees)
        {
            var rawEmployees = JsonConvert.DeserializeObject<DataFeedEmployees>(dataFeedEmployees.Content);
            return rawEmployees.Employees.Employee;
        }

        public void PostListOfEmployeesDataFeed(DataFeedEmployees employees)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var dataFeedApiEndPoint = ConfigurationManager.AppSettings["dataFeedApi_baseurl"];
            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{dataFeedApiEndPoint}/Employees";
            var request = new RestRequest(Method.POST)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };

            var jsonObject = JsonConvert.SerializeObject(employees);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            var response = RestHelpers.Execute<DataFeedEmployees>(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The POST list of employees call was unsuccessful");
        }

        /// <summary>
        /// Updates Roles to Titles mapping, Running this will map all the job titles to roles they are supposed to mapped to
        /// </summary>
        public void UpdateTitlesToRolesMapping()
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];

            var endPoint = $"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}/TitleToRole/updateTitleRoleMapping";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
            };
            var response = RestHelpers.Execute(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Update Title to Role Mapping call was Unsuccessful");
            Thread.Sleep(90000);
        }

        /// <summary>
        /// Assigns list of destinations to a user
        /// </summary>
        /// <param name="destinationsToAssign"> list of destinations to assign to</param>
        /// <param name="username">username to assign to</param>
        /// <param name="allDestinations">bool flagm if set to true, this will map all the destinations to a user</param>
        public void AssignDestinationsToUser(List<string> destinationsToAssign, string username,bool allDestinations=false)
        {
            var listOfAllDestinations = GetListOfDestinations();
            var userId = GetUserId(username);
            var destinationsAssignedToUser = GetDestinationsAssignedToAUser(username);
            var destinationIdToMap=new List<int>();
            if (allDestinations)
            {
                foreach (var destination in listOfAllDestinations)
                {
                    destinationIdToMap.Add(destination.id);
                }
            }
            else
            {
                foreach (var dest in destinationsToAssign)
                {
                    destinationIdToMap.Add(GetDestinationId(dest));
    
                }
            }
            foreach (var destination in destinationsAssignedToUser.DestinationIds)
            {
                destinationIdToMap.Remove(destination); // remove from list if already assigned
            }

            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            foreach (var destinationId in destinationIdToMap)
            {
                var resource = $"UserToDestinationMap/map?userId={userId}&destinationId={destinationId}";
                var request = new RestRequest(Method.POST)
                {
                    Resource = resource
                };
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", ParameterType.RequestBody);
                var response = RestHelpers.Execute(request, usrMgmtUri);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,  $"Unable to map the destination id {destinationId} to user id {userId}");
            }
        }

        /// <summary>
        /// Removes all the destinations from a user
        /// </summary>
        /// <param name="username">username to remove the destinations from</param>
        public void RemoveAllDestinationsFromUser(string username)
        {
            var destinationsAssignedToUser = GetDestinationsAssignedToAUser(username);
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            var userId = GetUserId(username);
            foreach (var destination in destinationsAssignedToUser.DestinationIds)
            {
                    var resource = $"UserToDestinationMap/unmap?userId={userId}&destinationId={destination}";
                    var request = new RestRequest(Method.POST)
                    {
                        Resource = resource
                    };
                    request.AddHeader("Accept", "application/json");
                    request.Parameters.Clear();
                    request.AddParameter("application/json", ParameterType.RequestBody);
                    var response = RestHelpers.Execute(request, usrMgmtUri);
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Unable to Unmap the destination id {destination} to user id {userId}");
            }
        }

        /// <summary>
        /// Gets List of Destinations assigned to a user
        /// </summary>
        /// <param name="username">username assigned to</param>
        /// <returns></returns>
        public UserToDestinationMap GetDestinationsAssignedToAUser(string username)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
            var endPoint = $"{usrMgmtUri}/UserToDestinationMap/destinations/{GetUserId(username)}";
            var request = new RestRequest(Method.GET)
            {
                Resource = endPoint,
                RequestFormat = DataFormat.Json,
            };
            var response = RestHelpers.Execute<List<UserToDestinationMap>>(request, new Uri(endPoint));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The Get list of destinations assigned to a user call failed");
            return JsonConvert.DeserializeObject<UserToDestinationMap>(response.Content);
        }

        /// <summary>
        /// Maps properties to a user
        /// </summary>
        /// <param name="propertiesToAssign">properties id to assign the user to</param>
        /// <param name="username">user to assign the properties</param>
        public void MapPropertiesToAUser(List<int> propertiesToAssign, string username)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var userId = GetUserId(username);
            foreach (var property in propertiesToAssign)
            {
                var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
                var resource = $"UserToPropertyMap/map?userId={userId}&propertyId={property}";
                var request = new RestRequest(Method.POST)
                {
                    Resource = resource
                };
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", ParameterType.RequestBody);
                var response = RestHelpers.Execute(request, usrMgmtUri);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Map Properties to Users call failed");
            }
        }

        /// <summary>
        /// Unmaps list of properties from a user
        /// </summary>
        /// <param name="propertiesToAssign">properties to unmap</param>
        /// <param name="username">username to unmap</param>
        public void UnMapPropertiesFromAUser(List<int> propertiesToAssign, string username)
        {
            var baseApiUri = ConfigurationManager.AppSettings["BaseApiUrl"].Replace("443", "");
            var userMgmtBaseUri = ConfigurationManager.AppSettings["usrmgmt_baseurl"];
            var usrMgmtSysAdminPort = ConfigurationManager.AppSettings["usrmgmtsysadmin_port"];
            var userId = GetUserId(username);
            foreach (var property in propertiesToAssign)
            {
                var usrMgmtUri = new Uri($"{baseApiUri}{usrMgmtSysAdminPort}{userMgmtBaseUri}");
                var resource = $"UserToPropertyMap/unmap?userId={userId}&propertyId={property}";
                var request = new RestRequest(Method.POST)
                {
                    Resource = resource
                };
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", ParameterType.RequestBody);
                var response = RestHelpers.Execute(request, usrMgmtUri);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Map Properties to Users call failed");
            }
        }
    }
}