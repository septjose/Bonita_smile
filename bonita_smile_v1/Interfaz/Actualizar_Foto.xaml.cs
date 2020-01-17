﻿using System;
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
        private string ruta = @"C:\capturas\";
        private string ruta2 = @"C:\paciente_foto\";
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        bool valor_bandera = false;

        PacienteModel paciente;
        public Page8_ActualizarFoto(PacienteModel paciente)
        {
            //MessageBox.Show(paciente.apellidos + "  "+paciente.nombre+"  "+paciente.clinica.id_clinica.ToString()+"   "+paciente.antecedente);
           
            InitializeComponent();
            btncapturar.IsEnabled = false;
            btnencender.IsEnabled = false;
            CargaDispositivos();
            this.paciente = paciente;
            //File.Delete(ruta2 + paciente.foto);
            //System.Windows.MessageBox.Show("el paciente es " + paciente.foto);
            if(paciente.foto.Equals(""))
            {
                string ruta2 = @"C:\bs\img1.jpg";
                rt_imagen.Fill = Imagen(ruta2);
            }
            else
            {
                

                bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", paciente.foto, ruta2 + paciente.foto, 10);
                if (descargo)
                {
                    //System.Windows.MessageBox.Show(":)");
                    rt_imagen.Fill = Imagen(@"C:\paciente_foto\" + paciente.foto);
                }
                else
                {
                    //System.Windows.MessageBox.Show(":(");
                }
            }
           
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
        public bool downloadFile(string servidor, string usuario, string password, string archivoOrigen, string carpetaDestino, int bufferdes)
        {
            bool descargar;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(servidor + archivoOrigen);
                reqFTP.Credentials = new NetworkCredential(usuario, password);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(@carpetaDestino, FileMode.Create);
                int Length = bufferdes;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
                descargar = true;
            }
            catch (WebException wEx)
            {
                descargar = false;
                throw wEx;

            }
            catch (Exception ex)
            {
                descargar = false;
                throw ex;


            }
            return descargar;
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

            string foto = this.paciente.nombre + "_" + this.paciente.apellidos + "_" + this.paciente.clinica.nombre_sucursal + ".jpg";
            foto = foto.Replace(" ", "_");
            if(valor_bandera==true)
            {
                if (MiWebCam != null && MiWebCam.IsRunning)
                {

                    /*CerrarWebCam();
                    string filePath = ruta +foto;
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);*/
                    System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string filePath = ruta + foto;
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                }

            }

            Servicios.Paciente paciente = new Servicios.Paciente();

            bool insertarPaciente = paciente.actualizarPaciente(this.paciente.id_paciente,this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);

            if (insertarPaciente)
            {
                
                if(valor_bandera)
                {
                    System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bool subir = SubirFicheroStockFTP(foto, ruta);

                    if (subir)
                    {
                        System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", foto,
                        @"C:\bs\" + foto, 10);
                        // File.Delete(ruta+foto);
                        //File.Delete(ruta2 + foto);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente el Paciente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pudo registrar el paciente ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

       
        

        private void rbt_si_Checked(object sender, RoutedEventArgs e)
        {
            if (rbt_si.IsChecked == true)
            {
                btncapturar.IsEnabled = true;
                btnencender.IsEnabled = true;
                valor_bandera = true;
                
            }
        }

        private void rbt_no_Checked(object sender, RoutedEventArgs e)
        {
            if (rbt_si.IsChecked == false)
            {
                btncapturar.IsEnabled = false;
                btnencender.IsEnabled = false;
                valor_bandera = false;
            }
        }
    }
}