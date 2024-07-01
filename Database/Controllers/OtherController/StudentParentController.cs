using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Service.IService.IRelativeTableServices;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using System.Data;

namespace CenterEnglishManagement.Controllers.OtherController
{
    public class StudentParentController:CommonController<StudentParent>
    {
        private readonly IStudentParentServices _services;
        private readonly IMapper _mapper;
        public StudentParentController(IMapper mapper,ICommonServices<StudentParent> commonServices, IStudentParentServices services) : base(commonServices)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("child/{studentId}")]
        public async Task<ActionResult> FindParentByStudentId(int studentId)
        {
            var user= await _services.GetParentByStudentIdAsync(studentId);
            if (user==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }
        [HttpGet("parent/{parentId}")]






        public async Task<ActionResult> FindStudentsByParentId(int parentId)
        {
            var users = await _services.GetStudentsByParentIdAsync(parentId);
            if (users == null)
            {
                return NotFound();
            }
            var userDtos = users.Select(u => _mapper.Map<UserDto>(u));
            return Ok(userDtos);
        }
        [HttpGet("payment/{parentId}")]
        public async Task<ActionResult> GetPaymentInforByParentId(int parentId) {
            var PaymentInfors = await _services.GetPaymentInforByParentIdAsync(parentId);
            if (PaymentInfors == null)
            {
                return NotFound();
            }
            return Ok(PaymentInfors);
        }
    }
}
