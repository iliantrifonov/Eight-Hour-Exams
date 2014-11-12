using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public virtual Article Article { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

    }
}