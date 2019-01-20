using POS.Interfaces;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly PosDbContext context;

        public ProductRepository(PosDbContext context)
        {
            this.context = context;
        }

        public Product Get(int productId)
        {
            return context.Products.Find(productId);
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public List<Product> Search(string eanCode)
        {
            return context.Products.Where(p => p.Ean == eanCode).ToList();
        }
    }
}
