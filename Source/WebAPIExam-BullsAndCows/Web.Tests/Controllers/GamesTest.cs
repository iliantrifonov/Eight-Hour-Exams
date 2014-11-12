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
    public class GamesTest
    {
        [TestMethod]
        public void GetAll_WhenLessThan10AndValid_ReturnsCorrect()
        {
            var fakeRepo = new Mock<IRepository<Game>>();

            var games = GetAllDifferenGamesToTest(5);

            fakeRepo.Setup(f => f.All()).Returns(games);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Games).Returns(fakeRepo.Object);

            var controller = new GamesController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetAll();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<GameSimpleOutputDataModel>>().Result.Select(u => u.Id).ToList();

            var expected = games.OrderBy(g => g.GameState)
                                    .ThenBy(g => g.Name)
                                    .ThenBy(g => g.DateCreated)
                                    .ThenBy(g => g.Red.UserName).Select(u => u.Id).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAll_WhenStateIsInvalid_ReturnsCorrect()
        {
            var fakeRepo = new Mock<IRepository<Game>>();

            var games = GetInvalidGameStateToTest(5);

            fakeRepo.Setup(f => f.All()).Returns(games);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Games).Returns(fakeRepo.Object);

            var controller = new GamesController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetAll();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<GameSimpleOutputDataModel>>().Result.Select(u => u.Id).ToList();

            var expected = games.Where(g=> g.GameState == GameState.WaitingForOpponent)
                .OrderBy(g => g.GameState)
                                    .ThenBy(g => g.Name)
                                    .ThenBy(g => g.DateCreated)
                                    .ThenBy(g => g.Red.UserName).Select(u => u.Id).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAll_WhenMoreThan10_PagesCorrectly()
        {
            var fakeRepo = new Mock<IRepository<Game>>();

            var games = GetAllDifferenGamesToTest(15);

            fakeRepo.Setup(f => f.All()).Returns(games);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Games).Returns(fakeRepo.Object);

            var controller = new GamesController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetAll();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<GameSimpleOutputDataModel>>().Result.Select(u => u.Id).ToList();

            var expected = games.OrderBy(g => g.GameState)
                                    .ThenBy(g => g.Name)
                                    .ThenBy(g => g.DateCreated)
                                    .ThenBy(g => g.Red.UserName).Select(u => u.Id).Take(10).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetAll_WhenNamesAreSame_ReturnsCorrect()
        {
            var fakeRepo = new Mock<IRepository<Game>>();

            var games = GetInvalidGameStateToTest(5);

            fakeRepo.Setup(f => f.All()).Returns(games);

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Games).Returns(fakeRepo.Object);

            var controller = new GamesController(fakeData.Object);

            SetupController(controller);

            var httpActionResult = controller.GetAll();

            var response = httpActionResult.ExecuteAsync(CancellationToken.None).Result;


            var actual = response.Content.ReadAsAsync<IEnumerable<GameSimpleOutputDataModel>>().Result.Select(u => u.Id).ToList();

            var expected = games.Where(g => g.GameState == GameState.WaitingForOpponent)
                .OrderBy(g => g.GameState)
                                    .ThenBy(g => g.Name)
                                    .ThenBy(g => g.DateCreated)
                                    .ThenBy(g => g.Red.UserName).Select(u => u.Id).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private IQueryable<Game> GetAllDifferenGamesToTest(int count)
        {
            var games = new Game[count];
            for (int i = 0; i < count; i++)
            {
                games[i] = new Game()
                {
                    Id = i,
                    GameState = GameState.WaitingForOpponent,
                    Name = "Test Name #" + i,
                    DateCreated = DateTime.Now,
                    Red = new Player() { UserName = "User #" + i }
                };
            }

            return games.AsQueryable();
        }

        private IQueryable<Game> GetSameNamesToTest(int count)
        {
            var games = new Game[count];
            for (int i = 0; i < count; i++)
            {
                games[i] = new Game()
                {
                    Id = i,
                    GameState = GameState.WaitingForOpponent,
                    Name = "Test Name #" + i,
                    DateCreated = DateTime.Now,
                    Red = new Player() { UserName = "User #" + i }
                };
            }

            return games.AsQueryable();
        }

        private IQueryable<Game> GetInvalidGameStateToTest(int count)
        {
            var games = new Game[count];
            for (int i = 0; i < count; i++)
            {
                games[i] = new Game()
                {
                    Id = i,
                    GameState = GameState.RedInTurn,
                    Name = "Test Name #" + i,
                    DateCreated = DateTime.Now,
                    Red = new Player() { UserName = "User #" + i }
                };
            }

            return games.AsQueryable();
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
                        { "controller", "games" }
                    });
        }

    }
}
