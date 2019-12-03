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
using bonita_smile_v1.Interfaz.Administrador.Paciente;
using bonita_smile_v1.Modelos;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Ventana_Administrador.xaml
    /// </summary>
    public partial class Ventana_Administrador : Window
    {
        ObservableCollection<PacienteModel> GPaciente;
        public Ventana_Administrador()
        {
            InitializeComponent();
            llenar_list_view();
        }

        void llenar_list_view()
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente().MostrarPaciente());

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
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

            ventana_Clinica vc = new ventana_Clinica();
            vc.ShowDialog();
        }

        //Paciente
        private void pac_nuevo_Click(object sender, RoutedEventArgs e)
        {

            Insertar_Paciente ip = new Insertar_Paciente();
            ip.ShowDialog();
        }

        private void pac_borrar_Click(object sender, RoutedEventArgs e)
        {

            MostrarPacientes mp = new MostrarPacientes();
            mp.ShowDialog();
        }

        private void pac_ver_Click(object sender, RoutedEventArgs e)
        {

            MostrarPacientes mp = new MostrarPacientes();
            mp.ShowDialog();
        }

        private void pac_actualizar_Click(object sender, RoutedEventArgs e)
        {

            MostrarPacientes mp = new MostrarPacientes();
            mp.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtnombre.Text;

          

                var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente().MostrarPaciente_unico(nombre));

                lv_Paciente.ItemsSource = pacientes;
                GPaciente = pacientes;
            

        }

        private void lv_Paciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            llenar_list_view();
            txtnombre.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                //System.Windows.MessageBox.Show("hi");
                Historial_clinico hc = new Historial_clinico(paciente);
                hc.ShowDialog();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
