using Microsoft.AspNetCore.Mvc;
using ProjectMVCAuth.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.VisualBasic;

namespace ProjectMVCAuth.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        public LoginController(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            var usuarioExiste = await _usuarioRepository.FindUser(correo, clave);
            if (usuarioExiste != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioExiste.Nombres),
                    new Claim(ClaimTypes.Email, usuarioExiste.Correo)
                };

                var usuarioRoles = await _rolRepository.GetRolUser(usuarioExiste.Id);
                var roles = usuarioRoles.Select(x => x.Descripcion).ToList();
                foreach (string rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index","Login");



        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
