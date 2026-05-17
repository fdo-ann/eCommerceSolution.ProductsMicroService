using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccessLayer.RepositoryContracts
{
    public interface IProductsRepository
    {
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
        Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

        Task<Product?> AddProduct(Product product);

        Task<Product?> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productID);

    }
}
