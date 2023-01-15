using Core.CustomClass;
using Core.Entities;
using Core.Interfaces.CustomOperation;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region ATTRIBUTES
        private readonly IProductServices _iProductServices;
        #endregion

        #region CONSTRUCTOR
        public ProductRepository(IProductServices iProductServices)
        {
            _iProductServices = iProductServices;
        }
        #endregion

        #region METHODS

        public async Task<IOperationResult<IEnumerable<Product>>> ProductosxRol(string rol)
        {
            IOperationResult<IEnumerable<Product>> products = await _iProductServices.GetProducts(rol);
            if (products == null)
                return BasicOperationResult<IEnumerable<Product>>.Fail("Rol no encontrado");
            return BasicOperationResult<IEnumerable<Product>>.Ok(products.Entity);

        }
        #endregion
    }
}
