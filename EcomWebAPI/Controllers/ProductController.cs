using EcomWebAPI.Models;
using EcomWebAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EcomWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productservice;
        public ProductController(ILogger<ProductController> logger, IProductService productservice)
        {
            _logger = logger;
            _productservice = productservice;

        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var lstproduct = await _productservice.GetAll();
            return Ok(lstproduct);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productservice.Create(product);
                return Ok(product);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Customer")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productservice.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if(id != product.Id)
                return BadRequest();
            await _productservice.Update(product);
            return Ok(product);

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productservice.GetProductById(id);
            if (product == null)
                return BadRequest();
            await _productservice.Delete(product);
            return Ok("Delete Success!");

        }

    }
}
