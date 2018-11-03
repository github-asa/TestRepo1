using System.Collections.Specialized;
using System.Configuration;

namespace J2BIOverseasOps
{
    public class RunData : IRunData
    {
        public string Browser => GetValue("Browser");
        public string BaseUrl => GetValue("BaseUrl");
        public string Headless => GetValue("Headless");
        public string BaseApiUrl => GetValue("BaseApiUrl");

        public string AdminUserFullName => GetSectionValue("UserData","AdminUserFullName");
        public string AdminUserName => GetSectionValue("UserData", "AdminUserName");
        public string AdminPassw => GetSectionValue("UserData", "AdminPassw");
        public string AdminRole => GetSectionValue("UserData", "AdminRole");
        public string RestrictedUserName => GetSectionValue("UserData", "RestrictedUsername");
        public string RestrictedPassw => GetSectionValue("UserData", "RestrictedPassw");
        public string RestrictedUserFullName => GetSectionValue("UserData", "RestrictedUserFullName");
        public string RestrictedRole => GetSectionValue("UserData","RestrictedRole");
        public string Team1UserFullName => GetSectionValue("UserData","Team1UserFullName");
        public string Team1UserName => GetSectionValue("UserData","Team1UserName");
        public string Team1Passw => GetSectionValue("UserData","Team1Passw");
        public string Team1Role => GetSectionValue("UserData","Team1Role");
        public string AdminAdfsUserFullName => GetSectionValue("UserData","ADFSAdminUserFullName");
        public string AdminAdfsUserName => GetSectionValue("UserData","ADFSAdminUserName");
        public string AdminAdfsPassw => GetSectionValue("UserData","ADFSAdminPassw");
        public string AdminAdfsRole => GetSectionValue("UserData","ADFSAdminRole");
        public string RestrictedAdfsUserFullName => GetSectionValue("UserData", "ADFSRestrictedUserFullName");
        public string RestrictedAdfsUserName => GetSectionValue("UserData", "ADFSRestrictedUserName");
        public string RestrictedAdfsPassw => GetSectionValue("UserData", "ADFSRestrictedPassw");
        public string RestrictedAdfsRole => GetSectionValue("UserData", "ADFSRestrictedRole");
        public string AuthorisationToken { get; set; } = "";


        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static string GetSectionValue(string sectionName,string key)
        {
            var section= (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            return section[key];
        }
    }

    public interface IRunData
    {
        string Browser { get; }
        string BaseUrl { get; }
        string AdminUserFullName { get; }
        string AdminUserName { get; }
        string AdminPassw { get; }
        string AdminRole { get; }
        string RestrictedUserName { get; }
        string RestrictedUserFullName { get; }
        string RestrictedPassw { get; }
        string RestrictedRole { get; }
        string Team1UserName { get; }
        string Team1UserFullName { get; }
        string Team1Passw { get; }
        string Team1Role { get; }
        string Headless { get; }
        string BaseApiUrl { get; }

        string AdminAdfsUserFullName { get; }
        string AdminAdfsUserName { get; }
        string AdminAdfsPassw { get; }
        string AdminAdfsRole { get; }

        string RestrictedAdfsUserFullName { get; }
        string RestrictedAdfsUserName { get; }
        string RestrictedAdfsPassw { get; }
        string RestrictedAdfsRole { get; }
        string AuthorisationToken { get; set; }
    }
}