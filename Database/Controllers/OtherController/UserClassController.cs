using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IRelativeTableServices;
using CenterEnglishManagement.Service.IService.IUserServices;
using CenterEnglishManagement.Service.RelativeTableServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Controllers.OtherController
{
    public class UserClassController:CommonController<UserClass>
    {
        private readonly IUserClassServices _services;
        public UserClassController(ICommonServices<UserClass> commonServices, IUserClassServices services):base(commonServices)
        {
            _services = services;
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetClassByUserId(int id)
        {
            var entity = await _services.GetClassesByUserIdAsync(id);
            return Ok(entity);
        }
        [HttpGet("class/{id}")]
        public async Task<IActionResult> GetUserByClassId(int id)
        {
            var entity = await _services.GetUsersByClassIdAsync(id);
            return Ok(entity);
        }
        [HttpDelete("class/{classId}/user/{studentId}")]
        public async Task<IActionResult> DeleteUserClass(int classId, int studentId)
        {
            var result = await _services.RemoveUserFromClassAsync(classId, studentId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}
