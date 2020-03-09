using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class OrdenDetalle
    {
        [Key]
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public String Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Monto { get; set; }

        public OrdenDetalle()
        {
            OrdenDetalleId = 0;
            OrdenId = 0;
            ProductoId = 0;
            Descripcion = String.Empty;
            Cantidad = 0;
            Precio = 0;
            Monto = 0;
        }
    }
}
