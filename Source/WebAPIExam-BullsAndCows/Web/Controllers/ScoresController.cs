namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Model;
    using Data;
    using DataModels;

    public class ScoresController : BaseApiController
    {
        private const int ScoreCount = 10;

        public ScoresController(IBullsAndCowsData data)
            : base(data)
        {

        }

        [HttpGet]
        public IHttpActionResult GetTopScores()
        {
            var usersToReturn = this.data.Users.All()
                                    .OrderBy(u => u.Rank)
                                    .ThenBy(u => u.UserName)
                                    .Take(ScoreCount)
                                    .Select(UserOutputDataModel.ToModel);

            return Ok(usersToReturn);
        }
    }
}
