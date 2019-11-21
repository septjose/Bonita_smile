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
using bonita_smile_v1.Interfaz.Administrador.Clinica;

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

        //Clinica

        private void cli_nuevo_Click(object sender, RoutedEventArgs e)
        {

            Ingresar_Clinica ic= new Ingresar_Clinica();
            ic.ShowDialog();
        }

        private void cli_borrar_Click(object sender, RoutedEventArgs e)
        {

            ventana_Clinica vc = new ventana_Clinica();
            vc.ShowDialog();
        }

        private void cli_ver_Click(object sender, RoutedEventArgs e)
        {

            ventana_Clinica vc = new ventana_Clinica();
            vc.ShowDialog();
        }

        private void cli_actualizar_Click(object sender, RoutedEventArgs e)
        {

            Actualizar_Clinica ac = new Actualizar_Clinica();
            ac.ShowDialog();
        }
    }
}
