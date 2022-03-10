using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccesoData.Contexto
{
    /// <summary>
    /// Clase primaria responsable de interactuar con la base de datos
    /// </summary>
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opciones) : base(opciones)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Examen> RegistroExamen { get; set; }
        public DbSet<Pregunta> RegistroPregunta { get; set; }
        public DbSet<Respuesta> RegistroRespuesta { get; set; }
    }
}