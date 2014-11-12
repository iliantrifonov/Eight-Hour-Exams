using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Web.Models;

namespace Web
{
    public class BasePage : Page
    {
        protected ApplicationDbContext DbContext { get; private set; }

        public BasePage()
        {
            this.DbContext = new ApplicationDbContext();
        }
    }
}