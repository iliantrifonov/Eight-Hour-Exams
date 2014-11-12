namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class Guess
    {
        public int Id { get; set; }

        public virtual Player Player { get; set; }

        public Game Game { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}
