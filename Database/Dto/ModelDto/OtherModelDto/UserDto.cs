using CenterEnglishManagement.Models.UserModels;

namespace CenterEnglishManagement.Dto.ModelDto.OtherModelDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public int Role {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string? Mobile { get; set; }
        public string? DOB { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
    }
}
