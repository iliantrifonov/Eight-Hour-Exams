namespace Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using Model;

    public class GameDetailedOutputDataModel
    {
        public GameDetailedOutputDataModel()
        {
            this.YourGuesses = new HashSet<GuessDataModel>();
            this.OpponentGuesses = new HashSet<GuessDataModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public string YourNumber { get; set; }

        public virtual IEnumerable<GuessDataModel> YourGuesses { get; set; }

        public virtual IEnumerable<GuessDataModel> OpponentGuesses { get; set; }

        public string YourColor { get; set; }

        public string GameState { get; set; }

        public static Expression<Func<Game, GameDetailedOutputDataModel>> ToModelRed
        {
            get
            {

                return g =>


                    new GameDetailedOutputDataModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    DateCreated = g.DateCreated,
                    Red = g.Red.UserName,
                    Blue = g.Blue.UserName,
                    YourNumber = g.RedNumber,
                    YourGuesses = g.RedGuesses.AsQueryable().Select(GuessDataModel.ToModel),
                    OpponentGuesses = g.BlueGuesses.AsQueryable().Select(GuessDataModel.ToModel),
                    YourColor = "red",
                    GameState = g.GameState.ToString(),
                };
            }
        }

        public static Expression<Func<Game, GameDetailedOutputDataModel>> ToModelBlue
        {
            get
            {
                return g => new GameDetailedOutputDataModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    DateCreated = g.DateCreated,
                    Red = g.Red.UserName,
                    Blue = g.Blue.UserName,
                    YourNumber = g.BlueNumber,
                    YourGuesses = g.BlueGuesses.AsQueryable().Select(GuessDataModel.ToModel),
                    OpponentGuesses = g.RedGuesses.AsQueryable().Select(GuessDataModel.ToModel),
                    YourColor = "blue",
                    GameState = g.GameState.ToString(),
                };
            }
        }
    }
}