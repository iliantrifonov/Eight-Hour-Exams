
namespace Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Web;

    using Model;
    using Data;

    public class UserOutputDataModel
    {
        public string Username { get; set; }

        public int Rank { get; set; }

        public static Expression<Func<Player, UserOutputDataModel>> ToModel
        {
            get
            {
                return u => new UserOutputDataModel()
                {
                    Username = u.UserName,
                    Rank = u.Rank
                };
            }
        }
    }
}