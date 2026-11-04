using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 99999999, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}