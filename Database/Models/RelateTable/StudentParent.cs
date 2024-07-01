using CenterEnglishManagement.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.RelateTable
{
    public class StudentParent
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public User? Student { get; set; }
        public User? Parent { get; set; }
    }
}
