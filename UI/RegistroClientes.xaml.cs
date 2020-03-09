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
    /// Interaction logic for RegistroClientes.xaml
    /// </summary>
    public partial class RegistroClientes : Window
    {
        Clientes clientes = new Clientes();

        public RegistroClientes()
        {
            InitializeComponent();
            this.DataContext = clientes;
            IdClienteTextBox.Text = "0";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = clientes;
        }

        private void Limpiar()
        {
            IdClienteTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnBaseDatos()
        {
            Clientes c = ClientesBLL.Buscar((int)Convert.ToInt32(IdClienteTextBox.Text));

            return (c != null);

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if(IdClienteTextBox.Text == "0")
            {
                paso = ClientesBLL.Guardar(clientes);
            }
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No existe en la BLL", "ERROR");

                }
                paso = ClientesBLL.Modificar(clientes);
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

            
            if (ClientesBLL.Eliminar(clientes.ClienteId))
            {
                Limpiar();

                MessageBox.Show("Eliminado!!", "EXITO");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar...", "ERROR");
            }

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Clientes c = ClientesBLL.Buscar(clientes.ClienteId);

            if(c != null)
            {
                clientes = c;
                Cargar();   
            }
            else
            {
                MessageBox.Show("No se encontro","Error");
            }

        }
    }
}
