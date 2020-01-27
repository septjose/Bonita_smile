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
using Image = System.Windows.Controls.Image;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page8_IngresarFoto.xaml
    /// </summary>
    public partial class Page8_ActualizarFoto : Page
    {

        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        private bool HayDispositivos;
        private string ruta = @"C:\bs\";
        private string ruta_aux = @"C:\bs_auxiliar\";
        private string ruta2 = @"C:\capturas\";
        private string ruta_offline = @"C:\fotos_offline\";
        private MySqlDataReader reader = null;
        private string query;
        string foto_vieja = "";
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        bool valor_bandera = false;
        bool bandera_online_offline = false;

        PacienteModel paciente;
        public Page8_ActualizarFoto(PacienteModel paciente)
        {
            //MessageBox.Show(paciente.apellidos + "  "+paciente.nombre+"  "+paciente.clinica.id_clinica.ToString()+"   "+paciente.antecedente);
           
            InitializeComponent();
            CargaDispositivos();
            this.paciente = paciente;
            foto_vieja = paciente.foto;
            //File.Delete(ruta2 + paciente.foto);
            //System.Windows.MessageBox.Show("el paciente es " + paciente.foto);

            if(paciente.foto.Equals(""))
            {
                string ruta2 = @"C:\bs\img1.jpg";
                rt_imagen.Fill = Imagen(ruta2);
            }
            else
            {
                rt_imagen.Fill = Imagen( paciente.foto);

                
            }
           
        }
        public ImageBrush Imagen(string filename)
        {
            string rutisima = "";
            string ruta3 = @"C:\bs\img1.jpg";
            if (File.Exists(ruta + filename) && File.Exists(ruta_aux + filename))
            {
                try
                {
                    File.Delete(ruta + filename);
                    string destFile = System.IO.Path.Combine(@"C:\bs\", filename);
                    //MessageBox.Show("el valor de result es " + result);
                    System.IO.File.Copy(ruta_aux + filename, destFile, true);
                    File.Delete(ruta_aux + filename);
                    rutisima = ruta + filename;
                    Image image = new Image();
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new System.Uri(rutisima);
                    bi.EndInit();
                    image.Source = bi;
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = bi;
                    return ib;
                }
                catch (Exception ex)
                {
                    rutisima = ruta_aux + filename;
                    Image image = new Image();
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new System.Uri(rutisima);
                    bi.EndInit();
                    image.Source = bi;
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = bi;
                    return ib;
                }


                //rt_imagen.Fill = ib;
            }
            else
            if (File.Exists(ruta + filename))
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta + filename);
                bi.EndInit();
                image.Source = bi;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;
                return ib;
            }
            else
            if (File.Exists(ruta_aux + filename))
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta_aux + filename);
                bi.EndInit();
                image.Source = bi;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;
                return ib;
            }
            else
            {
                Image image = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new System.Uri(ruta3);
                bi.EndInit();
                image.Source = bi;
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;
                return ib;
                //rt_imagen.Fill = ib;
            }
        }



        public void CargaDispositivos()
        {
            MisDispositivios = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivios.Count > 0)
            {
                HayDispositivos = true;
                for (int i = 0; i < MisDispositivios.Count; i++)
                    comboBox1.Items.Add(MisDispositivios[i].Name.ToString());
                comboBox1.Text = MisDispositivios[0].Name.ToString();
            }
            else
                HayDispositivos = false;
        }
        private void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string foto = this.paciente.nombre + "_" + this.paciente.apellidos + "_" + this.paciente.id_paciente + ".jpg";
            foto = foto.Replace(" ", "_");

            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                /*CerrarWebCam();
                string filePath = ruta +foto;
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);*/
                System.Windows.Forms.MessageBox.Show("Error no capturo la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string filePath = "";
                Test_Internet test_i = new Test_Internet();
                if (test_i.Test())
                {
                    filePath = ruta2 + foto;
                }
                else
                {
                    filePath = ruta_offline + foto;
                }

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);
                Paciente paciente = new Paciente(bandera_online_offline);

                bool insertarPaciente = paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
                Test_Internet ti = new Test_Internet();

                if (insertarPaciente)
                {
                    paciente = new Paciente(!bandera_online_offline);

                    paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
                    if (ti.Test())
                    {

                        System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + foto_vieja);
                        bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");
                        if (verdad)
                        {
                            bool subir = SubirFicheroStockFTP(foto, ruta2);

                            if (subir)
                            {
                                rt_imagen.Fill = null;
                                try
                                {
                                    string destFile = System.IO.Path.Combine(@"C:\bs_auxiliar\", foto);
                                    //MessageBox.Show("el valor de result es " + result);
                                    System.IO.File.Copy(ruta2 + foto, destFile, true);
                                    File.Delete(ruta2 + foto);
                                }
                                catch (Exception ex)
                                {
                                    string destFile = System.IO.Path.Combine(@"C:\bs\", foto);
                                    //MessageBox.Show("el valor de result es " + result);
                                    System.IO.File.Copy(ruta2 + foto, destFile, true);
                                    File.Delete(ruta2 + foto);
                                }


                                //bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", foto,
                                //@"C:\bs\" + foto, 10);



                                //System.IO.File.Delete(ruta + foto);


                                System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page6();
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    else
                    {

                        System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", " Falta de Internet ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo registrar el paciente ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

           


        }
         
        public bool SubirFicheroStockFTP(string foto, string ruta)
        {
            bool verdad;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                        "ftp://jjdeveloperswdm.com" +
                        "/" +
                       foto);

                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        ruta,
                        foto);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(
                        "bonita_smile@jjdeveloperswdm.com",
                        "bonita_smile");

                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var fileStream = File.OpenRead(filePath))
                {
                    using (var requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream);
                        requestStream.Close();
                    }
                }

                var response = (FtpWebResponse)request.GetResponse();

                response.Close();
                verdad = true;
            }
            catch (Exception ex)
            {
                //logger.Error("Error " + ex.Message + " " + ex.StackTrace);
                verdad = false;
                System.Windows.MessageBox.Show("Error " + ex.Message + " " + ex.StackTrace);


            }
            return verdad;

        }
        public static bool DeleteFileOnServer(Uri serverUri, string ftpUsername, string ftpPassword)
        {

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);

                //If you need to use network credentials
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                //additionally, if you want to use the current user's network credentials, just use:
                //System.Net.CredentialCache.DefaultNetworkCredentials

                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MisDispositivios[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();

        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    img1.Source = bi;
                }));
            }
            catch (Exception ex)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CerrarWebCam();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

       
        

        

        
    }
}
