
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
    using DataModels;
    using Data;
    using GameLogic;

    [RoutePrefix("api")]
    public class GuessController : BaseApiController
    {
        public GuessController(IBullsAndCowsData data)
            : base(data)
        {

        }

        [Authorize]
        [HttpPost]
        [Route("games/{id}/guess")]
        public IHttpActionResult PostGuess(int id, [FromBody] OnlyNumberDataModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var game = this.data.Games.Find(id);

            if (game == null)
            {
                return NotFound();
            }

            if (game.GameState != GameState.RedInTurn && game.GameState != GameState.BlueInTurn)
            {
                return BadRequest();
            }

            var userId = this.User.Identity.GetUserId();

            if (game.Red.Id != userId && game.Blue.Id != userId)
            {
                return BadRequest("This is not your game!");
            }

            if (game.GameState == GameState.BlueInTurn && game.Blue.Id != userId)
            {
                return BadRequest("It is not your turn");
            }

            if (game.GameState == GameState.RedInTurn && game.Red.Id != userId)
            {
                return BadRequest("It is not your turn");
            }

            if (!GameLogic.IsValidNumber(model.Number))
            {
                return BadRequest();
            }

            if (game.Red.Id == userId)
            {
                return this.RedPlays(game, model);
            }
            else
            {
                return this.BluePlays(game, model);
            }
        }

        private IHttpActionResult RedPlays(Game game, OnlyNumberDataModel model)
        {
            var bullsAndCows = GameLogic.GetCowAndBullCount(game.BlueNumber, model.Number);

            if (bullsAndCows.BullsCount == 4)
            {
                // red wins

                game.GameState = GameState.WonByRed;

                game.Red.WinCount += 1;
                game.Blue.LossCount += 1;

                game.Red.Rank = GameLogic.CalculateRank(game.Red.WinCount, game.Red.LossCount);
                game.Blue.Rank = GameLogic.CalculateRank(game.Blue.WinCount, game.Blue.LossCount);

                var winningNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = "You beat " + game.Blue.UserName + " in game \"" + game.Name + "\"",
                    Player = game.Red,
                    State = NotificationState.Unread,
                    Type = NotificationType.GameWon,
                };

                this.data.Notifications.Add(winningNotification);

                var losingNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = game.Red.UserName + " beat you in game \"" + game.Name + "\"",
                    Player = game.Blue,
                    State = NotificationState.Unread,
                    Type = NotificationType.GameLost,
                };

                this.data.Notifications.Add(losingNotification);

                this.data.SaveChanges();
            }
            else
            {
                game.GameState = GameState.BlueInTurn;

                var turnNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = "It is your turn in game \"" + game.Name + "\"",
                    Player = game.Blue,
                    State = NotificationState.Unread,
                    Type = NotificationType.YourTurn,
                };

                this.data.Notifications.Add(turnNotification);
            }

            var guess = new Guess()
            {
                BullsCount = bullsAndCows.BullsCount,
                CowsCount = bullsAndCows.CowsCount,
                DateMade = DateTime.Now,
                Game = game,
                Number = model.Number,
                Player = game.Red,
            };

            this.data.Guesses.Add(guess);
            this.data.SaveChanges();

            var guessToReturn = this.data.Guesses.All()
                .Where(g => g.Id == guess.Id)
                .Select(GuessDataModel.ToModel)
                .FirstOrDefault();

            var response = this.Request.CreateResponse(HttpStatusCode.Created, guessToReturn);

            return ResponseMessage(response);
        }

        private IHttpActionResult BluePlays(Game game, OnlyNumberDataModel model)
        {
            var bullsAndCows = GameLogic.GetCowAndBullCount(game.RedNumber, model.Number);

            if (bullsAndCows.BullsCount == 4)
            {
                // blue wins

                game.GameState = GameState.WonByBlue;

                game.Red.LossCount += 1;
                game.Blue.WinCount += 1;

                game.Red.Rank = GameLogic.CalculateRank(game.Red.WinCount, game.Red.LossCount);
                game.Blue.Rank = GameLogic.CalculateRank(game.Blue.WinCount, game.Blue.LossCount);

                var winningNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = "You beat " + game.Red.UserName + " in game \"" + game.Name + "\"",
                    Player = game.Blue,
                    State = NotificationState.Unread,
                    Type = NotificationType.GameWon,
                };

                this.data.Notifications.Add(winningNotification);

                var losingNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = game.Blue.UserName + " beat you in game \"" + game.Name + "\"",
                    Player = game.Red,
                    State = NotificationState.Unread,
                    Type = NotificationType.GameLost,
                };

                this.data.Notifications.Add(losingNotification);

                this.data.SaveChanges();
            }
            else
            {
                game.GameState = GameState.RedInTurn;

                var turnNotification = new Notification()
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    Message = "It is your turn in game \"" + game.Name + "\"",
                    Player = game.Red,
                    State = NotificationState.Unread,
                    Type = NotificationType.YourTurn,
                };

                this.data.Notifications.Add(turnNotification);
            }

            var guess = new Guess()
            {
                BullsCount = bullsAndCows.BullsCount,
                CowsCount = bullsAndCows.CowsCount,
                DateMade = DateTime.Now,
                Game = game,
                Number = model.Number,
                Player = game.Blue,
            };

            this.data.Guesses.Add(guess);
            this.data.SaveChanges();

            var guessToReturn = this.data.Guesses.All()
                .Where(g => g.Id == guess.Id)
                .Select(GuessDataModel.ToModel)
                .FirstOrDefault();

            var response = this.Request.CreateResponse(HttpStatusCode.Created, guessToReturn);

            return ResponseMessage(response);
        }
    }
}
