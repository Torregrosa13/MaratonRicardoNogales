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
    }
}
