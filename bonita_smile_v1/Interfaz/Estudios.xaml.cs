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
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Modelos;


namespace bonita_smile_v1

{
    /// <summary>
    /// Lógica de interacción para Pagina_Estudios.xaml
    /// </summary>
    public partial class Pagina_Estudios : Page
    {
        ObservableCollection<Carpeta_archivosModel> GCarpetas;
        int id_paciente = 0;
        public Pagina_Estudios(PacienteModel paciente)
        {
            InitializeComponent();
            //MessageBox.Show("El valor del id_del paciente es :" + paciente.id_paciente);
            //MessageBox.Show("El nombre del paciente es " + paciente.nombre);
            id_paciente = paciente.id_paciente;
            llenar_list_view(id_paciente);
        }

        void llenar_list_view(int id_paciente)
        {
            var carpetas = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos().MostrarCarpeta_archivos_paciente(id_paciente));

            lvCarpetas.ItemsSource = carpetas;
            GCarpetas = carpetas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            if (lvCarpetas.SelectedItems.Count > 0)

            {
                //System.Windows.MessageBox.Show( carpeta.id_carpeta+"");
                //System.Windows.MessageBox.Show(carpeta.id_paciente + "");
                //System.Windows.MessageBox.Show("hi");
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                if (admin != null)
                    admin.Main.Content = new Pagina_Agregar_Estudios(carpeta);
                else
                {
                    clin.Main2.Content = new Pagina_Agregar_Estudios(carpeta);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Agregar_Carpetas(id_paciente);
            resultado = mensaje.ShowDialog();
        }



        private void lvCarpetas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            if (lvCarpetas.SelectedItems.Count > 0)

            {
                // System.Windows.MessageBox.Show(carpeta.id_carpeta + "");
                // System.Windows.MessageBox.Show(carpeta.id_paciente + "");
                //System.Windows.MessageBox.Show("hi");
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                if (admin != null)
                    admin.Main.Content = new Fotos_de_Estudios(carpeta);
                else
                {
                    clin.Main2.Content = new Fotos_de_Estudios(carpeta);
                }
            }
        }

       
        private void EditZoneInfoContextMenu_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Agregar_Carpetas(id_paciente);
            resultado = mensaje.ShowDialog();
        }

        

        private void lvCarpetas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
