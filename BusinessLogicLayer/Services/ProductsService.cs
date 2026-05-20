using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
             _productsRepository = productsRepository;
        }
        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }
            
            Product ProductInput = new Product()
            {
                ProductName= productAddRequest.ProductName,
                Category = productAddRequest.Category.ToString(),
                UnitPrice = productAddRequest.UnitPrice,
                QuantityInStock = productAddRequest.QuantityInStock
            };

            Product? addedProduct = await _productsRepository.AddProduct(ProductInput);

            if (addedProduct == null)
            {
                return null;
            }

            ProductResponse? addedProductResponse = new ProductResponse() { 
                ProductID = addedProduct.ProductID,
                ProductName = addedProduct.ProductName,
                Category = Enum.Parse<CategoryOptions>(addedProduct.Category),
                UnitPrice = addedProduct.UnitPrice,
                QuantityInStock= addedProduct.QuantityInStock
            };

            return addedProductResponse;
        }

        public Task<bool> DeleteProduct(Guid productID)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponse?>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
