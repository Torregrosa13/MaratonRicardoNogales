using Microsoft.EntityFrameworkCore;
using MaratonRicardoNogales.Data;
using MaratonRicardoNogales.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public async Task<List<Equipo>> GetEquiposAsync()
        {
            var equipos = await this.context.Equipos
                .ToListAsync();
            return equipos;
        }

        public async Task<List<Jugador>> GetPlantillaEquipos(int idEquipo)
        {
            var plantilla = await (from datos in this.context.Jugadores
                            where datos.EquipoId == idEquipo
                            select datos)
                            .Include(i => i.Equipo)
                            .ToListAsync();         
            return plantilla;
        }
    }
}
