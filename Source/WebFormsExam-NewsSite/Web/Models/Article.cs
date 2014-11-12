using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Article
    {
        public Article()
        {
            this.Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        // if category or category ID has required field, they I would not be able to seed(without changes) I handle this in the Web form code.
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public DateTime DateCreated { get; set; }
    }
}