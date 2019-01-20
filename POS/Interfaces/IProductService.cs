using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Interfaces
{
    public interface IProductService
    {
        List<Product> Search(string eanCode);
        List<Product> GetAll();
        Product Get(int productId);
    }
}
