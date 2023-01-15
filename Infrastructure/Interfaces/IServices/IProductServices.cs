using Core.Entities;
using Core.Interfaces.CustomOperation;

namespace Infrastructure.Interfaces.IServices
{
    public interface IProductServices
    {
        Task<IOperationResult<IEnumerable<Product>>> GetProducts(string rol);
    }
}
