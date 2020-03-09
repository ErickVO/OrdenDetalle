using Microsoft.EntityFrameworkCore;
using OrdenDetalle.DAL;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdenDetalle.BLL
{
    public class ProductosBLL
    {
        public static bool Guardar(Productos productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if(db.Productos.Add(productos) != null)
                {
                    paso = (db.SaveChanges() > 0);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;

        }

        public static bool Modificar(Productos productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = ProductosBLL.Buscar(productos.ProductoId);
                foreach (var item in Anterior.OrdenDetalle)
                {
                    if (!productos.OrdenDetalle.Exists(d => d.ProductoId == item.ProductoId))
                        db.Entry(item).State = EntityState.Deleted;
                }
                db.Entry(productos).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;

        }

        public static bool Eliminar(int id )
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Productos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
                
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;

        }

        public static Productos Buscar(int id)
        {
            Productos productos = new Productos();
            Contexto db = new Contexto();

            try
            {
                productos = db.Productos.Include(x => x.OrdenDetalle)
                    .Where(x => x.ProductoId == id)
                    .SingleOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return productos;

        }
    }
}
