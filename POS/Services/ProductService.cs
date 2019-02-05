using POS.Models;
using System.Collections.Generic;

namespace POS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product Get(int productId) => productRepository.Get(productId);

        public Product Get(string ean) => productRepository.GetByEan(ean);

        public List<Product> GetAll() => productRepository.GetAll();

        public List<Product> Search(string eanCode) => productRepository.Search(eanCode);
    }
}
