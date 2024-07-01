using AutoMapper;
using CenterEnglishManagement.Dto.ModelDto;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IOtherServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAttendanceController:CommonController<StudentAttendance>
    {
        private readonly IStudentAttendanceServices _StudentAttendanceServices;
        private readonly IMapper _Mapper;
        public StudentAttendanceController(IMapper mapper,ICommonServices<StudentAttendance> commonServices,IStudentAttendanceServices studentAttendanceServices):base(commonServices)
        {
            _StudentAttendanceServices = studentAttendanceServices;
            _Mapper = mapper;

        }
        [HttpPost("class")]
        /*[Authorize(Policy = "AdminPolicy")]*/
        public async Task<IActionResult> CreateStudentAttendanceByClass(List<StudentAttendanceDto> studentAttendanceDtos) {
            foreach (var attendanceDto in studentAttendanceDtos)
            {
                var studentAttendance = _Mapper.Map<StudentAttendance>(attendanceDto);
                 await _StudentAttendanceServices.CreateAsync(studentAttendance);
            }
            return Ok();

        }


        [HttpGet("monthly")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> GetMonthlyStudent()
        {
            var entity=await _StudentAttendanceServices.GetMonthlyStudentAttendanceAsync();
            return Ok(entity);
        }
        [HttpGet("quarterly")]
        /*[Authorize(Policy = "StudentPolicy")]*/
        public async Task<IActionResult> GetQuarterlyStudent()
        {

            var entity =await _StudentAttendanceServices.GetQuarterlyStudentAttendanceAsync();
            return Ok(entity);
        }
        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearlyStudent()
        {
            var entity =await _StudentAttendanceServices.GetYearlyStudentAttendanceAsync();
            return Ok(entity);
        }
    }
}
