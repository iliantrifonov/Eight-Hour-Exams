namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Game
    {
        public Game()
        {
            this.RedGuesses = new HashSet<Guess>();
            this.BlueGuesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        public virtual Player Blue { get; set; }

        [Required]
        public virtual Player Red { get; set; }

        [Required]
        public GameState GameState { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Guess> RedGuesses { get; set; }

        public virtual ICollection<Guess> BlueGuesses { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        public string RedNumber { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        public string BlueNumber { get; set; }
    }
}
