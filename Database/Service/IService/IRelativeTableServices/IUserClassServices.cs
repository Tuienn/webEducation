using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;

namespace CenterEnglishManagement.Service.IService.IRelativeTableServices
{
    public interface IUserClassServices: ICommonServices<UserClass>
    {
        Task<IEnumerable<Class>> GetClassesByUserIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetUsersByClassIdAsync(int classId);
        Task<bool> RemoveUserFromClassAsync(int classId, int studentId);
    }
}
