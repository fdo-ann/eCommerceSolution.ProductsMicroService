using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccessLayer.Repository
{
    public class ProductRepository : IProductsRepository
    {
        ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product?> AddProduct(Product product)
        {
           _dbContext.Products.Add(product);
           await _dbContext.SaveChangesAsync();
           return product;
            //return Task.FromResult(product); if not async used
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
           Product? existinProduct = await _dbContext.Products.FirstOrDefaultAsync(temp=>temp.ProductID == productID);
            if (existinProduct == null) {
                return false;
            }
            _dbContext.Products.Remove(existinProduct);
            int affectedRowsCount = await _dbContext.SaveChangesAsync();
            return affectedRowsCount > 0; // equals ot return true
            //return true;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(conditionExpression);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            return await _dbContext.Products.Where(conditionExpression).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(temp=> temp.ProductID== product.ProductID);
            return existingProduct;
        }
    }
}
