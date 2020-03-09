using Microsoft.EntityFrameworkCore;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdenDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Productos> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Ordenes.db");
        }
    }
}
