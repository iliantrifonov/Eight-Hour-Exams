namespace Web.Controllers
{
    using Data;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        protected IBullsAndCowsData data;

        public BaseApiController(IBullsAndCowsData data)
        {
            this.data = data;
        }
    }
}
