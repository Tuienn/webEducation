using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using AutoMapper;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController:CommonController<Payment>
    {
        private readonly IPaymentServices _services;
        private readonly IMapper _mapper;
        public PaymentController(IMapper mapper, ICommonServices<Payment> commonServices, IPaymentServices services):base(commonServices)
        {
            _services = services;
            _mapper = mapper;
        }
        [HttpGet("total")]
        public IActionResult TotalPayments()
        {
            var entity = _services.CalculateTotalPayments();
            return Ok(entity);
        }
        [HttpGet("total/{idStudent}")]

        public IActionResult PaymentsForStudent(int idStudent)
        {
            var entity = _services.CalculateTotalPaymentsForStudent(idStudent);
            return Ok(entity);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePaymentByParent([FromBody]List<PaymentDto> paymentDtos)
        {
            foreach (var paymentDto in paymentDtos)
            {
                var payment=_mapper.Map<Payment>(paymentDto);
                await _services.CreateAsync(payment);
            }
            return Ok("success");
        }
    }
}
