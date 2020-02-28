using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;
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
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        ObservableCollection<Motivo_citaModel> GMotivo;

        string id = "";
        //string ruta = @"\\DESKTOP-ED8E774\bs\";
        string ruta;
        PacienteModel paciente;
        bool bandera_online_offline = false;
        Configuracion_Model configuracion;
        string alias = "";
        string nombre_doctor;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        public Page2(PacienteModel paciente,string nombre_doctor,string alias)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            InitializeComponent();
            this.configuracion = configuracion;
            this.ruta = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";
            rt_imagen.Fill= Imagen(this.ruta+paciente.foto);
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
            this.alias = alias;
            this.nombre_doctor=nombre_doctor;
        }

        public Page2()
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
            string ruta2 =  System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\img1.jpg");;
            if (File.Exists( filename))
            {
                Image image = new Image();
                //MessageBox.Show("se encontro la foto en " + filename);

                //MessageBox.Show("A");
                var stream = File.OpenRead(filename);
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

                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();

                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                //Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                if (admin != null)
                {
                    admin.Main.Content = new Page2_notas(paciente, motivo, nombre_doctor, alias);
                }

                else
                if (clin != null)
                {
                    
                    clin.Main2.Content = new Page2_notas(paciente, motivo, nombre_doctor, alias);
                }
                else
                if (socio != null)
                {

                    socio.Main4.Content = new Page2_notas(paciente, motivo,nombre_doctor,alias);
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
       
            DialogResult resultado = new DialogResult();
            Form mensaje = new IngresarMotivo(id,alias);
            resultado = mensaje.ShowDialog();
            this.GMotivo = new ObservableCollection<Motivo_citaModel>(new Servicios.Motivo_cita(false).Mostrar_MotivoCita(id));
            lvMotivo.ItemsSource = GMotivo;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Motivo_citaModel motivo = (Motivo_citaModel)lvMotivo.SelectedItem;
            if (lvMotivo.SelectedItems.Count > 0)
            {
                    var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el motivo :" + motivo.descripcion + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (confirmation == System.Windows.Forms.DialogResult.Yes)
                    {
                        Motivo_cita mot = new Motivo_cita(bandera_online_offline);
                        bool elimino = mot.eliminarMotivo_cita(motivo.id_motivo,motivo.paciente.id_paciente,alias);
                        if (elimino)
                        {
                        //mot = new Motivo_cita(!bandera_online_offline);
                        //elimino = mot.eliminarMotivo_cita(motivo.id_motivo,motivo.paciente.id_paciente);
                        GMotivo.Remove((Motivo_citaModel)lvMotivo.SelectedItem);

                        //lvMotivo.ItemsSource = GMotivo;
                            //System.Windows.Forms.MessageBox.Show("Se elimino el Motivo correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Motivo_citaModel motivo = (Motivo_citaModel)lvMotivo.SelectedItem;
            if (lvMotivo.SelectedItems.Count > 0)
            {
                DialogResult resultado = new DialogResult();
                Form mensaje = new Actualizar_Motivo(motivo,alias);
                resultado = mensaje.ShowDialog();
                this.GMotivo = new ObservableCollection<Motivo_citaModel>(new Servicios.Motivo_cita(false).Mostrar_MotivoCita(id));
                lvMotivo.ItemsSource = GMotivo;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           
        }
    }
}
