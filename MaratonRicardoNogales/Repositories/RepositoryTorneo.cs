using Microsoft.EntityFrameworkCore;
using MaratonRicardoNogales.Data;
using MaratonRicardoNogales.Models;

namespace MaratonRicardoNogales.Repositories
{
    public class RepositoryTorneo
    {
        public TournamentContext context;

        public RepositoryTorneo(TournamentContext context)
        {
            this.context = context;
        }

        public async Task<List<Partido>> GetPartidosAsync()
        {
            var partidos = await this.context.Partidos
                .Include(p => p.EquipoLocal)
                .Include(p => p.EquipoVisitante)
                .ToListAsync();
            return partidos;
        }
    }
}
