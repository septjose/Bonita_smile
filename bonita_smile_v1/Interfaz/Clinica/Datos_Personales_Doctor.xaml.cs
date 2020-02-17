using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Recepcionista;
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

namespace bonita_smile_v1.Interfaz.Clinica
{
    /// <summary>
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Datos_Personales_Doctor : Page
    {
        ObservableCollection<Motivo_citaModel> GMotivo;

        string id = "";
        //string ruta = @"\\DESKTOP-ED8E774\bs\";
        string ruta;
        PacienteModel paciente;
        bool bandera_online_offline = false;
        Configuracion_Model configuracion;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        public Datos_Personales_Doctor(PacienteModel paciente)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            InitializeComponent();
           
            string color = paciente.clinica.color;
            this.configuracion = configuracion;
            this.ruta = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";
            rt_imagen.Fill = Imagen(this.ruta+paciente.foto);

         



            txtNombre.Text = paciente.nombre + " " + paciente.apellidos;
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

        public Datos_Personales_Doctor()
        {
        }

        void llenar_list_view(string id_paciente)
        {
            var motivos = new ObservableCollection<Motivo_citaModel>(new Servicios.Motivo_cita(false).Mostrar_MotivoCita(id_paciente));

            lvMotivo.ItemsSource = motivos;
            GMotivo = motivos;
        }

        public ImageBrush Imagen(string filename)
        {
            ImageBrush ib = new ImageBrush();
            BitmapImage bi = new BitmapImage();
            string ruta2 = @"E:\PortableGit\programs_c#\bs_v1.4\Bonita_smile\bonita_smile_v1\Assets\img1.jpg";
            if (File.Exists( filename))
            {
      
                Image image = new Image();
                //MessageBox.Show("se encontro la foto en " + filename);

                //MessageBox.Show("A");
                var stream = File.OpenRead( filename);
                //MessageBox.Show("B");
                bi.BeginInit();
                //MessageBox.Show("C");
                bi.CacheOption = BitmapCacheOption.OnLoad;
                //MessageBox.Show("D");
                bi.StreamSource = stream;
                //MessageBox.Show("E");
                bi.EndInit();
                //MessageBox.Show("F");
                stream.Close();
                //MessageBox.Show("G");
                stream.Dispose();
                //MessageBox.Show("H");


                image.Source = bi;

                ib.ImageSource = bi;
                return ib;
                //rt_imagen.Fill = ib;
            }
            else
            {
                
                Image image = new Image();

                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta2);
                bi.EndInit();
                image.Source = bi;

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


                
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                
                if (clin != null)
                {
                    clin.Main2.Content = new Notas_recepcionista(paciente, motivo);
                }

              

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  

        
    }
}
