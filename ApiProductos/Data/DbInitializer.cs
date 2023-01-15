using Core.Entities;
using Infrastructure.Data;

namespace ApiProductos.Data
{
    public static class DbInitializer
    {        
        public static void AddCustomerData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var _context = scope.ServiceProvider.GetService<PersistenceContext>();

            if (_context.Products.Any())
            {
                return;
            }

            _context.Products.AddRange(
                new Product()
                {
                    Id = 1,
                    Rol = "Principal",
                    ProductName = "Prod_A"
                },
                new Product()
                {
                    Id = 2,
                    Rol = "Principal",
                    ProductName = "Prod_B"
                },               
                new Product()
                {
                    Id = 3,
                    Rol = "Principal",
                    ProductName = "Prod_C"
                },
                new Product()
                {
                    Id = 4,
                    Rol = "Delegado",
                    ProductName = "Prod_A"
                },
                new Product()
                {
                    Id = 5,
                    Rol = "Delegado",
                    ProductName = "Prod_C"
                }
             );

            _context.SaveChanges();
        }
    }
}
