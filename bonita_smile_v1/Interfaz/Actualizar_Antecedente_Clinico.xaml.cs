using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page7_Actualizar.xaml
    /// </summary>
    public partial class Page7_Actualizar : Page
    {
        PacienteModel paciente;
        bool bandera_online_offline = false;
        string foto = "";
        string nombre_viejo = "";
        List<string> lista = new List<string>();
        string alias = "";
        Configuracion_Model configuracion;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        public Page7_Actualizar(PacienteModel paciente,string nombre_viejo,List<string> lista,string alias)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            this.configuracion = configuracion;
            InitializeComponent();
            txtAntecedentes.Text = paciente.antecedente;
            //MessageBox.Show(paciente.antecedente);
            this.paciente = paciente;
            this.nombre_viejo = nombre_viejo;
            this.foto = paciente.foto;
            this.lista = lista;
            this.alias = alias;

        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("la foto es :" + paciente.foto);

            PacienteModel paciente_nuevo = new PacienteModel();
            ClinicaModel clinica = new ClinicaModel();
            paciente_nuevo.id_paciente = this.paciente.id_paciente;
            paciente_nuevo.nombre = this.paciente.nombre;
            paciente_nuevo.apellidos = this.paciente.apellidos;
            paciente_nuevo.direccion = this.paciente.direccion;
            paciente_nuevo.telefono = this.paciente.telefono;
            paciente_nuevo.foto = this.paciente.foto;
            paciente_nuevo.imagen = null;
            paciente_nuevo.antecedente = txtAntecedentes.Text;
            paciente_nuevo.email = this.paciente.email;
            paciente_nuevo.marketing = this.paciente.marketing;
            clinica.id_clinica = this.paciente.clinica.id_clinica;
            clinica.nombre_sucursal = this.paciente.clinica.nombre_sucursal;
            clinica.color = this.paciente.clinica.color;
            paciente_nuevo.clinica = clinica;
            this.foto = this.paciente.foto;
            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
            {
                admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                admin.Main.Content = new Page8_ActualizarFoto(paciente_nuevo,null,alias);
            }
               else
                if(recep!=null)
            {
                recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                recep.Main3.Content = new Page8_ActualizarFoto(paciente_nuevo,null,alias);
            }
            else
                if (socio != null)
            {
                socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

                socio.Main4.Content = new Page8_ActualizarFoto(paciente_nuevo,this.lista,this.alias);
            }


        }
        //this.paciente.clinica.id_clinica-> esto va en recepcionista en la ventana wpf
        private void btnOmitir_Click(object sender, RoutedEventArgs e)
        {
            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt";        
                
                    
                    Paciente pa = new Paciente(bandera_online_offline);
                    bool email_correcto = new Seguridad().email_bien_escrito(this.paciente.email);
                    if (email_correcto || this.paciente.email.Equals(""))
                    {
                    string viejo = this.nombre_viejo;
                        string nuevo = this.paciente.nombre + "_" + this.paciente.apellidos;
                        if (viejo.Equals(nuevo))
                        {
                            if (this.foto.Equals(""))
                            {
                                bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica ,alias);
                                if (inserto)
                                {
                                   // System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    pa = new Paciente(!bandera_online_offline);
                                //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                            if (admin != null)
                                    {
                                        admin.Main.Content = new Page6(alias);
                                    }
                                    else
                        if (recep != null)
                                    {
                                        recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
                                    }
                            else
                        if (socio != null)
                            {
                                socio.Main4.Content = new Pacientes_socio(this.lista,this.alias);
                            }

                        }
                            }
                            else
                            {
                                bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica,alias);
                            if (inserto)
                                {
                                    //System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    pa = new Paciente(!bandera_online_offline);
                                //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                            if (admin != null)
                            {
                                admin.Main.Content = new Page6(alias);
                            }
                            else
                        if (recep != null)
                            {
                                recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
                            }
                            else
                        if (socio != null)
                            {
                                socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                            }

                        }
                            }
                        }
                        else
                        {
                            if (foto.Equals(""))
                            {
                                bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica,alias);
                            {
                                    //System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    pa = new Paciente(!bandera_online_offline);
                                //pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                            if (admin != null)
                            {
                                admin.Main.Content = new Page6(alias);
                            }
                            else
                        if (recep != null)
                            {
                                recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica,alias);
                            }
                            else
                        if (socio != null)
                            {
                                socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                            }

                        }
                            }
                            else
                            {
                        Seguridad s = new Seguridad();
                        string nombre_nuevo_foto = this.paciente.nombre + "_" + this.paciente.apellidos + "_" + this.paciente.id_paciente + ".jpg";
                                nombre_nuevo_foto = nombre_nuevo_foto.Replace(" ", "_");
                        nombre_nuevo_foto = s.quitar_acentos(nombre_nuevo_foto);
                        bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, nombre_nuevo_foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica,alias);
                            if (inserto)
                                {
                                    renombrar(this.paciente.foto, nombre_nuevo_foto);
                                    if (File.Exists(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + this.paciente.foto))
                                    {
                                     File.Delete(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + this.paciente.foto);
                                    }
                            string destFile2 = System.IO.Path.Combine(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" , nombre_nuevo_foto);
                                    System.IO.File.Copy(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre_nuevo_foto, destFile2, true);
                                    Escribir_Archivo ea = new Escribir_Archivo();
                                    ea.escribir_imagen_eliminar(this.paciente.foto, @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt" );
                                   // System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                            if (admin != null)
                            {
                                admin.Main.Content = new Page6(alias);
                            }
                            else
                        if (recep != null)
                            {
                                recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica, alias);
                            }
                            else
                        if (socio != null)
                            {
                                socio.Main4.Content = new Pacientes_socio(this.lista, this.alias);
                            }
                            //pa = new Paciente(!bandera_online_offline);
                            //bool actualizo = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, nombre_nuevo_foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
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
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Correo no válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }               
            
        }


        public void renombrar(string nombre_viejo, string nombre_nuevo)
        {
            string sourceFile;

            sourceFile = @configuracion.carpetas.ruta_imagenes_carpeta+"\\";

            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile + nombre_viejo);
            // Check if file is there  
            if (fi.Exists)
            {
                //System.Windows.MessageBox.Show("Si esta");
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(sourceFile + nombre_nuevo);
                //string destFile = System.IO.Path.Combine(@"\\DESKTOP-ED8E774\bs\", nombre_nuevo);
                //System.IO.File.Copy(@"\\DESKTOP-ED8E774\fotos_offline\" + nombre_nuevo, destFile, true);
                ///System.Windows.MessageBox.Show("se pudo si");
            }
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
                //System.Windows.MessageBox.Show("Error " + ex.Message + " " + ex.StackTrace);


            }
            return verdad;

        }
    }
}
