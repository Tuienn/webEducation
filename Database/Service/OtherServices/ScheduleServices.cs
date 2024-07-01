using CenterEnglishManagement.Context;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Service.OtherServices
{
    public class ScheduleServices:CommonServices<Schedule>,IScheduleServices
    {
        private readonly ApplicationDbContext _context;
        public ScheduleServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<List<string>> GetScheduleDatesAsync(int  studentId)
        {
            int classId=await _context.UserClasses.Where(uc=>uc.UserId==studentId).Select(uc=>uc.Id).FirstOrDefaultAsync();
            var schedule=await _context.Schedules.FirstOrDefaultAsync(s=>s.ClassId==classId);
            var dates = new List<string>();
            DateTime startTime = schedule.StartTime;
            int numsOfsection = schedule.NumOfSession;
            List<string> result = GetClassDays(startTime, numsOfsection);
            return result;
        }
        public static List<string> GetClassDays(DateTime startTime, int NumsOfSection)
        {
            List<string> classDays = new List<string>();
            DayOfWeek[] evenDays = { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday };
            DayOfWeek[] oddDays = { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday };

            DayOfWeek[] scheduleDays = startTime.Day % 2 == 0 ? evenDays : oddDays;

            int sectionsAdded = 0;
            DateTime currentDay = startTime;

            while (sectionsAdded < NumsOfSection)
            {
                if (Array.Exists(scheduleDays, day => day == currentDay.DayOfWeek))
                {
                    classDays.Add(currentDay.ToString("yyyy-MM-dd"));
                    sectionsAdded++;
                }
                currentDay = currentDay.AddDays(1);
            }

            return classDays;
        }
        public async Task<List<bool>> GetAttendanceByStudentIdAsync(int studentId)
        {
            return await _context.StudentAttendances
                           .Where(sa => sa.UserId == studentId)
                           .Select(sa => sa.IsPresent)
                           .ToListAsync();
        }
    }
}
