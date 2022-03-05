using AccesoData;
using AccesoData.Contexto;
using AccesoData.InicializarDB;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccessoData.InicializarDB
{
    public class InicializadorDB : IInicializadorDB
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;

        public InicializadorDB(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void InicializarDB()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
                _db.Database.Migrate();

            if (!_roleManager.RoleExistsAsync(Roles.Administrador).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(Roles.Administrador)).GetAwaiter().GetResult();
            else
                return;

            var usuario = new Usuario
            {
                Nombre = "Admin",
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            _userManager.CreateAsync(usuario, "Admins1234*").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(usuario, Roles.Administrador).GetAwaiter().GetResult();
        }
    }
}
