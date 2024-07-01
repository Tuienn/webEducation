using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;

namespace CenterEnglishManagement.Service.IService.IRelativeTableServices
{
    public interface IStudentParentServices:ICommonServices<StudentParent>
    {
        Task<User> GetParentByStudentIdAsync(int studentId);
       Task<IEnumerable<User>> GetStudentsByParentIdAsync(int parentId);
        Task<IEnumerable<TuitionPaymentDto>> GetPaymentInforByParentIdAsync(int parentId);
    }
}
