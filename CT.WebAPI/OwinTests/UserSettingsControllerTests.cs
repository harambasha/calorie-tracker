using CT.Repository.Models.UserSetting;
using CT.WebAPI.OwinTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CT.WebAPI.OwinTests
{
    public class UserSettingsControllerTests
    {
        [TestClass]
        public class UserSettingsAuthenticatedControllerTests : BaseAuthenticatedTests
        {
            [TestMethod]
            public async Task ShouldPostUserSettingsWhenAuthenticated()
            {
                var model = new UserSetting
                {
                   DailyCalories = 200
                };

                var response = await PostAsync(model);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [TestMethod]
            public async Task ShouldGetUserSettingsWhenAuthenticated()
            {
                List<UserSetting> response = await GetAsync<List<UserSetting>>();
                Assert.AreNotEqual(0, response.Count());
            }

            protected override string Uri
            {
                get { return "/api/user-settings"; }
            }
        }


        [TestClass]
        public class UserSettingsNonAuthenticatedControllerTests : BaseServerTest
        {
            [TestMethod]
            public async Task ShouldNotGetMealsWhenNotAuthenticated()
            {
                var response = await GetAsync();
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            protected override string Uri
            {
                get { return "/api/user-settings"; }
            }
        }
    }
}