using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccesoData.Contexto
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opciones) : base(opciones)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RegistroExamen> RegistroExamen { get; set; }
    }
}