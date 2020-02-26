using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
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
namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Admin.xaml
    /// </summary>
    public partial class Soc : MetroWindow
    {
        ObservableCollection<PacienteModel> GPaciente;
        string alias = "";
        List<string> lista = new List<string>();
        string nombre_doctor;
        public Soc(List<string> lista,string nombre_doctor,string alias)
        {
            InitializeComponent();
            Main4.Content = new Pagina_Socio(lista,nombre_doctor,alias);
            this.lista = lista;
            this.alias = alias;
            this.nombre_doctor = nombre_doctor;
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

        private void txtNombre_KeyUp_1(object sender, KeyEventArgs e)
        {

        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {


            // MessageBox.Show("kakaka");
            // MessageBox.Show(txtNombre.Text = "You Entered: " + txtNombre.Text);

        }

        private void listViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new  Pagina_Socio(this.lista,nombre_doctor,alias); 
        }

        private void listViewItem1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new Socio_usuarios(this.lista, alias);
        }

        private void listViewItem2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new Pacientes_socio(this.lista,alias);
        }

        private void listViewItem3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new Permisos_Recepcionista_socio(this.lista, alias, 2);
        }

        private void listViewItem5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new Permisos_Recepcionista_socio(this.lista,alias,4); 
        }
       

        private void listViewItem4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main4.Content = new Ganancias_socio(this.alias,lista); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Paleta_Colores p_c = new Paleta_Colores();
            p_c.ShowDialog();
        }
    }
}
