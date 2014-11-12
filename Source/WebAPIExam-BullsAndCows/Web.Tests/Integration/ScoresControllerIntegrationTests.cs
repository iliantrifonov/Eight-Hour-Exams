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
    public class ScoresControllerIntegrationTests
    {
        private static Random rand = new Random();
        private string inMemoryServerUrl = "http://localhost:12345";

        [TestMethod]
        public void GetAll_ShouldReturnStatus200AndNonEmptyContent()
        {

            var fakeRepo = new Mock<IRepository<Player>>();

            var users = new Player[] { new Player() };

            fakeRepo.Setup(f => f.All()).Returns(users.AsQueryable());

            var fakeData = new Mock<IBullsAndCowsData>();

            fakeData.Setup(fd => fd.Users).Returns(fakeRepo.Object);

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, fakeData.Object);

            var response = server.CreateGetRequest("/api/scores");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }
    }
}