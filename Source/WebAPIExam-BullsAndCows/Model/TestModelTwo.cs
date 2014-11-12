//namespace Model
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Linq;
//    using System.Text;

//    public class TestModelTwo
//    {
//        public TestModelTwo()
//        {
//            this.CollectionOnes = new HashSet<TestModelOne>();
//        }

//        public int Id { get; set; }

//        [Required]
//        [MaxLength(50)]
//        [Index(IsUnique = true)]
//        public string Name { get; set; }

//        public virtual ICollection<TestModelOne> CollectionOnes { get; set; }
//    }
//}
