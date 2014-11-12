using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;
using System.Threading;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http.Routing;
using Model;
using Data;
using Data.Repositories;
using Web.DataModels;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class ScoreTests
    {
        [TestMethod]
        public void GetTopScores_WhenLessThan10ScoresGetsScores_SortsByScoreCorrectly()
        {
            var fakeRepo = new Mock<IRepository<Player>>();

            var users = GetUsersToTest(5);

            fakeRepo.Setup(f => f.All()).Returns(users);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Users).Returns(fakeRepo.Object);

            var controller = new ScoresController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetTopScores();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<UserOutputDataModel>>().Result.Select(u => u.Username).ToList();

            var expected = users.OrderBy(u => u.Rank)
                                .Take(10)
                                .Select(u => u.UserName).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetTopScores_WhenMoreThan10ScoresGetsScores_SortsByScoreCorrectly()
        {
            var fakeRepo = new Mock<IRepository<Player>>();

            var users = GetUsersToTest(15);

            fakeRepo.Setup(f => f.All()).Returns(users);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Users).Returns(fakeRepo.Object);

            var controller = new ScoresController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetTopScores();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<UserOutputDataModel>>().Result.Select(u => u.Username).ToList();

            var expected = users.OrderBy(u => u.Rank)
                                .Take(10)
                                .Select(u => u.UserName).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetTopScores_WhenRanksAreEqual_SortsByScoreCorrectly()
        {
            var fakeRepo = new Mock<IRepository<Player>>();

            var users = GetUsersToTestSameRank(15);

            fakeRepo.Setup(f => f.All()).Returns(users);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Users).Returns(fakeRepo.Object);

            var controller = new ScoresController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetTopScores();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<UserOutputDataModel>>().Result.Select(u => u.Username).ToList();

            var expected = users.OrderBy(u => u.Rank)
                                .ThenBy(u => u.UserName)
                                .Take(10)
                                .Select(u => u.UserName).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetTopScores_WhenEmpty_ReturnsZero()
        {
            var fakeRepo = new Mock<IRepository<Player>>();

            var users = GetUsersToTest(0);

            fakeRepo.Setup(f => f.All()).Returns(users);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Users).Returns(fakeRepo.Object);

            var controller = new ScoresController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetTopScores();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<UserOutputDataModel>>().Result.Select(u => u.Username).ToList();

            var expected = users.OrderBy(u => u.Rank)
                                .Take(10)
                                .Select(u => u.UserName).ToList();

            Assert.AreEqual(0, actual.Count);
            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private IQueryable<Player> GetUsersToTest(int count)
        {
            var users = new Player[count];

            for (int i = 0; i < count; i++)
            {
                users[i] = new Player()
                {
                    UserName = "Name #" + i,
                    Rank = 100 + i
                };
            }

            return users.AsQueryable();
        }

        private IQueryable<Player> GetUsersToTestSameRank(int count)
        {
            var users = new Player[count];

            for (int i = 0; i < count; i++)
            {
                users[i] = new Player()
                {
                    UserName = "Name #" + i,
                    Rank = 100 + i
                };
            }

            return users.AsQueryable();
        }

        private void SetupController(ApiController controller)
        {
            string serverUrl = "http://test-url.com";

            //Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };
            controller.Request = request;

            //Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;

            //Apply the routes of the controller
            controller.RequestContext.RouteData =
                new HttpRouteData(
                    route: new HttpRoute(),
                    values: new HttpRouteValueDictionary
                    {
                        { "controller", "scores" }
                    });
        }
    }
}
