using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.OtherServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController:CommonController<Class>
    {
        private readonly IClassServices _services;
        public ClassController(ICommonServices<Class> commonServices,IClassServices services):base(commonServices)
        {
            _services = services;
        }

        /*[HttpGet("classScheduleTuition/{classId}")]
        public async Task<IActionResult> FindClassById(int classId)
        { 
            _services.F
        
        
        }*/
            [HttpPost("classScheduleTuition")]
        public async Task<IActionResult> CreateClass([FromBody] ClassDto classDto)
        {


            if (classDto == null)
            {
                return BadRequest();
            }
            bool classNameExists = await _services.ClassNameExistsAsync(classDto.ClassName, classDto.Grade, classDto.Year);
            if (classNameExists)
            {
                return Conflict("A class with the same name already exists for the specified grade and year.");
            }
            var newClass = new Class
            {
                IsActive = classDto.IsActive,
                UrlImg= classDto.UrlImg,
                Grade = classDto.Grade,
                Year = classDto.Year,
                ClassName = classDto.ClassName,
                TeacherId = classDto.TeacherId,
            };
            var createdClass = await _services.CreateAsync(newClass);
            var tuitionFee = new TuitionFee
            {
                Amount = classDto.TuitionFeeAmount,
                ClassId = createdClass.Id
            };
            DateTime date = DateTime.SpecifyKind(DateTime.ParseExact(classDto.ScheduleDto.StartTime, "yyyy-MM-dd", null), DateTimeKind.Utc);
            await _services.CreateTuitionFeeAsync(tuitionFee);
            var schedule = new Schedule
            {
                DateType = classDto.ScheduleDto.DateType,
                Shift = classDto.ScheduleDto.Shift,
                StartTime = date,
                NumOfSession = classDto.ScheduleDto.NumOfSession,
                ClassId = createdClass.Id
            };
            await _services.CreateScheduleAsync(schedule);
            return Ok(classDto);
        }

        [HttpGet("{id}/teacher")]
        public async Task<IActionResult> GetTeacherByClassId(int id)
        {
            var teacherDto = await _services.GetTeacherByClassIdAsync(id);

            if (teacherDto == null)
            {
                return NotFound();
            }

            return Ok(teacherDto);
        }
        [HttpGet("Grade")]
        public async Task<IActionResult> GetClassesByGrade(string grade)
        {
            var classes=await _services.FindClassByGradeAsync(grade);
            return Ok(classes);
        }
        [HttpGet("Grade/Year")]
        public async Task<IActionResult> GetClassesByYear(string grade,int year)
        {
            var classes = await _services.FindClassByYearAsync(grade,year);
            return Ok(classes);
        }
        [HttpGet("Grade/Year/Name")]
        public async Task<IActionResult> GetClassesByName(string grade, int year,string name)
        {
            var classes = await _services.FindClassByNameAsync(grade, year,name);
            return Ok(classes);
        }
        [HttpGet("Grade/Name")]
        public async Task<IActionResult> GetYearsByName(string grade, int year, string name)
        {
            var years = await _services.FindYearByNameAsync(grade, name);
            return Ok(years);
        }
        [HttpGet("{classId}/schedule")]
        public async Task<IActionResult> GetScheduleByIdClasss(int classId)
        {
            var schedule= await _services.FindSchedule(classId);
            return Ok(schedule);
        }
            
    }
}
