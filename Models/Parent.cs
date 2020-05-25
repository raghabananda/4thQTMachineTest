using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDemo.Models
{
    public class Parent
    {
        [Key]
      
        public int ParentID { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }

        
        public ICollection<Child> children { get; set; }
    }
}
