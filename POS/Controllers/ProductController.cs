using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using POS.Services;
using POS.Models;
using System.ComponentModel;

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
        [HttpGet("ean/{eanCode}")]
        [Description("Find product by EAN code")]
        public ActionResult<List<Product>> SearchProduct(string eanCode)
        {
            var result = _productService.Search(eanCode);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
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