using Core.Entities;
using Core.Interfaces.CustomOperation;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiProductos.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        #region ATTRIBUTES
        private readonly IConfiguration _config;
        private readonly IProductRepository _iProductRepository;
        #endregion

        #region CONSTRUCTOR
        public ProductsController(
            IConfiguration config,
            IProductRepository iProductRepository)
        {
            _config = config;
            _iProductRepository = iProductRepository;
        }

        #endregion

        #region METHODS

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Products")]
        public async Task<IActionResult> GetProductoXRol([FromQuery] string rol)
        {
            // ProductosxRol
            IOperationResult<IEnumerable<Product>> result = await _iProductRepository.ProductosxRol(rol);

            if (!result.Success)
            {
                return BadRequest($"Hubo error con la autenticación. {result.Message}");
            }

            //ObtenerRolesUsuario
 
            return Ok(result.Entity);
        }

        #endregion
    }
}