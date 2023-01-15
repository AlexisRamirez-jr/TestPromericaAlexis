using Core.Entities;
using Infrastructure.Data;

namespace ApiUsuarios.Data
{
    public static class DbInitializer
    {        
        public static void AddCustomerData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var _context = scope.ServiceProvider.GetService<PersistenceContext>();

            if (_context.Users.Any())
            {
                return;
            }

            _context.Users.AddRange(
                new User()
                {
                    Id = 1,
                    UserName = "User1",
                    Password = "Clave1",
                    TypeUser = "Principal"
                },
                new User()
                {
                    Id = 2,
                    UserName = "User2",
                    Password = "Clave2",
                    TypeUser = "Delegado"
                }
             );

            _context.SaveChanges();
        }
    }
}
