using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaratonRicardoNogales.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("ROL")]
        public string Rol { get; set; }

        [Column("EQUIPO_SEGUIDO_ID")]
        public int? EquipoSeguidoId { get; set; }

        [ForeignKey("EquipoSeguidoId")]
        public Equipo EquipoSeguido { get; set; }
    }
}