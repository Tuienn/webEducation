using CenterEnglishManagement.Dto.ModelDto;
using CenterEnglishManagement.Models.OtherModels;

namespace CenterEnglishManagement.Service.IService.IOtherServices
{
    public interface IStudentAttendanceServices: ICommonServices<StudentAttendance>
    {
       Task<List<MonthlyStudentStatisticDto>> GetMonthlyStudentAttendanceAsync();
        Task<List<QuarterlyStudentStatisticDto>> GetQuarterlyStudentAttendanceAsync();
        Task<List<YearlyStudentStatisticDto>> GetYearlyStudentAttendanceAsync();
    }
}
