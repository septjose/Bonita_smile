using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Marketing;


using bonita_smile_v1.Modelos;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Pagina_Marketing : Page
    {
        ObservableCollection<PacienteModel> GPaciente;
        public Pagina_Marketing()
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

        void llenar_list_view()
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente(false).MostrarPaciente());

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                //System.Windows.MessageBox.Show("hi");
                //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                //Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                Market market = System.Windows.Application.Current.Windows.OfType<Market>().FirstOrDefault();

               
                    if (market != null)
                {
                    market.Main3.Content = new Pagina_Correo(paciente);
                }


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                //System.Windows.MessageBox.Show("hi");
                //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                //Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                Market market = System.Windows.Application.Current.Windows.OfType<Market>().FirstOrDefault();


                    market.Main3.Content = new Pagina_Whatssap(paciente);
                


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Market market = System.Windows.Application.Current.Windows.OfType<Market>().FirstOrDefault();


            market.Main3.Content = new Pagina_Messenger();
        }
    }
}
