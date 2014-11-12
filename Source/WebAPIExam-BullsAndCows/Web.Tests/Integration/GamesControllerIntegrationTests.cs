using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;
using System.Net.Http;
using Data.Repositories;
using Moq;
using Model;
using Data;
using Web.Controllers;

namespace Web.Tests.Integration
{
    [TestClass]
    public class GamesControllerIntegrationTests
    {
        private static Random rand = new Random();
        private string inMemoryServerUrl = "http://localhost:12345";

        [TestMethod]
        public void GetTop__ShouldReturnStatus200AndNonEmptyContent()
        {

            var fakeRepo = new Mock<IRepository<Game>>();

            var games = new Game[] { new Game() };

            fakeRepo.Setup(f => f.All()).Returns(games.AsQueryable());

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Games).Returns(fakeRepo.Object);

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, fakeData.Object);

            var response = server.CreateGetRequest("/api/games");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }
    }
}