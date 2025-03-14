using Microsoft.AspNetCore.Mvc;
using MaratonRicardoNogales.Models;
using MaratonRicardoNogales.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MaratonRicardoNogales.Controllers
{
    public class TorneosController : Controller
    {
        private RepositoryTorneo repo;
        public TorneosController(RepositoryTorneo repo) 
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Partido> partidos = await this.repo.GetPartidosAsync();
            return View(partidos);
        }

        public async Task<IActionResult> Equipos()
        {
            List<Equipo> equipos = await this.repo.GetEquiposAsync();
            return View(equipos);
        }

        public async Task<IActionResult> Plantilla(int idEquipo)
        {
            List<Jugador> plantilla = await this.repo.GetPlantillaEquiposAsync(idEquipo);
            if(plantilla.Any())
            {
                var nombreEquipo = plantilla.FirstOrDefault().Nombre;
                ViewData["NOMEQUIPO"] = nombreEquipo;
                return View(plantilla);
            }
            else
            {
                ViewData["MENSAJE"] = "Plantilla vacía chato";
                return View();
            }
        }

      
        public async Task<IActionResult> Perfil()
        {
            int idUsuario = (int)HttpContext.Session.GetInt32("Id");
            var usuario = await this.repo.GetUsuarioAsync(idUsuario);
            return View(usuario);
        }

        
         public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Equipo equipo)
        {            
            await repo.AddEquipoAsync(equipo.Nombre, equipo.Codigo, equipo.Confirmado.ToString());
            return RedirectToAction("Equipos", "Torneos");
               
        }

        public async Task<IActionResult> DeleteTeam(int idEquipo)
        {
            await this.repo.DeleteEquipoAsync(idEquipo);
            return RedirectToAction("Equipos");
        }

    }
}
