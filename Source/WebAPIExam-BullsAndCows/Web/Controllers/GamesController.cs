namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Data;
    using Model;
    using DataModels;

    public class GamesController : BaseApiController
    {
        public const int PageSize = 10;
        private Random random;

        public GamesController(IBullsAndCowsData data)
            : base(data)
        {
            this.random = new Random();
        }

        [HttpGet]
        public IHttpActionResult GetAll(int page)
        {
            var allGames = GetSortedGames();
            var userId = this.User.Identity.GetUserId();

            if (userId != null)
            {
                var toReturn = allGames.Where(g => g.GameState == GameState.WaitingForOpponent || ((g.Red.Id == userId || g.Blue.Id == userId) && (g.GameState != GameState.WonByBlue && g.GameState != GameState.WonByRed)))
                                                .Skip(page * PageSize)
                                                .Take(PageSize)
                                                .Select(GameSimpleOutputDataModel.ToModel);

                return Ok(toReturn);
            }

            var gamesToReturn = allGames.Where(g => g.GameState == GameState.WaitingForOpponent)
                                                .Skip(page * PageSize)
                                                .Take(PageSize)
                                                .Select(GameSimpleOutputDataModel.ToModel);

            return Ok(gamesToReturn);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(0);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var game = this.data.Games.All().Where(g => g.Id == id);

            if (game.FirstOrDefault() == null)
            {
                return NotFound();
            }

            if (game.FirstOrDefault().Blue.Id != userId && game.FirstOrDefault().Red.Id != userId)
            {
                return BadRequest();
            }

            if (game.FirstOrDefault().Blue.Id == userId)
            {
                var gameToReturn = game.Select(GameDetailedOutputDataModel.ToModelBlue).FirstOrDefault();

                return Ok(gameToReturn);
            }

            var gameToReturnWhenRed = game.Select(GameDetailedOutputDataModel.ToModelRed).FirstOrDefault();

            return Ok(gameToReturnWhenRed);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult CreateGame([FromBody] GameInputModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!GameLogic.GameLogic.IsValidNumber(model.Number))
            {
                return BadRequest();
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().Where(u => u.Id == userId).FirstOrDefault();

            var createdGame = new Game()
            {
                DateCreated = DateTime.Now,
                GameState = GameState.WaitingForOpponent,
                Name = model.Name,
                Red = user,
                RedNumber = model.Number,
            };

            this.data.Games.Add(createdGame);

            this.data.SaveChanges();

            var gameToReturn = new GameSimpleOutputDataModel()
            {
                Id = createdGame.Id,
                Name = createdGame.Name,
                Blue = createdGame.Blue == null ? "No blue player yet" : createdGame.Blue.UserName,
                Red = createdGame.Red.UserName,
                GameState = createdGame.GameState.ToString(),
                DateCreated = createdGame.DateCreated,
            };

            var response = this.Request.CreateResponse(HttpStatusCode.Created, gameToReturn);

            return ResponseMessage(response);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult JoinGame([FromUri]int id, [FromBody] OnlyNumberDataModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!GameLogic.GameLogic.IsValidNumber(model.Number))
            {
                return BadRequest();
            }

            var game = this.data.Games.Find(id);

            if (game == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();

            if (game.Red.Id == userId)
            {
                return BadRequest("Cannot join your own game");
            }

            if (game.GameState != GameState.WaitingForOpponent)
            {
                return BadRequest("Cannot join a game that is not in Waiting For Opponent state");
            }

            var bluePlayer = this.data.Users.Find(userId);

            game.Blue = bluePlayer;
            game.BlueNumber = model.Number;

            var notificationForJoin = new Notification()
            {
                Game = game,
                DateCreated = DateTime.Now,
                Message = bluePlayer.UserName + " joined your game \"" + game.Name + "\"",
                Player = game.Red,
                State = NotificationState.Unread,
                Type = NotificationType.GameJoined
            };

            this.data.Notifications.Add(notificationForJoin);

            if (this.random.Next(0, 2) == 0)
            {
                game.GameState = GameState.BlueInTurn;
                var notificationForTurn = new Notification()
                {
                    Game = game,
                    DateCreated = DateTime.Now,
                    Message = "It is your turn in game \"" + game.Name + "\"",
                    Player = game.Blue,
                    State = NotificationState.Unread,
                    Type = NotificationType.YourTurn
                };

                this.data.Notifications.Add(notificationForTurn);
            }
            else
            {
                game.GameState = GameState.RedInTurn;
                var notificationForTurn = new Notification()
                {
                    Game = game,
                    DateCreated = DateTime.Now,
                    Message = "It is your turn in game \"" + game.Name + "\"",
                    Player = game.Red,
                    State = NotificationState.Unread,
                    Type = NotificationType.YourTurn
                };

                this.data.Notifications.Add(notificationForTurn);
            }

            this.data.SaveChanges();

            var responseModel = new
            {
                result = "You joined game \"" + game.Name + "\""
            };

            return Ok(responseModel);
        }

        private IQueryable<Game> GetSortedGames()
        {
            return this.data.Games.All()
                                    .OrderBy(g => g.GameState)
                                    .ThenBy(g => g.Name)
                                    .ThenBy(g => g.DateCreated)
                                    .ThenBy(g => g.Red.UserName);
        }

    }
}
