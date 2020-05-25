using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDemo.Models
{
    public class FormPost
    {
        public string FirstName { get; set; }
        public string Email  { get; set; }
        public string ContactNo { get; set; }

        public string[] childNames { get; set; }
        public string[] childDobs { get; set; }
    }
    public class FormUpdate: FormPost
    {
        public string ParentID { get; set; }
        public string[] childIDs { get; set; }
    }
 }
