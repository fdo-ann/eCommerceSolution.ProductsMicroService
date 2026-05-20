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

        public async Task<bool> DeleteProduct(Guid productID)
        {
            Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productID);

            if (existingProduct == null)
            {
                return false;
            }

            //Attempt to delete product
            bool isDeleted = await _productsRepository.DeleteProduct(productID);
            return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            Product? product = await _productsRepository.GetProductByCondition(conditionExpression);
            if (product == null)
            {
                return null;
            }

            ProductResponse productResponse = new ProductResponse()
            {
                ProductID = product.ProductID,  
                ProductName= product.ProductName,
                Category = Enum.Parse<CategoryOptions>(product.Category),
                UnitPrice= product.UnitPrice,
                QuantityInStock = product.QuantityInStock
            };

            return productResponse;
        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
            IEnumerable<Product> products = await _productsRepository.GetProducts();

            //if (products == null)
            //{
            //    return null;
            //}

            List<ProductResponse> productResponseList = products.Select(product=> new ProductResponse()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Category = Enum.Parse<CategoryOptions>(product.Category),
                UnitPrice = product.UnitPrice,
                QuantityInStock = product.QuantityInStock
            }).ToList();
            return productResponseList;
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            IEnumerable<Product?> products = await _productsRepository.GetProductsByCondition(conditionExpression);

            List<ProductResponse> productResponseList = products.Select(product => new ProductResponse()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Category = Enum.Parse<CategoryOptions>(product.Category),
                UnitPrice = product.UnitPrice,
                QuantityInStock = product.QuantityInStock
            }).ToList();
            return productResponseList;

        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);

            if (existingProduct == null)
            {
                throw new ArgumentException("Invalid Product ID");
            }

            //Map from ProductUpdateRequest to Product type
            Product product = new Product()
            {
                ProductName = productUpdateRequest.ProductName,
                Category = productUpdateRequest.Category.ToString(),
                UnitPrice = productUpdateRequest.UnitPrice,
                QuantityInStock = productUpdateRequest.QuantityInStock
            };

            Product? updatedProduct = await _productsRepository.UpdateProduct(product);

            ProductResponse productResponse = new ProductResponse()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Category = Enum.Parse<CategoryOptions>(product.Category),
                UnitPrice = product.UnitPrice,
                QuantityInStock = product.QuantityInStock
            };

            return productResponse;
        }
    }
}
