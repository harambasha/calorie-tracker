using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CT.WebAPI.OwinTests.Base
{
    public abstract class BaseServerTest
    {
        protected TestServer server;

        [TestInitialize]
        public void Setup()
        {
            server = TestServer.Create(app =>
            {
                Startup startup = new Startup();
                startup.Configuration(app);
            });
            PostSetup(server);
        }

        protected virtual void PostSetup(TestServer server)
        {
        }

        [TestCleanup]
        public void Teardown()
        {
            if (server != null)
                server.Dispose();
        }

        protected abstract string Uri { get; }

        protected virtual async Task<HttpResponseMessage> GetAsync()
        {
            return await server.CreateRequest(Uri).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            return await server.CreateRequest(Uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }
    }
}