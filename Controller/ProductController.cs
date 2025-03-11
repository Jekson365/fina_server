using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dto.products;
using server.Interfaces;
using server.Models;

namespace server.Controller
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var product = await _productRepository.CreateAsync(createProductDto);
            return Ok(new { message = "Product created", product });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.UpdateAsync(updateProductDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Product updated", product });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var success = await _productRepository.DeleteAsync(Id);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Product deleted" });
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByGroupId([FromQuery] int? GroupId)
        {
            var products = await _productRepository.GetProductsByGroupIdAsync(GroupId);
            return Ok(products);
        }
    }
}