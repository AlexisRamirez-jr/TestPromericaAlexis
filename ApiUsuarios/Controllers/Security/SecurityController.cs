using Core.CustomEntities;
using Core.DTOS;
using Core.Entities;
using Core.Interfaces.CustomOperation;
using Infrastructure.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiUsuarios.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        #region ATTRIBUTES
        private readonly IConfiguration _config;
        private readonly IUserRepository _iUserRepository;
        #endregion

        #region CONSTRUCTOR
        public SecurityController(
            IConfiguration config,
            IUserRepository iUserRepository)
        {
            _config = config;
            _iUserRepository = iUserRepository;
        }

        #endregion

        #region METHODS

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto loginModel)
        {
            // ValidarCredenciales
            IOperationResult<User> result = await _iUserRepository.ValidarCredenciales(loginModel.UserName, loginModel.Password);

            if (!result.Success)
            {
                return BadRequest($"Hubo error con la autenticación. {result.Message}");
            }

            //ObtenerRolesUsuario
            IOperationResult<string> UserType = await _iUserRepository.ObtenerRolesUsuario(loginModel.UserName);

            string tokenString = BuildUserToken(result.Entity);

            return Ok(new
            {
                Code = 1,
                Data = tokenString
            });
        }

        private string BuildUserToken(User profile)
        {
            DateTime expirationDate = DateTime.Now.AddMinutes(12);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, profile.UserName),
                new Claim("Id", profile.Id.ToString()),
                new Claim(ClaimTypes.Role, profile.TypeUser)
                // Add some values
                //new Claim("nameFild", Convert.ToBase64String(Encoding.UTF8.GetBytes(profile.xxxxxx)))
            };

            string tokenUrl = _config.GetSection("Token:Url").Value;

            var token = new JwtSecurityToken(tokenUrl,
                tokenUrl,
                expires: expirationDate,
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
