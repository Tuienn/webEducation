using CenterEnglishManagement.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.OtherModels
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int  Amount { get; set; }
        public int StudentId {  get; set; }
        public User? Student { get; set; }
    }
}
