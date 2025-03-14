using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MaratonRicardoNogales.Models;
using MaratonRicardoNogales.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MaratonRicardoNogales.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryTorneo repo;

        public ManagedController(RepositoryTorneo repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Login(string email, string password)
        {
            var usuario = await this.repo.LoginAsync(email, password);
            if(usuario != null)
            {
                HttpContext.Session.SetInt32("Id", usuario.Id);
                HttpContext.Session.SetString("Nombre", usuario.Nombre);
                HttpContext.Session.SetString("Email", usuario.Email);
                HttpContext.Session.SetString("Password", usuario.Password);
                HttpContext.Session.SetString("Rol", usuario.Rol);

                return RedirectToAction("Index", "Torneos");
            }
            else
            {
                ViewData["MENSAJE"] = "Datos incorrectos";
                return View();
            }
        }

        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Torneos");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}