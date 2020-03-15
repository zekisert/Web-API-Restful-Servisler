using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.DataAccess;
using WebApiDemo.Entities;

namespace WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);

                if (product == null)
                {
                    return NotFound($"There is no product Id = {productId}");
                }
                return Ok(product);
            }
            catch { }

            return BadRequest();
            
            
        }

        public IActionResult Post(Product product)
        {
            try
            {
            
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch 
            {

                
            }
            return BadRequest();
        }
    }
}
