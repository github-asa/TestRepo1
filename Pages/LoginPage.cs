using System;
using System.Configuration;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using J2BIOverseasOps.Models;

namespace J2BIOverseasOps.Pages
{
    internal class LoginPage : BasePage
    {
        private readonly By _btnLogin = By.CssSelector("button.btn.btn-primary");
        private readonly By _errorMessage = By.CssSelector("div.alert.alert-danger");
        private readonly By _inputPassword = By.CssSelector("#Password");
        private readonly By _inputUsername = By.CssSelector("#Username");
        private readonly By _logoutLink = By.CssSelector("#navbar-logout");
        private readonly By _pageHeaderLogout = By.CssSelector("div.page-header.logged-out");
        private readonly By _adfsLogoutHeader=By.XPath("//div[@id='signoutArea']");
        private readonly By _jet2HolidasyIcon=By.XPath("//*[@src='/jet2.overseasops.identityserver/jet2h.jpg']");

        private readonly By _adfsLoginBtn = By.CssSelector("#submitButton");
        private readonly By _adfsInputPassword = By.CssSelector("#passwordInput");
        private readonly By _adfsInputUsername = By.CssSelector("#userNameInput");

        protected IRunData RunData;
        private readonly ApiCalls _apiCalls;

        public LoginPage(IWebDriver driver, ILog log, IRunData data) : base(driver, log)
        {
            RunData = data;
            _apiCalls = new ApiCalls(data);
        }

        public void EnterLoginUsername(string username)
        {
            if (username== "restricted_user")
            {
                username = RunData.RestrictedUserName;
            }
            Assert.True(Driver.WaitForItem(_inputUsername), $"The Username input field is not present");
            Log.Debug($"Log in username {username}");
            Driver.EnterText(_inputUsername, username);
        }

        public void EnterAdfsLoginUsername(string username)
        {
            Assert.True(Driver.WaitForItem(_adfsInputUsername), $"The Username input field for ADFS is not present");
            Log.Debug($"Log in adfs username {username}");
            Driver.EnterText(_adfsInputUsername, username);
        }

        public void EnterLoginPassword(string password)
        {
            if (password == "restricted_user_password")
            {
                password = RunData.RestrictedPassw;
            }
            Assert.True(Driver.WaitForItem(_inputPassword), $"The Password input field is not present");
            Log.Debug($"Log in password {password}");
            Driver.EnterText(_inputPassword, password);
        }

        public void EnterAdfsLoginPassword(string password)
        {
            Assert.True(Driver.WaitForItem(_adfsInputPassword), $"The ADFS Password input field is not present");
            Log.Debug($"Log in password {password}");
            Driver.EnterText(_adfsInputPassword, password);
        }

        public void ClickLoginButton()
        {
            Driver.ClickItem(_btnLogin);
        }

        public void ClickAdfsLoginButton()
        {
            Driver.ClickItem(_adfsLoginBtn);
        }

        public void LoginUser(string user)
        {
            string userName;
            string password;
            string role;
            string fullname;
            switch (user.ToLower())
            {
                case "admin":
                    userName = RunData.AdminUserName;
                    password = RunData.AdminPassw;
                    role = RunData.AdminRole;
                    fullname = RunData.AdminUserFullName;
                    break;
                case "restricted":
                    userName = RunData.RestrictedUserName;
				    password = RunData.RestrictedPassw;
                    role = RunData.RestrictedRole;
                    fullname = RunData.RestrictedUserFullName;
                    break;
                case "team1":
                    userName = RunData.Team1UserName;
                    password = RunData.Team1Passw;
                    role = RunData.Team1Role;
                    fullname = RunData.Team1UserFullName;
                    break;
                case "team3":
                    userName = ConfigurationManager.AppSettings["Tea3UserName"];
                    password = ConfigurationManager.AppSettings["Team3Passw"];
                    role = RunData.Team1Role;
                    fullname = RunData.Team1UserFullName;
                    break;
                default:
				    throw new Exception($"{user} is not a valid user Type");
		    }
           // Driver.ClickItem(_jet2HolidasyIcon);
          //  Driver.WaitForUrl("adfstest.dgtest.local");
            EnterLoginUsername(userName);
		    EnterLoginPassword(password);
		    ClickLoginButton();
			Driver.VerifyNavigatedToPage(RunData.BaseUrl);
	        ScenarioContext.Current[CurrentUsername] = userName;
	        ScenarioContext.Current[CurrentPassword] = password;
            ScenarioContext.Current[CurrentUserRole] = role;
            ScenarioContext.Current[CurrentUserFullname] = fullname;
            ScenarioContext.Current[CurrentUserDisplayName] = _apiCalls.GetUserDetails(userName).GetDisplayName();
            Driver.VerifyNavigatedToPage(RunData.BaseUrl);
            SetAuthorisedToken();
        }


        public void LoginAdfsUser(string user)
        {
            string userName;
            string password;
            string role;
            string fullname;
            switch (user.ToLower())
            {
                case "admin":
                    userName = RunData.AdminAdfsUserName;
                    password = RunData.AdminAdfsPassw;
                    role = RunData.AdminAdfsRole;
                    fullname = RunData.AdminAdfsUserFullName;
                    break;
                case "restricted":
                    userName = RunData.RestrictedAdfsUserName;
                    password = RunData.RestrictedAdfsPassw;
                    role = RunData.RestrictedAdfsRole;
                    fullname = RunData.RestrictedAdfsUserFullName;
                    break;
                default:
                    throw new Exception($"{user} is not a valid ADFS user Type");
            }
            EnterAdfsLoginUsername(userName);
            EnterAdfsLoginPassword(password);
            ClickAdfsLoginButton();
            Driver.VerifyNavigatedToPage(RunData.BaseUrl);
            ScenarioContext.Current[CurrentUsername] = userName;
            ScenarioContext.Current[CurrentPassword] = password;
            ScenarioContext.Current[CurrentUserRole] = role;
            ScenarioContext.Current[CurrentUserFullname] = fullname;
            ScenarioContext.Current[CurrentUserDisplayName] = _apiCalls.GetUserDetails(userName).GetDisplayName();
            Driver.VerifyNavigatedToPage(RunData.BaseUrl);
        }



        public void VerifyError(string text)

	    {
		    Assert.True(Driver.WaitForItem(_errorMessage));
		    var errorText = Driver.FindElement(_errorMessage).Text;
		    Assert.True(errorText.Contains("Error"), $"Unable to verify Error heading");
		    Assert.True(errorText.Contains(text), $"Unable to verify invalid credentials error message. Expected:{text} Actual :{errorText}");
	    }

	    public void VerifyLoginScreenDisplayed()
	    {
		    Assert.True(Driver.WaitForItem(_inputUsername),$"Unable to funde username input field on login screen");
		    Assert.True(Driver.WaitForItem(_inputPassword),$"Unable to find password input field on Login screen");
		}

        public void LogOut(bool adfs=false)
        {
            Driver.ClickItem(_logoutLink);
            Driver.WaitForPageToLoad();
//            if (adfs)
//            {
//                logoutHeader = _adfsLogoutHeader;
//            }
            Assert.True(Driver.WaitForItem(_btnLogin), "Timed out while waiting for logout page header");
        }

        public void ClickJet2LoginIcon()
        {
            Driver.ClickItem(_jet2HolidasyIcon);
        }

        private void SetAuthorisedToken()
        {
            var authorizationResult= Driver.GetLocalStorageItem("authorizationResult"); // get token from local storage of the driver
            // trim and substring the actual token
            if (authorizationResult != null)
            {
                var accessToken = authorizationResult.Substring(authorizationResult.IndexOf("access_token", StringComparison.Ordinal));
                var authorisationToken = "Bearer " + accessToken
                                             .Replace(
                                                 accessToken.Substring(accessToken.IndexOf("token_type",
                                                     StringComparison.Ordinal)), "")
                                             .Replace("access_token\":\"", "").Replace("\",\"","");         
                RunData.AuthorisationToken = authorisationToken;
            }
        }

    }
}