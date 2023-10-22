
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var list = await _productRepository.GetProductsAsync();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
           
        }

        [HttpGet("brands")]
        
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
           var brands = await _productRepository.GetProductsBrandsAsync();
            return Ok(brands);
        }
        
        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productRepository.GetProductsTypesAsync());
          
        }
    }
}
