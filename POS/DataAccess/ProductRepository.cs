using POS.Services;
using POS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return context.Products.Include(p => p.Tax).SingleOrDefault(p =>  p.Id == productId);
        }

        public List<Product> GetAll()
        {
            return context.Products.Include(p => p.Tax).ToList();
        }

        public Product GetByEan(string ean)
        {
            return context.Products.Include(p => p.Tax).SingleOrDefault(p => p.Ean == ean);
        }

        public List<Product> Search(string eanCode)
        {
            return context.Products.Include(p => p.Tax).Where(p => p.Ean == eanCode).ToList();
        }
    }
}
