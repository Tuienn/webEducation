using CenterEnglishManagement.Service;
using CenterEnglishManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CenterEnglishManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController<T>: ControllerBase where T : class
    {
        private readonly ICommonServices<T> _commonServices;
        public CommonController(ICommonServices<T> commonServices)
        {
            _commonServices = commonServices;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(T entity)
        {
            await _commonServices.CreateAsync(entity);
            return Ok(entity);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _commonServices.GetbyIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize, string sortBy, bool sortDesc)
        {
            var entities = await _commonServices.GetAllAsync(pageIndex, pageSize, sortBy, sortDesc);
            return Ok(entities);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, T entity)
        {
            await _commonServices.UpdateAsync(id, entity);
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commonServices.DeleteAsync(id);
            return Ok();
        }

    }
}
