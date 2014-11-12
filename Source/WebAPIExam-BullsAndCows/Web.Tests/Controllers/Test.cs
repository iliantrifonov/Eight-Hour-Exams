//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Data;
//using Moq;
//using Web.Controllers;
//using System.Threading;
//using System.Web.Http;
//using System.Net.Http;
//using System.Net;
//using System.Linq;
//using System.Collections.Generic;
//using Model;
//using System.Web.Http.Routing;
//using Web.DataModels;

//namespace Web.Tests.Controllers
//{
//    [TestClass]
//    public class TestControllerTest
//    {
//        [TestMethod]
//        public void GetOneData_WhenValid_ReturnsOkResponse()
//        {
//            // TODO :test for all constraints
//            //TODO : Mock a repository and give that to the application data
//            var fakeData = new Mock<IApplicationData>();
//            var collection = new List<TestModelOne>() { new TestModelOne() { Name = "TestName", Id = 2 }};
//            fakeData.Setup(f => f.Ones.All()).Returns(collection.AsQueryable());

//            var controller = new TestController(fakeData.Object);

//            SetupController(controller);

//            var httpActionResult = controller.GetOneData();

//            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;

            
//            Assert.IsTrue(response.Content.ReadAsAsync<IEnumerable<TestModel>>().Result.Select(r => r.Name).FirstOrDefault() == "TestName");
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }

//        private void SetupController(ApiController controller)
//        {
//            string serverUrl = "http://test-url.com";

//            //Setup the Request object of the controller
//            var request = new HttpRequestMessage()
//            {
//                RequestUri = new Uri(serverUrl)
//            };
//            controller.Request = request;

//            //Setup the configuration of the controller
//            var config = new HttpConfiguration();
//            config.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{id}",
//                defaults: new { id = RouteParameter.Optional });
//            controller.Configuration = config;

//            //Apply the routes of the controller
//            controller.RequestContext.RouteData =
//                new HttpRouteData(
//                    route: new HttpRoute(),
//                    values: new HttpRouteValueDictionary
//                    {
//                        { "controller", "test" }
//                    });
//        }
//    }
//}
