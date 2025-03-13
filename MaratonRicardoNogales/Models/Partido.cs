using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("PARTIDOS")]
    public class Partido
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("EQUIPO_LOCAL_ID")]
        public int EquipoLocalId { get; set; }

        [Column("EQUIPO_VISITANTE_ID")]
        public int EquipoVisitanteId { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("GOLES_LOCAL")]
        public int GolesLocal { get; set; }

        [Column("GOLES_VISITANTE")]
        public int GolesVisitante { get; set; }

        [Column("FINALIZADO")]
        public bool Finalizado { get; set; }

        [Column("FASE")]
        public string Fase { get; set; }

        public virtual Equipo EquipoLocal { get; set; }
        public virtual Equipo EquipoVisitante { get; set; }
    }
}
