using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using POS.Services;
using POS.Models;
using System.ComponentModel;
using System.Linq;

namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("ean/search/{eanCode}")]
        [Description("Find product by EAN code")]
        public ActionResult<List<Product>> SearchProduct(string eanCode)
        {
            var result = _productService.Search(eanCode);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("ean/{eanCode}")]
        [Description("Get product by EAN code")]
        public ActionResult<Product> GetProduct(string eanCode)
        {
            var result = _productService.Search(eanCode);
            if (result == null)
                return NotFound();
            else
                return Ok(result.FirstOrDefault());
        }

        [HttpGet("{productId}")]
        [Description("Find product by Id")]
        public ActionResult<Product> GetProduct(int productId)
        {
            var result = _productService.Get(productId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Description("Get all products")]
        public ActionResult<List<Product>> GetAll()
        {
            return Ok(_productService.GetAll());
        }


    }
}