using Workers.Web.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Workers.Web.Models
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Employees.Any() && context.Profiles.Any())
                {
                    return; 
                }

                var emp1 = new Employee()
                {
                    id = 1,
                    post = "Руководитель",
                    devision = "Отдел продаж",
                    salary = 200000,
                    profileId = 1
                };
                var prof1 = new Profile()
                {
                    id = 1,
                    name = "Иванов Иван Иванович",
                    email = "ivan@gmail.com",
                    birthday = new DateTime(1991, 12, 1),
                    phone = "3432030",
                    employeeId = 1
                };
                var emp2 = new Employee()
                {
                    id = 2,
                    post = "Ведущий разработчик",
                    devision = "Отдел разработки",
                    salary = 180000,
                    profileId = 2
                };
                var prof2 = new Profile()
                {
                    id = 2,
                    name = "Петров Петр Петрович",
                    email = "petr@gmail.com",
                    birthday = new DateTime(1993, 10, 21),
                    phone = "3432131",
                    employeeId = 2
                };
                emp1.profile = prof1;
                emp2.profile = prof2;
                prof1.employee = emp1;
                prof2.employee = emp2;

                context.Employees.AddRange(emp1, emp2);
                context.Profiles.AddRange(prof1, prof2);

                context.EmployeeProfiles.AddRange(new EmployeeProfile() { id = 1, Employee = emp1, Profile = prof1 },
                                                    new EmployeeProfile() { id = 2, Employee = emp2, Profile = prof2 });
                context.SaveChanges();
            }
        }
    }
}
