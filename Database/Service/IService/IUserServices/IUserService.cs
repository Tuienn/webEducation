using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Extentions;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.UserModels;

namespace CenterEnglishManagement.Service.IService.IUserServices
{
    public interface IUserService: ICommonServices<User>
    {
        Task<IEnumerable<User>> GetAllStudentAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc);
        Task<IEnumerable<User>> GetAllByRoleAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc, UserRole role);
        Task<User> ValidateUser(string email, string password);
        List<int> PaymentandTuitionTotal();
        Task<IEnumerable<Class>> GetTeacherClassesAsync(int teacherId);
        Task UpdateUserAsync(User user);
    }
}
