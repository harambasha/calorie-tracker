using CT.Repository.Models.CalorieTracking;
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
    public class MealsControllerTests
    {
        [TestClass]
        public class MealsAuthenticatedControllerTests : BaseAuthenticatedTests
        {
            [TestMethod]
            public async Task ShouldGetMealsWhenAuthenticated()
            {
                List<Meal> response = await GetAsync<List<Meal>>();
                Assert.AreNotEqual(0, response.Count());
            }

            protected override string Uri
            {
                get { return "/api/meals"; }
            }
        }

        [TestClass]
        public class MealsUnAuthenticatedControllerTests : BaseServerTest 
        {
            [TestMethod]
            public async Task ShouldNotGetMealsWhenNotAuthenticated()
            {
                var response = await GetAsync();
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }

            protected override string Uri
            {
                get { return "/api/meals"; }
            }
        }
    }
}