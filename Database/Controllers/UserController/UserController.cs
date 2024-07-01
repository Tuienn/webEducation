using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service.IService.IUserServices;
using CenterEnglishManagement.Extentions;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using AutoMapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using CenterEnglishManagement.Service;

namespace CenterEnglishManagement.Controllers.UserController
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class UserController : CommonController<User>
        {
            private readonly IUserService _services;
            private readonly IMapper _mapper;
            public UserController(ICommonServices<User> commonServices, IUserService service, IMapper mapper) : base(commonServices)
            {
                _services = service;
            _mapper = mapper;
            }
        [HttpGet("role")]
        public async Task<IActionResult> GetAllByRole(int pageIndex, int pageSize, string sortBy, bool sortDesc, UserRole role)
        {
            var entities = await _services.GetAllByRoleAsync(pageIndex, pageSize, sortBy, sortDesc, role);
            var userDtos = entities.Select(u => _mapper.Map<UserDto>(u));
            return Ok(userDtos);
        }
        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetUserByRoleAndId(int id,UserRole role)
        {
            var user = await _services.GetbyIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Role != role)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }
        [HttpPost("role")]
        public async Task<IActionResult> CreateUserByRole([FromBody] CreateUserDto createUserDto)
        {
            var user = new User
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                Mobile = createUserDto.Mobile,
                DOB = string.IsNullOrEmpty(createUserDto.DOB) ? (DateTime?)null : DateTime.SpecifyKind(DateTime.ParseExact(createUserDto.DOB, "yyyy-MM-dd", null), DateTimeKind.Utc),
                Gender = createUserDto.Gender,
                IsActive = true,
                Role = createUserDto.Role,
                Address = createUserDto.Address,
            };
            await _services.CreateAsync(user);
            return Ok(user);
        }
        [HttpPut("role/{userId}")]
        public async Task<IActionResult> EditUserByRole([FromBody] EditUserRoleDto userDto,int userId)
        {
            User user= await _services.GetbyIdAsync(userId);
            user.Name = userDto.Name;
            user.Mobile = userDto.Mobile;
            user.DOB = string.IsNullOrEmpty(userDto.DOB) ? (DateTime?)null : DateTime.SpecifyKind(DateTime.ParseExact(userDto.DOB, "yyyy-MM-dd", null), DateTimeKind.Utc);
            user.Gender = userDto.Gender;
            await _services.UpdateUserAsync(user);
            return Ok(user);
        }
        [HttpGet("totalPayment")]
        public IActionResult GetPaymentandTuitionTotal() { 
            return Ok(_services.PaymentandTuitionTotal());
        }
        [HttpGet("teacher/{idTeacher}/classes")]
        public async Task<IActionResult> GetClassesbybyTeacherId(int idTeacher)
        {
            var classes = await _services.GetTeacherClassesAsync(idTeacher);
            return Ok(classes);
        }
        private static List<InfinityUserIdDto> _infinityStudentIds = new List<InfinityUserIdDto>();
        private static List<InfinityUserIdDto> _infinityParentIds = new List<InfinityUserIdDto>();
        private static List<InfinityUserIdDto> _infinityTeacherIds = new List<InfinityUserIdDto>();
        [HttpGet("student/infiniteId")]
        public IActionResult GetStudentInfiniteId()
        {
            return Ok(_infinityStudentIds);
        }
        [HttpPost("student/infiniteId")]
        public IActionResult PostStudentInfiniteId([FromBody] InfinityUserIdDto user)
        {
             _infinityStudentIds.Add(new InfinityUserIdDto
             {
                 Id = _infinityStudentIds.Count+1,
                UserId = user.UserId
             });
            return Ok(_infinityStudentIds);
        }


        [HttpGet("parent/infiniteId")]
        public IActionResult GetparentInfiniteId()
        {
            return Ok(_infinityParentIds);
        }
        [HttpPost("parent/infiniteId")]
        public IActionResult PostparentInfiniteId([FromBody] InfinityUserIdDto user)
        {
            _infinityParentIds.Add(new InfinityUserIdDto
            {
                Id = _infinityParentIds.Count + 1,
                UserId = user.UserId
            });
            return Ok(_infinityParentIds);
        }


        [HttpGet("teacher/infiniteId")]
        public IActionResult GetTeacherInfiniteId()
        {
            return Ok(_infinityTeacherIds);
        }
        [HttpPost("teacher/infiniteId")]
        public IActionResult PostTeacherInfiniteId([FromBody] InfinityUserIdDto user)
        {
            _infinityTeacherIds.Add(new InfinityUserIdDto
            {
                Id = _infinityTeacherIds.Count + 1,
                UserId = user.UserId
            });
            return Ok(_infinityTeacherIds);
        }
    }
    
}
