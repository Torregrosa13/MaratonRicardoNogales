using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("GOLEADORES")]
    public class Goleador
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PARTIDO_ID")]
        public int PartidoId { get; set; }

        [Column("JUGADOR_ID")]
        public int JugadorId { get; set; }

        [Column("CANTIDAD")]
        public int Cantidad { get; set; }
    }
}
