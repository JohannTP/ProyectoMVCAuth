using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectMVCAuth.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class OpcionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Administrador, Empleado, Usuario")]
        public IActionResult Products()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Ventas()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Manage()
        {
            return View();
        }
    }
}
