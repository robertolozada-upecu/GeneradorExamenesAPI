using Microsoft.AspNetCore.Identity;

namespace AccesoData
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}