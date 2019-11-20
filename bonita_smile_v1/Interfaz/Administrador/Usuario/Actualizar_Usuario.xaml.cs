using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;


namespace bonita_smile_v1.Interfaz.Administrador.Usuario
{
    /// <summary>
    /// Lógica de interacción para Actualizar_Usuario.xaml
    /// </summary>
    public partial class Actualizar_Usuario : MetroWindow
    {
       
        public Actualizar_Usuario(string alias)
        {


            txtAlias = new TextBox();
            txtAlias.Text = alias;

            InitializeComponent();
          

        }

        

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
