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

        public async Task<List<Jugador>> GetPlantillaEquipos(int idEquipo)
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


        public async Task AddEquipoAsync(Equipo equipo)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nombre",equipo.Nombre),
                new SqlParameter("@Codigo",equipo.Codigo),
                new SqlParameter("@Confirmado",equipo.Confirmado)
            };
            await this.context.Database.ExecuteSqlRawAsync("EXEC SP_ADD_EQUIPO @Nombre, @Codigo, @Confirmado", parameters);
        }

    }
}
