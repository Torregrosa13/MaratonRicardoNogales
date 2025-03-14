using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MaratonRicardoNogales.Models;
using MaratonRicardoNogales.Repositories;
using System.Security.Claims;

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
            Usuario user = await
                this.repo.LoginAsync(email, password);
            if (user != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                Claim claimEmail =
                    new Claim(ClaimTypes.Name, user.Email);
                identity.AddClaim(claimEmail);
                Claim claimPass =
                    new Claim(ClaimTypes.NameIdentifier
                    , user.Password);
                identity.AddClaim(claimPass);
                Claim claimRol =
                    new Claim(ClaimTypes.Role, user.Rol);
                identity.AddClaim(claimRol);
                if (user.Password == "admin")
                {
                    //CREAMOS UN CLAIM
                    Claim claimAdmin =
                        new Claim("Admin", "Soy el super jefe de la empresa");
                    identity.AddClaim(claimAdmin);
                }

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                string controller =
                    TempData["controller"].ToString();
                string action =
                    TempData["action"].ToString();
                if (TempData["id"] != null)
                {
                    string id =
                        TempData["id"].ToString();
                    return RedirectToAction(action, controller
                        , new { id = id });
                }
                else
                {
                    return RedirectToAction(action, controller);
                }
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}