using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoData
{
    /// <summary>
    /// Clase Examen
    /// </summary>
    public class Examen
    {
        [Key]
        public int ExamenId { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; }
        public string NombreExamen { get; set; }
        public string? Descripcion { get; set; }
        public int TotalExamenes { get; set; }
        public DateTime FechaCreacionExamen { get; set; }
    }
}
