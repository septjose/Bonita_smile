using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Configuration;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Threading;
using Size = System.Windows.Size;
using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Interfaz.Administrador;
using System.Windows.Forms;
using bonita_smile_v1.Interfaz.Recepcionista;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page6_Ingresar.xaml
    /// </summary>
    public partial class Page6_Ingresar : Page
    {
        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        private bool HayDispositivos;
        private string ruta = @"E:\PortableGit\programs_c#\ftp_v1.0\ftp_camara\";
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        public Page6_Ingresar()
        {
            this.conexionBD = obj.conexion(false);
            InitializeComponent();
           
            llenar_Combo();
        }

        private void Capturar_Click(object sender, RoutedEventArgs e)
        {

        }

        public void llenar_Combo()
        {
            query = "SELECT * FROM clinica";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    string clinica = reader[1].ToString();
                    cmbClinica.Items.Add(clinica);

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
     
        private void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }

        
   


        public void SaveToJpeg(FrameworkElement visual, string fileName)
        {
            var encoder = new JpegBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }


        private void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 2, 2, PixelFormats.Pbgra32);
            Size visualSize = new Size(visual.ActualWidth, visual.ActualHeight);
            visual.Measure(visualSize);
            visual.Arrange(new Rect(visualSize));
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

            if (txtNombre.Text.Equals("")|| txtApellidos.Text.Equals("")||txtDireccion.Text.Equals("") || cmbClinica.SelectedIndex.Equals(-1))
            {
                System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                valor = cmbClinica.SelectedItem.ToString();
                string id_clinica = obtener_id_clinica(valor);
                PacienteModel pacienteModel = new PacienteModel();
                ClinicaModel clinicaModel = new ClinicaModel();

                pacienteModel.apellidos = txtApellidos.Text;
                pacienteModel.nombre = txtNombre.Text;
                pacienteModel.direccion = txtDireccion.Text;
                pacienteModel.telefono = txtTelefono.Text;
                pacienteModel.foto = "";
                pacienteModel.email = txtEmail.Text;
                pacienteModel.marketing = 0;
                clinicaModel.id_clinica = id_clinica;
                //pacienteModel.id_clinica = int.Parse(txtclinica.Text.ToString());
                pacienteModel.clinica = clinicaModel;
                try
                {
                    
                    bool email_correcto = new Seguridad().email_bien_escrito(txtEmail.Text);
                    if (email_correcto)
                    {
                        if(new Seguridad().ValidarTelefonos7a10Digitos(txtTelefono.Text))
                        {
 string tel = String.Format("{0:(###) ###-####}", Int32.Parse(txtTelefono.Text));                            // new Ingresar_Antecedentes_Clinicos(pacienteModel).ShowDialog();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();

                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            if (admin != null)
                            {
                                admin.Main.Content = new Page7_Ingresar(pacienteModel, null, "");
                            }
                            else
                                if (recep != null)
                            {
                                recep.Main3.Content = new Page7_Ingresar(pacienteModel, null, "");
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono debe de tener 10 digitos", "Teléfono no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }  
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Correo no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }  
                }
                catch (Exception ex)
                {
                   
                }
            }
           
           
            //MessageBox.Show(nombre + " " + apellidos + " " + direccion + " " + telefono + " " + email);

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    valor = cmbClinica.SelectedItem.ToString();
                    string id_clinica = obtener_id_clinica(valor);
                    Paciente pa = new Paciente(false);
                    bool email_correcto = new Seguridad().email_bien_escrito(txtEmail.Text);
                    if (email_correcto)
                    {

                        if(new Seguridad().ValidarTelefonos7a10Digitos(txtTelefono.Text))
                        {
                           string tel = String.Format("{0:(###) ###-####}", Int32.Parse(txtTelefono.Text));
                            System.Windows.MessageBox.Show(tel);
                            bool inserto = pa.insertarPaciente(txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, "", "", txtEmail.Text, 0, id_clinica);
                            if (inserto)

                            {
                                pa = new Paciente(true);
                                pa.insertarPaciente(txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, "", "", txtEmail.Text, 0, id_clinica);
                                Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                {
                                    admin.Main.Content = new Page6();
                                    System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                if (recep != null)
                                {
                                    recep.Main3.Content = new Page6();
                                    System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar el Paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono debe de tener 10 digitos", "Teléfono no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Correo no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        

                } catch (Exception ex) 
                {
                    System.Windows.Forms.MessageBox.Show("No selecciono el comboBox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
             }      

        }
            public string obtener_id_clinica(string nombre_sucursal)
        {
            string id = "";
            query = "SELECT id_clinica FROM clinica where nombre_sucursal='" + nombre_sucursal + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    id = reader[0].ToString();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            conexionBD.Close();

            return id;
        }

       

        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
