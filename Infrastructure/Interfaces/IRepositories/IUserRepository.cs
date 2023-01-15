using Core.Entities;
using Core.Interfaces.CustomOperation;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Find user by Name and Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IOperationResult<User>> ValidarCredenciales(string username, string password);

        /// <summary>
        /// Find user by Name and Password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<IOperationResult<string>> ObtenerRolesUsuario(string username);
    }
}