using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("JUGADORES")]
    public class Jugador
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("EQUIPO_ID")]
        public int EquipoId { get; set; }

        [ForeignKey("EquipoId")]
        public Equipo Equipo { get; set; }
    }
}
