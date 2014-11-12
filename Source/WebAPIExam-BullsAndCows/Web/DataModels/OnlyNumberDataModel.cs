namespace Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class OnlyNumberDataModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string Number { get; set; }
    }
}