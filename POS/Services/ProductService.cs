using POS.Interfaces;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product Get(int productId) => productRepository.Get(productId);

        public List<Product> GetAll() => productRepository.GetAll();

        public List<Product> Search(string eanCode) => productRepository.Search(eanCode);
    }
}
