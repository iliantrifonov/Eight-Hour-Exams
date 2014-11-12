namespace WCF.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class UserDetailedModel
    {
        public string Id { get; set; }

        public int Losses { get; set; }

        public int Rank { get; set; }

        public string Username { get; set; }

        public int Wins { get; set; }
    }
}