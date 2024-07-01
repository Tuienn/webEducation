using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.UserModels;

namespace CenterEnglishManagement.Models.RelateTable
{
    public class UserClass
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId {  get; set; }
        public  User? User { get; set; }
        public  Class? Class { get; set; }
    }
}
