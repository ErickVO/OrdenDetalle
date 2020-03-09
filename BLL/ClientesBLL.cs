using Microsoft.EntityFrameworkCore;
using OrdenDetalle.DAL;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdenDetalle.BLL
{
    public class ClientesBLL
    {
        public static bool Guardar(Clientes clientes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Clientes.Add(clientes) != null)
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

        public static bool Modificar(Clientes clientes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(clientes).State = EntityState.Modified;
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

        public static Clientes Buscar(int id)
        {
            Clientes cliente = new Clientes();
            Contexto db = new Contexto();

            try
            {
                cliente = db.Clientes.Include(x => x.Orden)
                     .Where(x => x.ClienteId == id)
                     .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return cliente;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Clientes.Find(id);
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
