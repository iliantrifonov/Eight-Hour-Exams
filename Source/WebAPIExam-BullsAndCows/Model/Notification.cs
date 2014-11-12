namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        [Required]
        public NotificationState State { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
