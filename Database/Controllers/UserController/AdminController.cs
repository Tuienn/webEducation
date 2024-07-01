
using CenterEnglishManagement.Service.IService.IUserServices;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController:ControllerBase
    {

        private readonly IUserService _userServices;
        public AdminController( IUserService adminServices)
        {
            _userServices = adminServices;
        }
       
    }
}
