using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
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
    /// Lógica de interacción para Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        ObservableCollection<ClinicaModel> Gclinica;
        public Page5()
        {
            InitializeComponent();
            llenar_list_view();
        }
        void llenar_list_view()
        {
            //Usuarios user = new Usuarios();
            //List<UsuarioModel> items = new List<UsuarioModel>();
            //items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/

            //ObservableCollection<UsuarioModel> Gusuario;
            var clinicas = new ObservableCollection<ClinicaModel>((new Clinicas().MostrarClinica()));

            lv_Clinica.ItemsSource = clinicas;
            //lv_aux = lv_Users;
            Gclinica = clinicas;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClinicaModel clinica = (ClinicaModel)lv_Clinica.SelectedItem;
            if (lv_Clinica.SelectedItems.Count > 0)
            {
                int id_clinica = clinica.id_clinica;
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                    admin.Main.Content = new Page5_Actualizar(clinica); 
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClinicaModel clinica = (ClinicaModel)lv_Clinica.SelectedItem;
            if (lv_Clinica.SelectedItems.Count > 0)
            {
                int id_clinica = clinica.id_clinica;
                string nombre_sucursal = clinica.nombre_sucursal;
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar la clinica :" + nombre_sucursal + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Clinicas clin = new Clinicas();

                    bool elimino = clin.eliminarClinica(id_clinica);
                    if (elimino)
                    {
                        Gclinica.Remove((ClinicaModel)lv_Clinica.SelectedItem);
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

        private void lv_Clinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page5_Ingresar(); ;
        }
    }
}
