namespace Data
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Data.Repositories;
    using Model;
    public class BullsAndCowsData : IBullsAndCowsData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public BullsAndCowsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public BullsAndCowsData()
            : this(new ApplicationDbContext())
        {

        }

        public IRepository<Player> Users
        {
            get
            {
                return GetRepository<Player>();
            }
        }

        public IRepository<Game> Games
        {
            get
            {
                return GetRepository<Game>();
            }
        }

        public IRepository<Guess> Guesses
        {
            get
            {
                return GetRepository<Guess>();
            }
        }

        public IRepository<Notification> Notifications
        {
            get
            {
                return GetRepository<Notification>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }
            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
