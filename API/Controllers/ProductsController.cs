
using API.Dtos;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> products,
            IGenericRepository<ProductBrand> prodBrands,
            IGenericRepository<ProductType> prodTypes,
            IMapper mapper)
        {
            _products = products;
            _prodBrands = prodBrands;
            _prodTypes = prodTypes;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _products.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _products.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);
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
