using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.UserModels;
using Microsoft.AspNetCore.Identity;

namespace CenterEnglishManagement.Service.IService.IOtherServices
{
    public interface IClassServices: ICommonServices<Class>
    {
        Task<TuitionFee> CreateTuitionFeeAsync(TuitionFee tuitionFee);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task<bool> ClassNameExistsAsync(string className, string grade, int year);
        Task<UserDto> GetTeacherByClassIdAsync(int classId);
        Task<IEnumerable<Schedule>> FindSchedule(int classId);
        Task<IEnumerable<Class>> FindClassByGradeAsync(string grade);
        Task<IEnumerable<Class>> FindClassByYearAsync(string grade, int year);
        Task<IEnumerable<Class>> FindClassByNameAsync(string grade, int year, string name);
        Task<IEnumerable<int>> FindYearByNameAsync(string grade, string name);

    }
}
