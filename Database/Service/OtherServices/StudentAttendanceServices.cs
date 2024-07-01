using CenterEnglishManagement.Context;
using CenterEnglishManagement.Dto.ModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Service.OtherServices
{
    public class StudentAttendanceServices:CommonServices<StudentAttendance>,IStudentAttendanceServices
    {
        private readonly ApplicationDbContext _context;
        public StudentAttendanceServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<List<MonthlyStudentStatisticDto>> GetMonthlyStudentAttendanceAsync()
        {
        

            var monthlyStatistics =await _context.StudentAttendances
                .GroupBy(a => new { a.Date.Year, a.Date.Month })
                .Select(g => new MonthlyStudentStatisticDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    StudentCount = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            return monthlyStatistics;
        }
        public async Task<List<QuarterlyStudentStatisticDto>> GetQuarterlyStudentAttendanceAsync()
        {
            var quarterlyStatistics =await _context.StudentAttendances
                .GroupBy(a => new
                {
                    a.Date.Year,
                    Quarter = ((a.Date.Month - 1) / 3) + 1
                })
                .Select(g => new QuarterlyStudentStatisticDto
                {
                    Year = g.Key.Year,
                    Quarter = g.Key.Quarter,
                    StudentCount = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            return quarterlyStatistics;
        }

        public async Task<List<YearlyStudentStatisticDto>> GetYearlyStudentAttendanceAsync()
        {
            var yearlyStatistics =await _context.StudentAttendances
                .GroupBy(a => a.Date.Year)
                .Select(g => new YearlyStudentStatisticDto
                {
                    Year = g.Key,
                    StudentCount = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            return yearlyStatistics;
        }

        /*public async Task*/
    }
}
