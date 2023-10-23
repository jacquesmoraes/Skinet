
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _products;
        private readonly IGenericRepository<ProductBrand> _prodBrands;
        private readonly IGenericRepository<ProductType> _prodTypes;

        public ProductsController(IGenericRepository<Product> products,
            IGenericRepository<ProductBrand> prodBrands,
            IGenericRepository<ProductType> prodTypes)
        {
            _products = products;
            _prodBrands = prodBrands;
            _prodTypes = prodTypes;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            return Ok(await _products.ListAsync(spec));
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _products.GetByIdAsync(id);
           
        }

        [HttpGet("brands")]
        
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
           return Ok(await _prodBrands.ListAllAsync());
        }
        
        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _prodBrands.ListAllAsync());
          
        }
    }
}
