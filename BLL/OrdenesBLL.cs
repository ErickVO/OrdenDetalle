using Microsoft.EntityFrameworkCore;
using OrdenDetalle.DAL;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdenDetalle.BLL
{
   public class OrdenesBLL
    {
        public static bool Guardar(Ordenes ordenes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if(db.Ordenes.Add(ordenes) != null)
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

        public static bool Modificar(Ordenes ordenes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenDetalles Where OrdenId={ordenes.OrdenId}");
                foreach (var item in ordenes.OrdenDetalles)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(ordenes).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Ordenes ordenes = new Ordenes();
            Contexto db = new Contexto();


            try 
            {
                ordenes = db.Ordenes.Include(x => x.OrdenDetalles)
                    .Where(x => x.OrdenId == id)
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

            return ordenes;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Ordenes.Find(id);
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
    }
}
