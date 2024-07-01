using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IOtherServices;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TuitionFeeController:CommonController<TuitionFee>
    {
       private readonly ITuitionFeeServices _service;
       public TuitionFeeController(ICommonServices<TuitionFee> commonServices,ITuitionFeeServices service):base(commonServices)
        {
            _service = service;
        }
        [HttpGet("total")]
        public IActionResult TotalTuitionFees()
        {
            var entity = _service.CalculateTotalTuitionFees();
            return Ok(entity);
        }
        [HttpGet("total/{studentId}")]
        public IActionResult TotalTuitionFeeForUser(int studentId)
        {
            var entity = _service.CalculateTotalTuitionFeeForUser(studentId);
            return Ok(entity);
        }
    }
}
