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
    /// Interaction logic for RegistroOrden.xaml
    /// </summary>
    public partial class RegistroOrden : Window
    {
        Ordenes orden = new Ordenes();


        public RegistroOrden()
        {
            InitializeComponent();
            this.DataContext = orden;
            OrdenIdTextBox.Text = "0";
         

        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = orden;
        }

        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            IdClienteTextBox.Text = string.Empty;
            NombreClienteTextBox.Text = string.Empty;
            ProductoIdTextBox.Text = string.Empty;
            DescripcionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            PrecioTextBox.Text = string.Empty;
            CantidadTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            orden.OrdenDetalles = new List<OrdenDetalles>();
            orden = new Ordenes();
            Cargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes o = OrdenesBLL.Buscar(orden.OrdenId);

            return (o != null);
        }

        private bool ExisteEnLaBaseDeDatosClientes()
        {
            Clientes c = ClientesBLL.Buscar(orden.ClienteId);

            return c != null;
        }

        private bool ExisteEnLaBaseDeDatosProductos()
        {
            Productos p = ProductosBLL.Buscar(Convert.ToInt32(IdClienteTextBox.Text));

            return p != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (orden.OrdenId == 0)
                paso = OrdenesBLL.Guardar(orden);
            else
            {
                if (ExisteEnLaBaseDeDatos() && ExisteEnLaBaseDeDatosClientes() && ExisteEnLaBaseDeDatosProductos())
                {
                    paso = OrdenesBLL.Modificar(orden);
                }
                else
                {
                    MessageBox.Show("No existe en la BLL", "ERROR");
                    return;
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito");
            }
            else
            {
                MessageBox.Show("No se pudo guardar...", "ERROR");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesBLL.Eliminar(orden.OrdenId))
            {
                MessageBox.Show("Eliminado", "EXITO");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar...", "ERROR");
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            Ordenes o = OrdenesBLL.Buscar(orden.OrdenId);

            if (o != null)
            {
                orden = o;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Orden no encontrada", "ERROR");


            }
        }
        private void AgregarButton__Click(object sender, RoutedEventArgs e)
        {
            orden.OrdenDetalles.Add(new OrdenDetalles(orden.OrdenId, Convert.ToInt32(ProductoIdTextBox.Text),
                DescripcionTextBox.Text, Convert.ToDecimal(CantidadTextBox.Text), Convert.ToDecimal(PrecioTextBox.Text), Convert.ToDecimal(MontoTextBox.Text)));
            
            MontoTTextBox.Text = Convert.ToString(orden.MontoTotal);
            Cargar();

            ProductoIdTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
            MontoTextBox.Clear();
            ProductoIdTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenDataGrid.Items.Count > 1 && OrdenDataGrid.SelectedIndex < OrdenDataGrid.Items.Count - 1)
            {
                orden.OrdenDetalles.RemoveAt(OrdenDataGrid.SelectedIndex);
                Cargar();
            }
        }

        private void LlenaNCliente(Clientes cliente)
        {
            NombreClienteTextBox.Text = cliente.Nombre;
            MontoTTextBox.Text = Convert.ToString(orden.MontoTotal);
        }

        private void IdClienteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IdClienteTextBox.Text))
            {
                int id;
                Clientes clientes = new Clientes();
                int.TryParse(IdClienteTextBox.Text, out id);

                clientes = ClientesBLL.Buscar(id);
                if (clientes != null)
                {
                    LlenaNCliente(clientes);
                }
                else
                {
                    NombreClienteTextBox.Text = "No existe este Cliente";
                }
            }
        }

        private void LlenaProductos(Productos productos)
        {
            DescripcionTextBox.Text = productos.NombreProducto;
            PrecioTextBox.Text = Convert.ToString(productos.Precio);
        }


        private void ProductoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProductoIdTextBox.Text))
            {
                int id;
                Productos producto = new Productos();
                int.TryParse(ProductoIdTextBox.Text, out id);

                producto = ProductosBLL.Buscar(id);
                if (producto != null)
                {
                    LlenaProductos(producto);
                }
                else
                {
                    DescripcionTextBox.Text = "No existe el producto";
                    PrecioTextBox.Text = "No Existe el precio";
                }
            }
        }

        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal Monto, Precio, Cantidad;

            decimal.TryParse(PrecioTextBox.Text, out Precio);
            decimal.TryParse(CantidadTextBox.Text, out Cantidad);
            
            Monto = Precio * Cantidad;
            
            MontoTextBox.Text = Convert.ToString(Monto);
        }

       
    }
}
