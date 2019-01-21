﻿using System.Collections.Generic;
using POS.Models;

namespace POS.Interfaces
{
    public interface IProductService
    {
        List<Product> Search(string eanCode);
        List<Product> GetAll();
        Product Get(int productId);
        Product Get(string ean);
    }
}
