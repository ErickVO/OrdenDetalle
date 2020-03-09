using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public String NombreProducto { get; set; }
        public decimal Precio { get; set; }

        [ForeignKey("ProductoId")]
        public virtual List<OrdenDetalles> OrdenDetalle { get; set; }

        public Productos()
        {
            ProductoId = 0;
            NombreProducto = String.Empty;
            Precio = 0;
        }
    }
}
