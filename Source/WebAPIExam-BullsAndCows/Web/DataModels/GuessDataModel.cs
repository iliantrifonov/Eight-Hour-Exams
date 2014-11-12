namespace Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using Model;
    using System.Linq.Expressions;

    public class GuessDataModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }

        public static Expression<Func<Guess, GuessDataModel>> ToModel
        {
            get
            {
                return g => new GuessDataModel()
                {
                    Id = g.Id,
                    UserId = g.Player.Id,
                    Username = g.Player.UserName,
                    GameId = g.Game.Id,
                    Number = g.Number,
                    DateMade = g.DateMade,
                    CowsCount = g.CowsCount,
                    BullsCount = g.BullsCount
                };
            }
        }
    }
}