using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;

namespace CT.WebAPI.OwinTests.Base
{
    public abstract class BaseAuthenticatedTests : BaseServerTest
    {
        protected virtual string Username { get { return "SuperPowerUser"; } }
        protected virtual string Password { get { return "Password11"; } }

        private string token;

        protected override void PostSetup(TestServer server)
        {
            var tokenDetails = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = server.HttpClient.PostAsync("/oauth/token", tokenPostData).Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResult.StatusCode);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            token = (string)body["access_token"];
        }

        protected async Task<TResult> GetAsync<TResult>()
        {
            var response = await GetAsync();
            return await response.Content.ReadAsAsync<TResult>();
        }

        protected override async Task<HttpResponseMessage> GetAsync()
        {
            return await server.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            return await server.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }
    }
}