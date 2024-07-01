using CenterEnglishManagement.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.OtherModels
{
    public class StudentAttendance
    {
        [Key]
        public int Id { get; set; }
        public bool IsPresent {  get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
