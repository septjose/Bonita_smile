using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using bonita_smile_v1.Servicios;
using System.Windows.Forms;

namespace bonita_smile_v1.Interfaz.Administrador.Paciente
{
    /// <summary>
    /// Lógica de interacción para MostrarPacientes.xaml
    /// </summary>
    public partial class MostrarPacientes : MetroWindow
    {
        ObservableCollection<PacienteModel> GPaciente;
        public MostrarPacientes()
        {
            InitializeComponent();
            llenar_list_view();
        }

        private void Borrar(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                int id_clinica = paciente.clinica.id_clinica;
                string nombre_paciente = paciente.nombre;
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  paciente :" + nombre_paciente + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Clinicas clin = new Clinicas();

                    bool elimino = clin.eliminarClinica(id_clinica);
                    if (elimino)
                    {
                        GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                        System.Windows.Forms.MessageBox.Show("Se elimino la clinica correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                
                ActualizarPaciente ap = new ActualizarPaciente(paciente);
                ap.ShowDialog();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void llenar_list_view()
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente().MostrarPaciente());

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }
    }
}