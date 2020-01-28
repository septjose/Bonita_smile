using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        ObservableCollection<PacienteModel> GPaciente;
        bool bandera_online_offline = false;
        public Page6()
        {
            InitializeComponent();
            llenar_list_view();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
                return true;
            else
                return ((item as PacienteModel).nombre.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0 || (item as PacienteModel).apellidos.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void Borrar(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                string id_paciente = paciente.id_paciente;
                string nombre_paciente = paciente.nombre;

                    var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  paciente :" + nombre_paciente + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (confirmation == System.Windows.Forms.DialogResult.Yes)
                    {
                        Paciente clin = new Paciente(bandera_online_offline);

                        bool elimino = clin.eliminarPaciente(id_paciente);
                        if (elimino)
                        {
                            GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                            System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clin = new Paciente(!bandera_online_offline);

                            clin.eliminarPaciente(id_paciente);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo eliminar la  clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {

                //ActualizarPaciente ap = new ActualizarPaciente(paciente);
                //ap.ShowDialog()
                string destFile = System.IO.Path.Combine();
                
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                    admin.Main.Content = new Page6_Actualizar(paciente);
                lv_Paciente.ItemsSource = null;
                lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void llenar_list_view()
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page6_Ingresar();

            lv_Paciente.ItemsSource = null;
           lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());

            
        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }
    }
}
