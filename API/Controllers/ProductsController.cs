using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this will be products";
        }
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "this will be a product";
        }

    }
}
