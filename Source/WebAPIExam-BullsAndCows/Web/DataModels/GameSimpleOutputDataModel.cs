namespace Web.DataModels
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class GameSimpleOutputDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public static Expression<Func<Game, GameSimpleOutputDataModel>> ToModel
        {
            get
            {
                return g => new GameSimpleOutputDataModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Blue = g.Blue == null ? "No blue player yet" : g.Blue.UserName,
                    Red = g.Red.UserName,
                    GameState = g.GameState.ToString(),
                    DateCreated = g.DateCreated,
                };
            }
        }
    }
}