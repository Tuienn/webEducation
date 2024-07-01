using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.OtherModels
{
    public class Class
    {
        [Key] 
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string? UrlImg {  get; set; }
        public string Grade { get; set; }
        //Grade: 1,2,3
        public int Year { get; set; }
        public string ClassName { get; set; }
        public int TeacherId { get; set; }

        public User? Teacher { get; set; }

        public ICollection<UserClass>? UserClasses { get; set; }
        public TuitionFee? TuitionFee { get; set; } 
        public Schedule? Schedule { get; set; }
    }
}
