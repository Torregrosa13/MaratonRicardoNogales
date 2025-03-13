using Microsoft.AspNetCore.Mvc;
using MaratonRicardoNogales.Models;
using MaratonRicardoNogales.Repositories;

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
    }
}
