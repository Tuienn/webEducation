using CenterEnglishManagement.Extentions;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.RelateTable;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CenterEnglishManagement.Models.UserModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }



        public UserRole Role { get;set; }//int 012
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string? Mobile { get; set; }
        public DateTime? DOB { get; set; }
        public int? Gender { get; set; }//0:Female, 1:male
        public string? Address { get; set; }

        public ICollection<StudentParent>? StudentParents { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<StudentAttendance>? StudentStudentAttendances { get; set; }
        public ICollection<Salary>? Salaries { get; set; }
        public ICollection<UserClass>? UserClasses { get; set; }
        public ICollection<Class>? Classes { get; set; }
    }

}
