using static J2BIOverseasOps.Models.UserMgmtApi;

namespace J2BIOverseasOps.Models
{
    public static class UsersHelpers
    {
        public static string GetDisplayName(this Users user)
        {

            if (string.IsNullOrWhiteSpace(user.forename) || string.IsNullOrWhiteSpace(user.surname))
            {
                return $"{user.username} ({user.title})";
            }

            return $"{user.forename} {user.surname} ({user.title})";

        }
    }

    public class UserMgmtApi
    {

        public class Users
        {
            public int userId { get; set; }
            public string username { get; set; }
            public string forename { get; set; }
            public string surname { get; set; }
            public string workEmail { get; set; }
            public bool isActive { get; set; }
            public string department { get; set; }
            public string title { get; set; }
            public string mobile { get; set; }
            public string destination { get; set; }
        }


        public class UserPermissionsInfo
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
            public string Fullname { get; set; }
            public string JobTitle { get; set; }
            public string Department { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public object[] Destinations { get; set; }
            public object[] Properties { get; set; }
            public string Rolename { get; set; }
        }

    }
}

