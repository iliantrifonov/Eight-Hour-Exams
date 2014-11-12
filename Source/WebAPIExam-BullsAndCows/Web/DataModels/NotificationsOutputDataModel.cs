namespace Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using Data;
    using Model;

    public class NotificationsOutputDataModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string Type { get; set; }

        public string State { get; set; }

        public int GameId { get; set; }

        public static Expression<Func<Notification, NotificationsOutputDataModel>> ToModel
        {
            get
            {
                return n => new NotificationsOutputDataModel()
                {
                    Id = n.Id,
                    Message = n.Message,
                    DateCreated = n.DateCreated,
                    Type = n.Type.ToString(),
                    State = n.State.ToString(),
                    GameId = n.Game.Id
                };
            }
        }
    }
}