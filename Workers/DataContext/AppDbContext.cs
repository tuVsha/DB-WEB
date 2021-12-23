using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workers.Web.Models;

namespace Workers.Web.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Profile> Profiles { get; set;  }
        public DbSet<Models.EmployeeProfile> EmployeeProfiles { get; set; }
    }
}
