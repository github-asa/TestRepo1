using static J2BIOverseasOps.Models.CaseAutoAssigneesApi;

namespace J2BIOverseasOps.Models
{
    public class AvailableAssigneesApi
    {

        public class CaseAssignee
        {
            public string Title { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string UserDomain { get; }
            public string UserName { get; }
            public string JobTitle { get; }
            public string Email { get; }
            public string Telephone { get; }

            public Identifier Identifier { get; }

            public CaseAssignee(string title, string firstName, string lastName, string userDomain, string userName, string jobTitle, string email, string telephone, Identifier identifier)
            {
                Title = title;
                FirstName = firstName;
                LastName = lastName;
                UserDomain = userDomain;
                UserName = userName;
                JobTitle = jobTitle;
                Email = email;
                Telephone = telephone;
                Identifier = identifier;
            }
        }

    }
}
