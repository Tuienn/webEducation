using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController: CommonController<Schedule>
    {
        private readonly IScheduleServices _services;
        public ScheduleController(ICommonServices<Schedule> commonServices, IScheduleServices services):base(commonServices)
        {
            _services = services;
        }
        [HttpGet("{studentId}/dates")]
        public async Task<IActionResult> GetScheduleDates(int studentId)
        {

            var schedule= await _services.GetScheduleDatesAsync(studentId);
            var attendance=await _services.GetAttendanceByStudentIdAsync(studentId);
            return Ok(new 
            {
                schedule, attendance
            });
        }
    }
}
