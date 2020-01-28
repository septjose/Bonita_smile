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
using bonita_smile_v1.Servicios;

namespace bonita_smile_v1

{
    /// <summary>
    /// Lógica de interacción para Pagina_Estudios.xaml
    /// </summary>
    public partial class Pagina_Estudios : Page
    {
        ObservableCollection<Carpeta_archivosModel> GCarpetas;
        ObservableCollection<Carpeta_archivosModel> carpetas;
        Carpeta_archivosModel item_carpeta;
        string id_paciente = "";
        string id_motivo = "";
        bool bandera_offline_online = false;
        public Pagina_Estudios(PacienteModel paciente,Motivo_citaModel motivo)
        {
            InitializeComponent();
            //MessageBox.Show("El valor del id_del paciente es :" + paciente.id_paciente);
            //MessageBox.Show("El nombre del paciente es " + paciente.nombre);
            id_paciente = paciente.id_paciente;
            id_motivo = motivo.id_motivo;
            llenar_list_view(id_paciente);
        }

        void llenar_list_view(string id_paciente)
        {
            carpetas = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente));

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

                
            }
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
            Form mensaje = new Agregar_Carpetas(id_paciente,id_motivo);
            resultado = mensaje.ShowDialog();

            lvCarpetas.ItemsSource = null;
            lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente));
        }


        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.item_carpeta = ((FrameworkElement)e.OriginalSource).DataContext as Carpeta_archivosModel;
            //System.Windows.MessageBox.Show(item_carpeta.nombre_carpeta);
            // System.Windows.MessageBox.Show("Clic Derecho");
            System.Windows.Controls.ContextMenu cm = this.FindResource("cmButton") as System.Windows.Controls.ContextMenu;
            cm.PlacementTarget = sender as System.Windows.Controls.Button;
            cm.IsOpen = true;
            e.Handled = true;
        }

        

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show(this.item_carpeta.nombre_carpeta);
            //Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            //if (lvCarpetas.SelectedIndex == -1)
            //{
            //    return;
            //}
            new Carpeta_archivos(bandera_offline_online ).eliminarCarpeta_archivos(this.item_carpeta.id_carpeta);
            new Carpeta_archivos(!bandera_offline_online).eliminarCarpeta_archivos(this.item_carpeta.id_carpeta);
            lvCarpetas.ItemsSource = null;
            lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente));
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Actualizar_Nombre_Carpeta(item_carpeta.id_paciente, item_carpeta.id_carpeta,id_motivo);
            resultado = mensaje.ShowDialog();

            lvCarpetas.ItemsSource = null;
            lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente));
        }
    }
}





















