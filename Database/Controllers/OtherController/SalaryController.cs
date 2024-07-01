using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers.OtherController
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController:CommonController<Salary>
    {
        private readonly ISalaryServices _services;
        public SalaryController(ICommonServices<Salary> commonServices, ISalaryServices services):base(commonServices)
        {
            _services = services;
        }
    }
}
