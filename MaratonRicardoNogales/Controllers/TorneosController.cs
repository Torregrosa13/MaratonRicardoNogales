using Microsoft.AspNetCore.Mvc;
using MaratonRicardoNogales.Models;
using MaratonRicardoNogales.Repositories;
using Microsoft.AspNetCore.Identity;
using MaratonRicardoNogales.Filters;

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
            List<Jugador> plantilla = await this.repo.GetPlantillaEquipos(idEquipo);
            var nombreEquipo = plantilla.FirstOrDefault().Equipo.Nombre;
            ViewData["NomEquipo"] = nombreEquipo;
            return View(plantilla);
        }

        [AuthorizeUsuarios]
        public IActionResult Perfil()
        {
            return View();
        }

        
         public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await repo.AddEquipoAsync(equipo);
                    return RedirectToAction("Equipos", "Torneos");
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = ex.Message;
                }
            }
            return View(equipo);
        }

    }
}
