using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Data.Repositories;
using Web.Controllers;
using Model;
using Data;

namespace Web.Tests.Integration
{
    public class DependencyResolver : IDependencyResolver
    {
        private IBullsAndCowsData data;

        public IBullsAndCowsData Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            //add all controllers
            if (serviceType == typeof(ScoresController))
            {
                return new ScoresController(this.Data as IBullsAndCowsData);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {

        }
    }
}
