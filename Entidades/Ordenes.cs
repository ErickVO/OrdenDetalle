using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public String NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }

        [ForeignKey("OrdenId")]
        public virtual List<OrdenDetalles> OrdenDetalles { get; set; }

        public Ordenes()
        {
            OrdenId = 0;
            ClienteId = 0;
            NombreCliente = String.Empty;
            Fecha = DateTime.Now;
            MontoTotal = 0;
            OrdenDetalles = new List<OrdenDetalles>();
        }
    }
}
