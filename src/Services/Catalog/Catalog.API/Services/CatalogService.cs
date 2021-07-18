using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogService> _logger;
        public CatalogService(IProductRepository productRepository, ILogger<CatalogService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task CreateProduct(Product product)
        {
            await _productRepository.CreateProduct(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await _productRepository.GetProductByCategory(category);
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _productRepository.GetProductByName(name);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }
    }
}
