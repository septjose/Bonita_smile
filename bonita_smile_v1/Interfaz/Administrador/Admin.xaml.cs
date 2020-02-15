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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Admin.xaml
    /// </summary>
    public partial class Admin : MetroWindow
    {
        ObservableCollection<PacienteModel> GPaciente;
        public Admin()
        {
            InitializeComponent();
            Main.Content = new Page1();
            //llenar_list_view();

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource);
            //view.Filter = UserFilter;

        }
        /* private bool UserFilter(object item)
         {
             if (String.IsNullOrEmpty(txtNombre.Text))
                 return true;
             else
                 return ((item as PacienteModel).nombre.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0 || (item as PacienteModel).apellidos.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0);
         }

         void llenar_list_view()
         {
             var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente().MostrarPaciente());

             lv_Paciente.ItemsSource = pacientes;
             GPaciente = pacientes;
         }*/
       

        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void lvPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtNombre_KeyUp_1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
        }

        private void txtNombre_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
         
            
               // MessageBox.Show("kakaka");
               // MessageBox.Show(txtNombre.Text = "You Entered: " + txtNombre.Text);
            
        }

        private void listViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page1();
        }

        private void listViewItem1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page4();
        }

        private void listViewItem2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page5();
        }

        private void listViewItem3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Page6();
        }

        private void listViewItem5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Pagina_Permisos(2);
        }

        private void listViewItem6_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Pagina_Permisos(4);
             
        }

        private void listViewItem7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Pagina_Permisos(5);
             
        }

        private void listViewItem4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Ganancias_Clinica();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Mandar ventana para mostrar las configuraciones");
            Archivo_Binario ab = new Archivo_Binario();
            string ruta = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.cfg");

            //string ruta = "E:\\PortableGit\\programs_c#\\bs_v1.4\\Bonita_smile\\bonita_smile_v1\\Assets\\Configuracion.cfg";
            Configuracion_Model configuracion = ab.Cargar(ruta);
            DialogResult resultado = new DialogResult();
            Form mensaje = new Configuracion(configuracion,ruta);
            resultado = mensaje.ShowDialog();
        }
    }
}
