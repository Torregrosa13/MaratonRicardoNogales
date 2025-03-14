using Microsoft.EntityFrameworkCore;
using MaratonRicardoNogales.Data;
using MaratonRicardoNogales.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Diagnostics.Metrics;
using Humanizer;

namespace MaratonRicardoNogales.Repositories
{
    #region PROCEDURES
    //    alter PROCEDURE SP_ADD_EQUIPO
    //    @Nombre NVARCHAR(100),
    //    @Codigo NVARCHAR(50),
    //    @Confirmado BIT
    //AS
    //BEGIN
    //    INSERT INTO Equipos(Nombre, Codigo, Confirmado)
    //    VALUES(@Nombre, @Codigo, @Confirmado);
    //    END;

//    CREATE PROCEDURE SP_ADD_JUGADOR_EQUIPO
//    @Nombre NVARCHAR(100),
//    @EquipoId INT,
//    @Dorsal INT
//AS
//BEGIN
//    SET NOCOUNT ON;

//    INSERT INTO JUGADORES(NOMBRE, EQUIPO_ID, DORSAL)
//    VALUES(@Nombre, @EquipoId, @Dorsal);
//    END;



    #endregion
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

        public async Task<List<Jugador>> GetPlantillaEquiposAsync(int idEquipo)
        {
            var plantilla = await (from datos in this.context.Jugadores
                            where datos.EquipoId == idEquipo
                            select datos)
                            .Include(i => i.Equipo)
                            .ToListAsync();         
            return plantilla;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            Usuario usuario = await this.context.Usuarios
                .Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return usuario;
        }

        public List<Usuario> GetAllUsuarios()
        {
            return context.Usuarios.ToList(); 
        }

        public List<Grupo> GetAllGrupos()
        {
            return context.Grupos.ToList(); 
        }


        public async Task AddEquipoAsync(string Nombre, string Codigo, string Confirmado)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nombre", Nombre),
                new SqlParameter("@Codigo", Codigo),
                new SqlParameter("@Confirmado", Confirmado)
            };
            await this.context.Database.ExecuteSqlRawAsync("EXEC SP_ADD_EQUIPO @Nombre, @Codigo, @Confirmado", parameters);
        }

        public async Task<Usuario> GetUsuarioAsync(int id)
        {
            var usuario = await this.context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
            return usuario;
     
        }

        public async Task<List<Jugador>> GetPlantillaAsync(int idEquipo)
        {
            List<Jugador> plantilla = await this.context.Jugadores
                .Where(x => x.EquipoId == idEquipo).ToListAsync();
            return plantilla;
        }

        public async Task DeleteEquipoAsync(int idEquipo)
        {
            var equipo = await this.context.Equipos.FindAsync(idEquipo);
            this.context.Equipos.Remove(equipo);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEquipoAsync(Equipo equipo)
        {

        }

        public async Task AddJugadorEquipoAsync(string Nombre, int EquipoId, int Dorsal)
        {
            var equipoExists = await this.context.Equipos.AnyAsync(e => e.Id == EquipoId);
            if (!equipoExists)
            {
                throw new ArgumentException("Invalid EquipoId");
            }

            var parameters = new[]
            {
                new SqlParameter("@Nombre", Nombre),
                new SqlParameter("@EquipoId", EquipoId),
                new SqlParameter("Dorsal", Dorsal)
            };
            await this.context.Database.ExecuteSqlRawAsync("EXEC SP_ADD_JUGADOR_EQUIPO @Nombre, @EquipoId, @Dorsal", parameters);
        }


        public async Task<Equipo> FindEquipoAsync(int idEquipo)
        {
            var equipo = await this.context.Equipos.Where(x => x.Id == idEquipo).FirstOrDefaultAsync();
            return equipo;
        }
    }
}
