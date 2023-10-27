
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams ProductParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(ProductParams);
            var countSpec = new ProductWithFilterForCountSpecification(ProductParams);
            var TotalItems = await _products.CountAsync(countSpec);
            var products = await _products.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(ProductParams.PageIndex,
                ProductParams.PageSize, TotalItems, data));
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _products.GetEntityWithSpec(spec);
            if (product == null) { return NotFound(new ApiResponse(404)); }
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
