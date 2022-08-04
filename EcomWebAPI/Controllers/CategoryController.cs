using EcomWebAPI.Models;
using EcomWebAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EcomWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;

        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var lstproduct = await _categoryService.GetCategoryList();
            return Ok(lstproduct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var catgogry = await _categoryService.GetCategoryById(id);
            if (catgogry == null)
            {
                return NotFound();
            }
            return Ok(catgogry);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateProduct(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(category);
                return Ok(category);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateProduct(int id,Category category)
        {
            if (id != category.Id)
                return BadRequest();
            await _categoryService.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var catgogry = await _categoryService.GetCategoryById(id);
            if (catgogry == null)
                return BadRequest();
            await _categoryService.Delete(catgogry);
            return Ok("Delete Success!");

        }

    }
}
