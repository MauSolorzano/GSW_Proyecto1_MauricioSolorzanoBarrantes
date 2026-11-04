using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}