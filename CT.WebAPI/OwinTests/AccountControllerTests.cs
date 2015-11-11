using CT.Repository.Models.Identity;
using CT.WebAPI.OwinTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CT.WebAPI.OwinTests
{
    [TestClass]
    public class AccountControllerTests : BaseServerTest
    {
        [TestMethod]
        public async Task CanCreateUser()
        {
            var model = new CreateUserBindingModel
            {
                Username = "UserManagement" + DateTime.Now.Ticks,
                Email = "ismirharambasic81" + DateTime.Now.Ticks +"@gmail.com",
                FirstName = "Ismir",
                LastName = "Harambasic",
                Password = "Password11'",
                ConfirmPassword = "Password11'"
            };

            uri = uriBase + "/register";

            var response = await PostAsync(model);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task CannotCreateAUserEmailTaken()
        {
            var model = new CreateUserBindingModel
            {
                Username = "UserManagement" + DateTime.Now.Ticks,
                Email = "ismirharambasic81@gmail.com",
                FirstName = "Ismir",
                LastName = "Harambasic",
                Password = "Password11'",
                ConfirmPassword = "Password11'"
            };

            uri = uriBase + "/register";

            var response = await PostAsync(model);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestMethod]
        public async Task CannotCreateAUserUsernameTaken()
        {
            var model = new CreateUserBindingModel
            {
                Username = "UserManagement",
                Email = "ismirharambasic81@gmail.com",
                FirstName = "Ismir",
                LastName = "Harambasic",
                Password = "Password11'",
                ConfirmPassword = "Password11'"
            };

            uri = uriBase + "/register";

            var response = await PostAsync(model);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task ShouldNotGetLocalUserInfoWhenNotAuthenticated()
        {
            uri = uriBase + "/local-user-info";
            var response = await GetAsync();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        private string uriBase = "/api/account";
        private string uri = string.Empty;

        protected override string Uri
        {
            get { return uri; }
        }
    }
    

    [TestClass]
    public class AccountControllerAuthenticatedTests : BaseAuthenticatedTests
    {
        [TestMethod]
        public async Task ShouldGetLocalUserInfoWhenAuthenticated()
        {
            uri = uriBase + "/local-user-info";
            UserReturnModel userReturned = await GetAsync<UserReturnModel>();

            Assert.AreEqual("SuperPowerUser", userReturned.UserName);
            Assert.AreEqual("ismirharambasic@gmail.com", userReturned.Email);
            Assert.AreEqual("Ismir Harambasic", userReturned.FullName);
            Assert.AreEqual(2, userReturned.Roles.Count());

        }

        private string uriBase = "/api/account";
        private string uri = string.Empty;

        protected override string Uri
        {
            get { return uri; }
        }
    }
}