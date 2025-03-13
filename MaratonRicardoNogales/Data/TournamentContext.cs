using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MaratonRicardoNogales.Models;
using System.Text.RegularExpressions;

namespace MaratonRicardoNogales.Data
{
    public class TournamentContext: DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Clasificacion> Clasificaciones { get; set; }
        public DbSet<Goleador> Goleadores { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
    }
}
