namespace Data
{
    using System;
    using System.Linq;

    using Data.Repositories;
    using Model;

    public interface IBullsAndCowsData
    {
        IRepository<Player> Users { get; }

        IRepository<Game> Games { get; }

        IRepository<Guess> Guesses { get; }

        IRepository<Notification> Notifications { get; }
        
        int SaveChanges();
    }
}
