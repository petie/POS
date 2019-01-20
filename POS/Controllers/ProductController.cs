using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Interfaces;
using POS.Models;

namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        public ActionResult<List<Product>> SearchProduct(string eanCode)
        {
            return Ok(_productService.Search(eanCode));
        }

        public ActionResult<Product> GetProduct(int productId)
        {
            return Ok(_productService.Get(productId));
        }

        public ActionResult<List<Product>> GetAll()
        {
            return Ok(_productService.GetAll());
        }


    }
}