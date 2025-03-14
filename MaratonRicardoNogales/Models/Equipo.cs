using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("EQUIPOS")]
    public class Equipo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("CODIGO")]
        public string Codigo { get; set; }

        [Column("CONFIRMADO")]
        public bool Confirmado { get; set; }

        [Column("GRUPO_ID")]
        public int? GrupoId { get; set; }

        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }

    }
}
