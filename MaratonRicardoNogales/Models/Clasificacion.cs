using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("CLASIFICACION")]
    public class Clasificacion
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("EQUIPO_ID")]
        public int EquipoId { get; set; }

        [Column("PUNTOS")]
        public int Puntos { get; set; }

        [Column("GOLES_A_FAVOR")]
        public int GolesAFavor { get; set; }

        [Column("GOLES_EN_CONTRA")]
        public int GolesEnContra { get; set; }

        [Column("TARJETAS")]
        public int Tarjetas { get; set; }
    }
}
