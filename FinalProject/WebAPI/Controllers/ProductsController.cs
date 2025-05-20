using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() 
        {
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            } 
            return BadRequest(result.Message);

            //return new List<Product> {
            //    new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
            //    new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
            //    new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
            //    new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
            //    new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 }
            //};
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

          
        }
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);


        }

    }
}
