using OrdenDetalle.BLL;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrdenDetalle.UI
{
    /// <summary>
    /// Interaction logic for RegistroProducto.xaml
    /// </summary>
    public partial class RegistroProducto : Window
    {
        Productos productos = new Productos();
        public RegistroProducto()
        {
            InitializeComponent();
            this.DataContext = productos;
            IdProductoTextBox.Text = "0";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = productos;
        }

        private void Limpiar()
        {
            IdProductoTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            PrecioTextBox.Text = string.Empty;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteBaseDatos()
        {
            Productos p = ProductosBLL.Buscar((int)Convert.ToInt32(IdProductoTextBox.Text));

            return (p != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (IdProductoTextBox.Text == "0")
            {
                paso = ProductosBLL.Guardar(productos);
            }
            else
            {
                if (!ExisteBaseDatos())
                {
                    MessageBox.Show("No existe en la BLL", "ERROR");
                    return;

                }
                paso = ProductosBLL.Modificar(productos);
            }

            if (paso)
            {
                MessageBox.Show("Guardado!!", "Exito");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se guardo...", "ERROR");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosBLL.Eliminar(productos.ProductoId))
            {
                Limpiar();
                MessageBox.Show("Eliminado", "EXITO");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar...", "ERROR");
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos p = ProductosBLL.Buscar(productos.ProductoId);

            if (p != null)
            {
                productos = p;
                Cargar();
            }
            else
            {
                MessageBox.Show("No se encontro", "Error");
            }
        }
    }
}
