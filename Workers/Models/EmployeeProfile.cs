using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Workers.Web.Models
{
    public class EmployeeProfile
    {
        public int id { get; set; }
        public Employee Employee { get; set; }
        public Profile Profile { get; set; }
    }
}
