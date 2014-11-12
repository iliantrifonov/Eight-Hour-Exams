namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Model;
    using Data.Migrations;

    public class ApplicationDbContext : IdentityDbContext<Player>
    {
        public ApplicationDbContext()
            : base("ApplicationDatabase", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());     
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Guess> Guesses { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }
    }
}
