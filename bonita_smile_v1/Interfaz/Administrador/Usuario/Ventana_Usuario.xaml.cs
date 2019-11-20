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
using bonita_smile_v1.Interfaz.Administrador.Usuario;

namespace bonita_smile_v1.Interfaz.Administrador.Usuario
{
    /// <summary>
    /// Lógica de interacción para Ventana_Usuario.xaml
    /// </summary>
    public partial class Ventana_Usuario : MetroWindow
    {
        public Ventana_Usuario()
        {
            InitializeComponent();
            llenar_list_view();
        }

       void llenar_list_view()
        {
            Usuarios user = new Usuarios();
            List<UsuarioModel> items = new List<UsuarioModel>();
            items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/
            lv_Users.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Insertar_Usuario iu = new Insertar_Usuario();
            iu.ShowDialog();
        }
    }
}
