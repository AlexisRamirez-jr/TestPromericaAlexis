using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebTest.Extensions;
using WebTest.Models;
using System.IdentityModel.Tokens.Jwt;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<
            HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View("Index");

            Resultado resultLogin = await ServiceExtension.ExcuteAPI<Resultado>(_httpClientFactory, "API", "api/Security/login", ServiceExtension.POST, model);
            if (resultLogin.Data != null)
            {
                //HttpContext.Session.SetString(Sesiones.SessionKeyUserName, "Lucia Lopez");
                var jwt = resultLogin.Data.ToString();
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                HttpContext.Session.SetString("IdUser", token.Claims.First(c => c.Type == "Id").Value);
                HttpContext.Session.SetString("TypeUser", token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value);
                IList<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Productos");
            }
            else
            {
                ViewBag.Error = "El usuario o contraseña es incorrecto";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}