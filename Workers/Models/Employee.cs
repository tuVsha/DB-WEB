using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Web.Models
{
    public class Employee
    {
        public int id { get; set; }
        [StringLength(100)]
        public string post { get; set; }
        [StringLength(150)]
        public string devision { get; set; }
        [Range(1,200000)]
        public int salary { get; set; }
        [ForeignKey("profileId")]
        public int? profileId { get; set; } 
        public Profile profile;
    }
}
