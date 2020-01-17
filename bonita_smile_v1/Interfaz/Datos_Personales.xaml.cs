using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Modelos;
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
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        ObservableCollection<Motivo_citaModel> GMotivo;

        string id = "";
        PacienteModel paciente;
        public Page2(PacienteModel paciente)
        {
            
            InitializeComponent();
            rt_imagen.Fill= Imagen(@"C:\bs\" + paciente.foto);
            string color = paciente.clinica.color;

            
           
            


            txtNombre.Text = paciente.nombre+" "+paciente.apellidos;
            txtNombre.IsEnabled = false;
            txtDireccion.IsEnabled = false;
            txtTelefono.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtAntecedentes.IsEnabled = false;

            //txtClinica.Text = paciente.clinica.nombre_sucursal;
            txtDireccion.Text = paciente.direccion;
            txtTelefono.Text = paciente.telefono;
            txtEmail.Text = paciente.email;
            txtAntecedentes.Text = paciente.antecedente;
            llenar_list_view(paciente.id_paciente);
            id = paciente.id_paciente;
            this.paciente = paciente;
        }

        public Page2()
        {
        }

        void llenar_list_view(string id_paciente)
        {
            var motivos = new ObservableCollection<Motivo_citaModel>(new Servicios.Motivo_cita().Mostrar_MotivoCita(id_paciente));

            lvMotivo.ItemsSource = motivos;
            GMotivo = motivos;
        }

        public ImageBrush Imagen(string ruta)
        {
            string ruta2 = @"C:\bs\img1.jpg";
            if (File.Exists(ruta))
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta);
                bi.EndInit();
                image.Source = bi;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;
                return ib;
                //rt_imagen.Fill = ib;
            }
            else
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta2);
                bi.EndInit();
                image.Source = bi;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;
                return ib;
                //rt_imagen.Fill = ib;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Motivo_citaModel motivo = (Motivo_citaModel)lvMotivo.SelectedItem;
            if (lvMotivo.SelectedItems.Count > 0)
            {
                //System.Windows.MessageBox.Show("id_paciente :" + motivo.paciente.id_paciente.ToString() + "   " + "id_motivo   " + motivo.id_motivo.ToString());
               

                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                if (admin != null)
                    admin.Main.Content = new Page2_notas(paciente, motivo);
                else
                {
                    clin.Main2.Content = new Page2_notas(paciente, motivo);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //System.Windows.Application.Current.Windows[0].Close();

            //Ingresar_Motivo im = new Ingresar_Motivo(id);
            //im.ShowDialog();
       

            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
                admin.Main.Content = new Page3(id);
            else
            {
                clin.Main2.Content = new Page3(id);
            }
        }

    }
}
