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
using bonita_smile_v1.Interfaz.Administrador.Usuario;

namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Ventana_Administrador.xaml
    /// </summary>
    public partial class Ventana_Administrador : Window
    {
        public Ventana_Administrador()
        {
            InitializeComponent();
        }

        private void usu_nuevo_Click(object sender, RoutedEventArgs e)
        {

            Insertar_Usuario iu = new Insertar_Usuario();
            iu.ShowDialog();
        }

        private void usu_borrar_Click(object sender, RoutedEventArgs e)
        {

            Ventana_Usuario vu = new Ventana_Usuario();
            vu.ShowDialog();
        }

        private void usu_ver_Click(object sender, RoutedEventArgs e)
        {

            Ventana_Usuario vu = new Ventana_Usuario();
            vu.ShowDialog();
        }

        private void usu_actualizar_Click(object sender, RoutedEventArgs e)
        {

            Ventana_Usuario vu = new Ventana_Usuario();
            vu.ShowDialog();
        }


    }
}
