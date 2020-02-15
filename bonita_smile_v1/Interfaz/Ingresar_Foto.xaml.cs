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
using bonita_smile_v1.Interfaz.Socio;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page8_IngresarFoto.xaml
    /// </summary>
    public partial class Page8_IngresarFoto : Page
    {
        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        private bool HayDispositivos;
        //private string ruta = @"\\DESKTOP-ED8E774\capturas\";
        string ruta;

        //private string ruta_offline = @"\\DESKTOP-ED8E774\fotos_offline\";
        string ruta_offline;
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        bool bandera_offline_online = false;
        int i;
        string NombreVideo;
          string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.cfg");
        Configuracion_Model configuracion;
        PacienteModel paciente;
        List<string> lista = new List<string>();
        string alias = "";
        public Page8_IngresarFoto(PacienteModel paciente,List<string> lista,string alias)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            //MessageBox.Show(paciente.apellidos + "  "+paciente.nombre+"  "+paciente.clinica.id_clinica.ToString()+"   "+paciente.antecedente);
            InitializeComponent();
           
            CargaDispositivos();

            //MiWebCam.SignalToStop();
            //MiWebCam = null;
            this.configuracion = configuracion;
            this.ruta = @configuracion.carpetas.ruta_fotografias_carpeta + "\\";
            System.Windows.MessageBox.Show("ghhhxhhhd"+ruta);
            this.ruta_offline = @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\";
            this.btn_capturar.IsEnabled = false;
            this.btnSiguiente.IsEnabled = false;            
            this.paciente = paciente;
            this.lista = lista;
            this.alias = alias;
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
            if (!(MiWebCam == null) && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string identificador = new Seguridad().SHA1(DateTime.Now+"");
            string foto = this.paciente.nombre + "_" + this.paciente.apellidos + "_"+identificador + ".jpg";
            foto = foto.Replace(" ", "_");

            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                /*CerrarWebCam();
                string filePath = ruta +foto;
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);*/
                System.Windows.Forms.MessageBox.Show("La camara sigue encendida no ha tomado la foto ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string filePath = filePath = ruta + foto;

               
                
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);
            }

           Servicios.Paciente paciente = new Servicios.Paciente(bandera_offline_online);
            bool inserto= paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
            if(inserto)
            {
                string destFile = System.IO.Path.Combine(ruta_offline, foto);
                string destFile2 = System.IO.Path.Combine(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" , foto);
                System.IO.File.Copy(ruta + foto, destFile, true);
                System.IO.File.Copy(ruta + foto, destFile2, true);
                if(File.Exists(ruta+foto))
                {
                    File.Delete(ruta + foto);
                }
                 
                paciente = new Servicios.Paciente(!bandera_offline_online);
                bool inserto_2 = paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
                if(inserto_2)
                {
                    System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bool subir = SubirFicheroStockFTP(foto, @configuracion.carpetas.ruta_imagenes_carpeta + "\\");
                    if (subir)
                    {
                        
                        System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6();
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                        }
                        else
                        if (socio != null)
                        {
                            socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            socio.Main4.Content = new Pacientes_socio(this.lista,this.alias);
                        }

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6();
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                        }
                        else
                        if (socio != null)
                        {
                            socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                        }
                    }
                }
                else
                {
                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                    Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                    if (admin != null)
                    {
                        admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        admin.Main.Content = new Page6();
                    }
                    else
                    if (recep != null)
                    {
                        recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                    }
                    else
                    if (socio != null)
                    {
                        socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                    }

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pudo registrar el paciente ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //bool insertarPaciente = paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);

            //if (insertarPaciente)
            //{
            //    System.Windows.Forms.MessageBox.Show("Se registro correctamente el Paciente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Test_Internet ti = new Test_Internet();
            //    if (ti.Test())
            //    {
            //        System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        bool subir = SubirFicheroStockFTP(foto, ruta);
            //        if (subir)
            //        {

            //            //bool descargo = downloadFile("ftp://jjdeveloperswdm.com/", "bonita_smile@jjdeveloperswdm.com", "bonita_smile", foto,
            //            //  @"\\DESKTOP-ED8E774\bs\" + foto, 10);
            //            string destFile = System.IO.Path.Combine(@"\\DESKTOP-ED8E774\bs\", foto);
            //            //MessageBox.Show("el valor de result es " + result);
            //            System.IO.File.Copy(ruta+foto, destFile, true);
            //            //File.Delete(ruta + foto);
            //            System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //           
            //        }
            //        else
            //        {
            //            System.Windows.Forms.MessageBox.Show("No se pudo subir la foto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }

            //    else
            //    {
            //        System.Windows.Forms.MessageBox.Show("No se pudo subir la foto por el internet ", "Error por falta de internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        string destFile = System.IO.Path.Combine(ruta_offline, foto);
            //        string destFile2 = System.IO.Path.Combine(@"\\DESKTOP-ED8E774\bs\", foto);
            //        //MessageBox.Show("el valor de result es " + result);
            //        System.IO.File.Copy(ruta + foto, destFile, true);
            //        System.IO.File.Copy(ruta + foto, destFile2, true);
            //        File.Delete(ruta + foto);
            //        System.Windows.Forms.MessageBox.Show("Se subira la foto cuando tengas internet y des click en sincronizar ", "Se guardara la foto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
            //        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            //        if (admin != null)
            //        {
            //            admin.Main.Content = new Page6();
            //        }
            //        else
            //        if (recep != null)
            //        {
            //            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
            //        }
            //    }
            //}
            //

        }
        public bool SubirFicheroStockFTP(string foto,string ruta)
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
                //System.Windows.MessageBox.Show("Error " + ex.Message + " " + ex.StackTrace);


            }
            return verdad;

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CerrarWebCam();
            this.i = comboBox1.SelectedIndex;
            this.NombreVideo = MisDispositivios[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();
            btn_encender.IsEnabled = false;
            btn_capturar.IsEnabled = true;
            btnSiguiente.IsEnabled = false;

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
            this.btn_capturar.IsEnabled = false;
            this.btnSiguiente.IsEnabled = true;
            this.btn_encender.IsEnabled = true;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btn_Omitir_Click(object sender, RoutedEventArgs e)
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                CerrarWebCam();
            }
            Servicios.Paciente paciente = new Servicios.Paciente(bandera_offline_online);
            bool inserto = paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, "", this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
            if(inserto)
            {
                paciente = new Servicios.Paciente(!bandera_offline_online);
                bool inserto_2 = paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, "", this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);

                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                {
                    admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                    admin.Main.Content = new Page6();
                }
                else
                if (recep != null)
                {
                    recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                    recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                }
                else
                if (socio != null)
                {
                    socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                    socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                }
            }

        }
    }
}
