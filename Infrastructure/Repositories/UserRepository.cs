using Core.CustomClass;
using Core.Entities;
using Core.Interfaces.CustomOperation;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region ATTRIBUTES
        private readonly IUserServices _iUserServices;
        #endregion

        #region CONSTRUCTOR
        public UserRepository(IUserServices iUserServices)
        {
            _iUserServices = iUserServices;
        }
        #endregion

        #region METHODS
        public async Task<IOperationResult<User>> ValidarCredenciales(string username, string password)
        {
            User user = await _iUserServices.LoginUser(username, password);
            if (user == null)
                return BasicOperationResult<User>.Fail("El usuario no fue encontrado");
            return BasicOperationResult<User>.Ok(user);
        }

        public async Task<IOperationResult<string>> ObtenerRolesUsuario(string username)
        {
            User user = await _iUserServices.GetUser(username);
            if (user == null)
                return BasicOperationResult<string>.Fail("El usuario no fue encontrado");
            return BasicOperationResult<string>.Ok(user.TypeUser.ToString());

        }
        #endregion
    }
}
