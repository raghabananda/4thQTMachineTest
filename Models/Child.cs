using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDemo.Models
{
    public class Child
    {
        [Key]
       
        public int ChildID { get; set; }
        public string ChildName { get; set; }
        public DateTime Dob { get; set; }
        
        public int ParentID { get; set; }
        [ForeignKey("ParentID")]
        public Parent parent { get; set; }
    }
}
