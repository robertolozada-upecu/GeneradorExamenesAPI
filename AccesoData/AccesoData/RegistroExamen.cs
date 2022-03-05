using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoData
{
    public class RegistroExamen
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; }
        public string NombreExamen { get; set; }
        public int NotaFinal { get; set; }
        public DateTime FechaExamen { get; set; }
    }
}
