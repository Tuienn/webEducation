using CenterEnglishManagement.Extentions;

namespace CenterEnglishManagement.Dto.ModelDto.OtherModelDto
{
    public class CreateUserDto
    {
        public int Id { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string? Mobile { get; set; }
        public string? DOB { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
    }
}
