﻿using System.Collections.Generic;
using POS.Models;

namespace POS.Services
{
    public interface IProductRepository
    {
        List<Product> Search(string eanCode);
        List<Product> GetAll();
        Product Get(int productId);
        Product GetByEan(string ean);
    }
}