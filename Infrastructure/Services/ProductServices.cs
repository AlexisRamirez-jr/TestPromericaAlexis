using Core.CustomClass;
using Core.Entities;
using Core.Interfaces.CustomOperation;
using Infrastructure.Data;
using Infrastructure.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class ProductServices : IProductServices
    {
        #region ATTRIBUTES
        private readonly PersistenceContext _persistenceContext;
        #endregion

        #region CONSTRUCTOR
        public ProductServices(PersistenceContext persistenceContext)
        {
            _persistenceContext = persistenceContext;
        }
        #endregion

        #region METHODS

        public async Task<IOperationResult<IEnumerable<Product>>> GetProducts(string rol)
        {
            var productsall = _persistenceContext.Products.Where(sig => sig.Rol == rol);
            List<Product>  products = new();
           // IEnumerable<Product> newList = null;

            foreach (var items in products)
            {
                products.Add(new Product
                {
                    Id = items.Id,
                    Rol = items.Rol,
                    ProductName = items.ProductName
                });
            }
            return BasicOperationResult<IEnumerable<Product>>.Ok(products);
        }
        #endregion
    }
}
