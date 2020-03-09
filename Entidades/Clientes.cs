using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public String Nombre { get; set; }
        public String Cedula { get; set; }
        public String Telefono { get; set; }


        [ForeignKey("ClienteId")]
        public virtual List<Ordenes> Orden { get; set;}
        public Clientes()
        {
            ClienteId = 0;
            Nombre = String.Empty;
            Cedula = String.Empty;
            Telefono = String.Empty;

            Orden = new List<Ordenes>();
        }
    }
}
