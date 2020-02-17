using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Socio;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1.Interfaz.Clinica
{
    /// <summary>
    /// Lógica de interacción para Page2_notas.xaml
    /// </summary>
    public partial class Notas_recepcionista : Page
    {
        ObservableCollection<Nota_de_digi_evolucionModel> GNotas;
        PacienteModel paciente;
        Motivo_citaModel motivo;
        string id_motivo = "";
        string id_paciente = "";
        bool bandera_online_offline = false;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        Configuracion_Model configuracion;
        public Notas_recepcionista(PacienteModel paciente, Motivo_citaModel motivo)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            InitializeComponent();
            rt_imagen.Fill = new Page2().Imagen(@configuracion.carpetas.ruta_imagenes_carpeta + "\\"+paciente.foto);
            this.paciente = paciente;
            this.motivo = motivo;
            txtNombre.Text = paciente.nombre + " " + paciente.apellidos;
            txtNombre.IsEnabled = false;
            txtMotivo.Text = motivo.descripcion;
            txtMotivo.IsEnabled = false;
            this.configuracion = configuracion;
           
            //lblmotivo.Content = motivo.descripcion;
            //lblTotal.Content = motivo.costo.ToString();
            Abonos abono = new Abonos(bandera_online_offline);
            
            //lblAbonado.Content = abono.Abonados(motivo.id_motivo).ToString();
            //lblRestante.Content = 
            //System.Windows.MessageBox.Show(motivo.id_motivo.ToString() + "  " + paciente.id_paciente.ToString());
            id_motivo = motivo.id_motivo;
            id_paciente = paciente.id_paciente;
            llenar_list_view(motivo.id_motivo, paciente.id_paciente);

        }

        void llenar_list_view(string id_motivo, string id_paciente)
        {
            var notas = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo, id_paciente));

            lvNotas.ItemsSource = notas;
            GNotas = notas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Agregar_Nota_Evolucion(motivo.id_motivo, motivo.descripcion, paciente.id_paciente);
            resultado = mensaje.ShowDialog();
            this.GNotas = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo, id_paciente));
            lvNotas.ItemsSource = GNotas;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();

            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
            {
                admin.Main.Content = new Pagina_Estudios(paciente, motivo);
            }

            else
            if (clin != null)
            {
                clin.Main2.Content = new Pagina_Estudios(paciente, motivo);
            }
            else
            if (socio != null)
            {
                socio.Main4.Content = new Pagina_Estudios(paciente, motivo);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();

            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
            {
                admin.Main.Content = new Page2_Abonos(paciente, motivo);
            }
            else
            if (clin != null)
            {
                clin.Main2.Content = new Page2_Abonos(paciente, motivo);
            }
            else
            if (socio != null)
            {
                socio.Main4.Content = new Page2_Abonos(paciente, motivo);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @configuracion.carpetas.ruta_temporal_carpeta + "\\eliminar_imagen_temporal.txt";
            Nota_de_digi_evolucionModel nota = (Nota_de_digi_evolucionModel)lvNotas.SelectedItem;

            var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el motivo :" + nota.descripcion + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (confirmation == System.Windows.Forms.DialogResult.Yes)
            {

                //ELIMINAR PRIMERO LO REFERENTE A LA CARPETA

                //RECUPERAR NOMBRE DE ARCHIVOS DE LA CARPETA
                var carpeta = new Carpeta_archivos(false).carpetaArchivos(nota.id_nota);
                var listaNombreArchivos = new Fotos_estudio_carpeta(false).MostrarFoto_estudio_carpeta(carpeta.id_nota, id_paciente);

                //ELIMINAR REGISTRO
                bool elimino = new Carpeta_archivos(bandera_online_offline).eliminarCarpeta_archivos(carpeta.id_carpeta);
                if (elimino)
                {
                    System.Windows.MessageBox.Show("llego aqio");

                    Escribir_Archivo ea = new Escribir_Archivo();
                    if (listaNombreArchivos.Count == 0)
                    {
                        ea.escribir_imagen_eliminar("", @configuracion.carpetas.ruta_temporal_carpeta + "\\eliminar_imagen_temporal.txt");
                    }
                    else
                    {

                        foreach (var nombre in listaNombreArchivos)
                        {
                            System.Windows.MessageBox.Show("escribio en archivo");

                            //PASAR LOS NOMBRES DE LOS ARCHIVOS DE LA CARPETA EN UN ARCHIVO
                            ea.escribir_imagen_eliminar(nombre.foto_completa, @configuracion.carpetas.ruta_temporal_carpeta + "\\eliminar_imagen_temporal.txt");
                            //ELIMINAR FOTOS
                            if (File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre.foto_completa))
                            {
                                File.Delete(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre.foto_completa);
                            }
                            
                        }
                    }

                    ////ELIMINAR DEL SERVIDOR/

                    ///****POSIBLEMENTE SE QUITE DE AQUI Y SE HACE UNICAMENTE EN EL BOTON DE SINCRONIZAR****/
                    ////ELIMINAR REGISTRO
                    //elimino = new Carpeta_archivos(!bandera_online_offline).eliminarCarpeta_archivos(carpeta.id_carpeta);
                    //if (elimino)
                    //{
                    //    //ELIMINAR FOTOS DE SERVIDOR, OBTENIENDO NOMBRE DEL ARCHIVO
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
                    //    }
                    //}
                    //else
                    //{
                    //    //SI NO HAY INTERNET, NO HACER NADA
                    //}
                    ///**********************************/
                }

                //ELIMINAR DESPUES TODO LO REFERENTE A LA NOTA
                Nota_de_digi_evolucion mot = new Nota_de_digi_evolucion(bandera_online_offline);

                elimino = mot.eliminarNotaEvolucion(nota.id_nota, paciente.id_paciente, motivo.id_motivo);
                if (elimino)
                {
                    mot = new Nota_de_digi_evolucion(!bandera_online_offline);
                    mot.eliminarNotaEvolucion(nota.id_nota, paciente.id_paciente, motivo.id_motivo);
                    // mot.eliminarMotivo_cita(motivo.id_motivo,motivo.paciente.id_paciente);
                    GNotas.Remove((Nota_de_digi_evolucionModel)lvNotas.SelectedItem);
                    System.Windows.Forms.MessageBox.Show("Se elimino el motivo correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Nota_de_digi_evolucionModel nota = (Nota_de_digi_evolucionModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                DialogResult resultado = new DialogResult();
                Form mensaje = new Actualizar_Nota_Evolucion(nota);
                resultado = mensaje.ShowDialog();
                System.Windows.MessageBox.Show(nota.fecha);
                this.GNotas = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo, id_paciente));
                System.Windows.MessageBox.Show(GNotas[0].fecha);
                lvNotas.ItemsSource = GNotas;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (WebException e)
            {
                FtpWebResponse response = (FtpWebResponse)e.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return true;
                }
                return false;
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Nota_de_digi_evolucionModel nota = (Nota_de_digi_evolucionModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                Carpeta_archivosModel carpeta = new Carpeta_archivosModel();
                carpeta.id_carpeta = nota.carpeta.id_carpeta;
                carpeta.nombre_carpeta = nota.carpeta.nombre_carpeta;
                carpeta.id_paciente = nota.id_paciente;
                carpeta.id_motivo = nota.id_motivo;
                carpeta.id_nota = nota.id_nota;

                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                if (admin != null)
                    admin.Main.Content = new Fotos_de_Estudios(carpeta);
                else
                if (clin != null)
                {

                    clin.Main2.Content = new Fotos_de_Estudios(carpeta);
                }
                else
                if (socio != null)
                {

                    socio.Main4.Content = new Fotos_de_Estudios(carpeta);
                }


            }
        }
    }
}
