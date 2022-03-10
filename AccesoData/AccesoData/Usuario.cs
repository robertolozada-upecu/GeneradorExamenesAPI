﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoData
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public decimal TotalExamenes { get; set; }
        public string RolUsuario { get; set; }
        
        //[Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
    }
}