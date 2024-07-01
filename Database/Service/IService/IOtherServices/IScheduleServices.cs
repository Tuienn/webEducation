using CenterEnglishManagement.Models.OtherModels;

namespace CenterEnglishManagement.Service.IService.IOtherServices
{
    public interface IScheduleServices:ICommonServices<Schedule>
    {
        Task<List<string>> GetScheduleDatesAsync(int classId);
        Task<List<bool>> GetAttendanceByStudentIdAsync(int studentId);
    }
}
