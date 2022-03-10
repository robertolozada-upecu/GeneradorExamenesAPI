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
        /// <summary>
        /// Inicializa la Base de datos
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="db"></param>
        public InicializadorDB(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        /// <summary>
        /// Fuerza la migración de la base de datos de existir algo pendiente y crea un rol de administrator si no existe
        /// </summary>
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
                FechaCreacion = DateTime.Now,
                EmailConfirmed = true
            };

            _userManager.CreateAsync(usuario, "Admin1234*").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(usuario, Roles.Administrador).GetAwaiter().GetResult();
        }
    }
}
