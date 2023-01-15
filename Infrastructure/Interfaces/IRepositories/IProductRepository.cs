using Core.Entities;
using Core.Interfaces.CustomOperation;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<IOperationResult<IEnumerable<Product>>> ProductosxRol(string rol);
    }
}
