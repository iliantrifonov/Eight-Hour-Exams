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

    public class NotificationsController : BaseApiController
    {
        private const int NotificationsPerPage = 10;

        public NotificationsController(IBullsAndCowsData data)
            : base(data)
        {

        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetNotifications(int page)
        {
            var userId = this.User.Identity.GetUserId();

            var notifications = GetSortedNotifications(userId)
                .Skip(page * NotificationsPerPage)
                .Take(NotificationsPerPage);

            var toReturn = notifications.Select(NotificationsOutputDataModel.ToModel).ToList();

            foreach (var notification in notifications)
            {
                notification.State = NotificationState.Read;
            }

            this.data.SaveChanges();

            return Ok(toReturn);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetNotifications()
        {
            return this.GetNotifications(0);
        }

        [Authorize]
        [HttpGet]
        [Route("api/notifications/next")]
        public IHttpActionResult GetNext()
        {

            var userId = this.User.Identity.GetUserId();

            var notification = this.data.Notifications.All()
                                   .Where(n => n.State == NotificationState.Unread)
                                   .OrderByDescending(n => n.DateCreated)
                                   .Select(NotificationsOutputDataModel.ToModel)
                                   .FirstOrDefault();

            if (notification == null)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.NotModified);
                return ResponseMessage(response);
            }

            var toChange = this.data.Notifications.All()
                               .Where(n => n.State == NotificationState.Unread)
                               .OrderBy(n => n.DateCreated).FirstOrDefault();

            toChange.State = NotificationState.Read;

            this.data.SaveChanges();

            return Ok(notification);
        }

        private IQueryable<Notification> GetSortedNotifications(string userId)
        {
            return this.data.Notifications.All()
                       .Where(n => n.Player.Id == userId)
                       .OrderBy(n => n.State)
                       .ThenByDescending(n => n.DateCreated);
        }
    }
}
