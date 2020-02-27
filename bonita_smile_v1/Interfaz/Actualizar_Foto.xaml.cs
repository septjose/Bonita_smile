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
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page8_IngresarFoto.xaml
    /// </summary>
    public partial class Page8_ActualizarFoto : Page
    {
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        private bool HayDispositivos;
        //private string ruta = @"\\DESKTOP-ED8E774\bs\";
        string ruta;
        //private string ruta2 = @"\\DESKTOP-ED8E774\capturas\";
        string ruta2;
        //private string ruta_offline = @"\\DESKTOP-ED8E774\fotos_offline\";
        string ruta_offline;
        private MySqlDataReader reader = null;
        private string query;
        string foto_vieja = "";
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        bool valor_bandera = false;
        bool bandera_online_offline = false;
        ImageBrush ib = new ImageBrush();
        BitmapImage bi = new BitmapImage();
        bool bandera_offline_online = false;
        PacienteModel paciente;
        List<string> lista = new List<string>();
        string alias = "";
        Configuracion_Model configuracion;
        public Page8_ActualizarFoto(PacienteModel paciente,List<string> lista,string alias)
        {
            //MessageBox.Show(paciente.apellidos + "  "+paciente.nombre+"  "+paciente.clinica.id_clinica.ToString()+"   "+paciente.antecedente);
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            InitializeComponent();
            this.ruta = @configuracion.carpetas.ruta_imagenes_carpeta;
            this.ruta2 = @configuracion.carpetas.ruta_fotografias_carpeta;
            this.ruta_offline = @configuracion.carpetas.ruta_subir_servidor_carpeta;
            this.configuracion = configuracion;
            CargaDispositivos();
            this.paciente = paciente;
            this.lista = lista;
            this.alias = alias;
            this.foto_vieja = paciente.foto;
            this.btncapturar.IsEnabled = false;
            this.btnSiguiente.IsEnabled = false;

            //File.Delete(ruta2 + paciente.foto);
            //System.Windows.MessageBox.Show("el paciente es " + paciente.foto);
            System.Windows.MessageBox.Show(paciente.foto);
            if(paciente.foto.Equals(""))
            {
                string ruta2 =  System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\img1.jpg");;
                rt_imagen.Fill = Imagen(ruta2);
            }
            else
            {
                System.Windows.MessageBox.Show("no esta vacio"+paciente.foto);
               // string ruta2 = "/Assets//img1.jpgimg1.jpg";
                rt_imagen.Fill = Imagen(@configuracion.carpetas.ruta_imagenes_carpeta+"\\"+ paciente.foto);                
            }
           
        }
        public ImageBrush Imagen(string filename)
        {
            string ruta2 =  System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\img1.jpg");;
            if (File.Exists(filename))
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
            bool eliminarArchivo = true;
            string rutaArchivoEliminar =@configuracion.carpetas.ruta_respaldo_carpeta+"\\eliminar_imagen_temporal_"+alias+".txt";

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
                System.Windows.Forms.MessageBox.Show("No apagó la cámara ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string filePath = "";
                Test_Internet test_i = new Test_Internet();
           
                
                    filePath = ruta2 + foto;
                
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img1.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);
                Paciente paciente = new Paciente(bandera_online_offline);

                bool actualizo = paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica ,alias);

                if(actualizo)
                {
                    if (this.foto_vieja.Equals(""))
                    {
                        string destfile_bs = @configuracion.carpetas.ruta_imagenes_carpeta+"\\" + foto;
                        System.IO.File.Copy(ruta2 + foto, destfile_bs, true);
                        string destfile_fotos = @configuracion.carpetas.ruta_subir_servidor_carpeta+"\\" + foto;
                        System.IO.File.Copy(destfile_bs, destfile_fotos, true);
                        File.Delete(ruta2 + foto);
                        //paciente = new Paciente(!bandera_online_offline);
                        //bool actualizo_again = paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
                        //if (actualizo_again)
                        //{
                        //    System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    bool subir = SubirFicheroStockFTP(foto, @configuracion.carpetas.ruta_imagenes_carpeta+"\\");
                        //    System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        //    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        //    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        //    if (admin != null)
                        //    {
                        //        admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        //        admin.Main.Content = new Page6();
                        //    }
                        //    else
                        //    if (recep != null)
                        //    {
                        //        recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        //        recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                        //    }
                        //    else
                        //    if (socio != null)
                        //    {
                        //        socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                        //        socio.Main4.Content = new Pacientes_socio(this.lista,this.alias);
                        //    }

                        //}
                        //else
                        //{
                            System.Windows.Forms.MessageBox.Show("Offline", "Offline", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                            if (admin != null)
                            {
                                admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                                admin.Main.Content = new Page6(alias);
                            }
                            else
                            if (recep != null)
                            {
                                recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                                recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica ,alias);
                            }
                            else
                            if (socio != null)
                            {
                                socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                                socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                            }
                        
                    }
                    else
                    {
                        if(File.Exists(@configuracion.carpetas.ruta_subir_servidor_carpeta+"\\"+foto_vieja))
                        {
                            File.Delete(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + foto_vieja);
                        }
                        if(File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta+"\\"+foto_vieja))
                        {
                            File.Delete(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + foto_vieja);
                        }
                        
                        string destfile_bs = @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + foto;
                        System.IO.File.Copy(ruta2 + foto, destfile_bs, true);
                        string destfile_fotos = @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + foto;
                        System.IO.File.Copy(destfile_bs, destfile_fotos, true);
                        if(File.Exists(ruta2+foto))
                        {
                            File.Delete(ruta2 + foto);
                        }
                        
                        

                        Escribir_Archivo ea = new Escribir_Archivo();
                        ea.escribir_imagen_eliminar(this.foto_vieja, @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt");
                        //paciente = new Paciente(!bandera_online_offline);
                        //bool actualizo_again = paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, foto, this.paciente.antecedente, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
                        //if (actualizo_again)
                        //{
                        //    System.Windows.Forms.MessageBox.Show("Tardaran unos minutos al subir la foto", "Espera", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    var datos = ea.leer(rutaArchivoEliminar);

                        //    foreach (string imagen in datos)
                        //    {
                        //        Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + imagen);
                        //        bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");

                        //        if (!verdad)
                        //            eliminarArchivo = false;
                        //    }

                        //    if (eliminarArchivo)
                        //    {
                        //        System.Windows.MessageBox.Show("elimino Archivo");
                        //        ea.SetFileReadAccess(rutaArchivoEliminar, false);
                        //        File.Delete(@"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
                        //        bool subir = SubirFicheroStockFTP(foto, @"\\DESKTOP-ED8E774\bs\");
                        //        System.Windows.Forms.MessageBox.Show("Se subio correctamente la foto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        //        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        //        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        //        if (admin != null)
                        //        {
                        //            admin.Main.Content = new Page6();
                        //        }
                        //        else
                        //        if (recep != null)
                        //        {
                        //            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                        //        }
                        //        else
                        //        if (socio != null)
                        //        {
                        //            socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                        //        }
                        //    }
                        //}

                        System.Windows.Forms.MessageBox.Show("Offline", "Offline", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6(alias);
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica , alias);
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
                    System.Windows.Forms.MessageBox.Show("No se pudo actualizar el paciente ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.btnencender.IsEnabled = false;
            this.btncapturar.IsEnabled = true;
            this.btnSiguiente.IsEnabled = false;

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
            this.btncapturar.IsEnabled = false;
            this.btnSiguiente.IsEnabled = true;
            this.btnencender.IsEnabled = true;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnOmitir_Click(object sender, RoutedEventArgs e)
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                CerrarWebCam();
            }

            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt";


            Paciente pa = new Paciente(bandera_online_offline);
            bool email_correcto = new Seguridad().email_bien_escrito(this.paciente.email);
          
                string viejo = this.foto_vieja;
                string nuevo = this.paciente.nombre + "_" + this.paciente.apellidos;
                if (viejo.Equals(nuevo))
                {
                    if (this.foto_vieja.Equals(""))
                    {
                        bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica , alias);
                        if (inserto)
                        {
                            System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //pa = new Paciente(!bandera_online_offline);
                            //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6(alias);
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
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
                        bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica,alias);
                        if (inserto)
                        {
                            System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //pa = new Paciente(!bandera_online_offline);
                            //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6(alias);
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
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
                else
                {
                    if (foto_vieja.Equals(""))
                    {
                        bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica ,alias);
                        {
                            System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //pa = new Paciente(!bandera_online_offline);
                            //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto_vieja, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6(alias);
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
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
                        string nombre_nuevo_foto = this.paciente.nombre + "_" + this.paciente.apellidos + "_" + this.paciente.id_paciente + ".jpg";
                        nombre_nuevo_foto = nombre_nuevo_foto.Replace(" ", "_");
                        bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, nombre_nuevo_foto, paciente.antecedente, this.paciente.email, 0, this.paciente.clinica.id_clinica , alias);
                        if (inserto)
                        {
                            renombrar(this.paciente.foto, nombre_nuevo_foto);
                            if (File.Exists(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + this.paciente.foto))
                            {
                                File.Delete(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + this.paciente.foto);
                            }
                            string destFile2 = System.IO.Path.Combine(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" , nombre_nuevo_foto);
                            System.IO.File.Copy(@configuracion.carpetas.ruta_imagenes_carpeta + "\\"+ nombre_nuevo_foto, destFile2, true);
                            Escribir_Archivo ea = new Escribir_Archivo();
                            ea.escribir_imagen_eliminar(this.paciente.foto, @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt");
                            System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (admin != null)
                        {
                            admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            admin.Main.Content = new Page6(alias);
                        }
                        else
                        if (recep != null)
                        {
                            recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
                        }
                        else
                        if (socio != null)
                        {
                            socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
                            socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                        }

                        //        pa = new Paciente(!bandera_online_offline);
                        //        bool actualizo = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, nombre_nuevo_foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                        //    if (actualizo)
                        //        {
                        //            var datos = ea.leer(rutaArchivoEliminar);

                        //            foreach (string imagen in datos)
                        //            {
                        //                Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + imagen);
                        //                bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");

                        //                if (!verdad)
                        //                    eliminarArchivo = false;
                        //            }

                        //            if (eliminarArchivo)
                        //            {
                        //                System.Windows.MessageBox.Show("elimino Archivo");
                        //                ea.SetFileReadAccess(rutaArchivoEliminar, false);
                        //                File.Delete(@"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
                        //                bool subir = SubirFicheroStockFTP(nombre_nuevo_foto, @"\\DESKTOP-ED8E774\bs\");
                        //        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        //        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                        //        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        //        if (admin != null)
                        //        {
                        //            admin.Main.Content = new Page6();
                        //        }
                        //        else
                        //    if (recep != null)
                        //        {
                        //            recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                        //        }
                        //        else
                        //    if (socio != null)
                        //        {
                        //            socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                        //        }

                        //    }
                        //}



                    }
                    }
                }

           
        }

        public void renombrar(string nombre_viejo, string nombre_nuevo)
        {
            string sourceFile;

            sourceFile = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";

            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile + nombre_viejo);
            // Check if file is there  
            if (fi.Exists)
            {
                System.Windows.MessageBox.Show("Si esta");
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(sourceFile + nombre_nuevo);
                //string destFile = System.IO.Path.Combine(@"\\DESKTOP-ED8E774\bs\", nombre_nuevo);
                //System.IO.File.Copy(@"\\DESKTOP-ED8E774\fotos_offline\" + nombre_nuevo, destFile, true);
                System.Windows.MessageBox.Show("se pudo si");
            }
        }
    }
}
