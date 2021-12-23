using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Web.Models
{
    public class Profile
    {
        public int id { get; set; }
        [StringLength(100)]
        public string name { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [AvailableBirthdate]
        public DateTime birthday { get; set; }
        [StringLength(15)]
        public string phone { get; set; }
        [ForeignKey("employeeId")]
        public int? employeeId { get; set; }
        public Employee employee;
    }

    public class AvailableBirthdate : ValidationAttribute
    {
        private const int old = 65;
        private const int young = 18;
        public string GetErrorMessage() =>
            "Сотрудник не может быть старше 65 или моложе 18";

        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var date = (DateTime)value;
            var difference = ((DateTime.Now - date).TotalDays) / 365.25;
            var profile = (Profile)validationContext.ObjectInstance;
            if(difference > 65 || difference < 18)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
