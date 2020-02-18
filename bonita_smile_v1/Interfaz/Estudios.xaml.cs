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
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Socio;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;

namespace bonita_smile_v1

{
    /// <summary>
    /// Lógica de interacción para Pagina_Estudios.xaml
    /// </summary>
    public partial class Pagina_Estudios : Page
    {
        ObservableCollection<Carpeta_archivosModel> GCarpetas;
        ObservableCollection<Carpeta_archivosModel> carpetas;
        Carpeta_archivosModel item_carpeta;
        string id_paciente = "";
        string id_motivo = "";
        bool bandera_online_offline = false;
        Configuracion_Model configuracion;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        public Pagina_Estudios(PacienteModel paciente, Motivo_citaModel motivo)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            InitializeComponent();
            //MessageBox.Show("El valor del id_del paciente es :" + paciente.id_paciente);
            //MessageBox.Show("El nombre del paciente es " + paciente.nombre);
            this.configuracion = configuracion;
            id_paciente = paciente.id_paciente;
            id_motivo = motivo.id_motivo;
            llenar_list_view(id_paciente);
        }

        void llenar_list_view(string id_paciente)
        {
            carpetas = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente,id_motivo));

            lvCarpetas.ItemsSource = carpetas;
            GCarpetas = carpetas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            if (lvCarpetas.SelectedItems.Count > 0)

            {
                //System.Windows.MessageBox.Show( carpeta.id_carpeta+"");
                //System.Windows.MessageBox.Show(carpeta.id_paciente + "");
                //System.Windows.MessageBox.Show("hi");
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
            }
        }


        private void lvCarpetas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            


            if (lvCarpetas.SelectedItems.Count > 0)

            {
                System.Windows.MessageBox.Show(lvCarpetas.SelectedItems.Count + "");
                // System.Windows.MessageBox.Show(carpeta.id_carpeta + "");
                // System.Windows.MessageBox.Show(carpeta.id_paciente + "");
                //System.Windows.MessageBox.Show("hi");
                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                if (admin != null)
                    admin.Main.Content = new Fotos_de_Estudios(carpeta);
                else
                if(clin!=null)
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

        private void EditZoneInfoContextMenu_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Agregar_Carpetas(id_paciente, id_motivo);
            resultado = mensaje.ShowDialog();

            lvCarpetas.ItemsSource = null;
            lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente,id_motivo));
        }


        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.item_carpeta = ((FrameworkElement)e.OriginalSource).DataContext as Carpeta_archivosModel;
            //System.Windows.MessageBox.Show(item_carpeta.nombre_carpeta);
            // System.Windows.MessageBox.Show("Clic Derecho");
            System.Windows.Controls.ContextMenu cm = this.FindResource("cmButton") as System.Windows.Controls.ContextMenu;
            cm.PlacementTarget = sender as System.Windows.Controls.Button;
            cm.IsOpen = true;
            e.Handled = true;
        }



        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show(this.item_carpeta.nombre_carpeta);
            //Carpeta_archivosModel carpeta = (Carpeta_archivosModel)lvCarpetas.SelectedItem;
            //if (lvCarpetas.SelectedIndex == -1)
            //{
            //    return;
            //}

            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @configuracion.carpetas.ruta_temporal_carpeta + "\\eliminar_imagen_temporal.txt";
            // ELIMINARLA DE LA BS LOCAL/

            // SI LA CARPETA ESTA ASOCIADA A UNA NOTA NO ELIMINARLA, DE LO CONTRARIO SI ELIMINARLA
            System.Windows.MessageBox.Show("imprimo " +  item_carpeta.id_nota );
            if (item_carpeta.id_nota.Equals("")|| item_carpeta.id_nota==null)

            {
                //NO ESTA ASOCIADA, ENTONCES SE PUEDE ELIMINAR

                //RECUPERAR NOMBRE DE ARCHIVOS DE LA CARPETA
                var listaNombreArchivos = new Fotos_estudio_carpeta(false).MostrarFoto_estudio_carpeta(this.item_carpeta.id_carpeta, id_paciente);

                //ELIMINAR REGISTRO
                bool elimino = new Carpeta_archivos(bandera_online_offline).eliminarCarpeta_archivos(this.item_carpeta.id_carpeta);
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
                            System.Windows.MessageBox.Show("RUTA PARA BORRAR EN BS " + @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre.foto_completa);
                            File.Delete(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre.foto_completa);
                            
                        }
                    }


                    lvCarpetas.ItemsSource = null;
                    lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente,id_motivo));

                    //    // ELIMINAR DEL SERVIDOR/

                    //     /****POSIBLEMENTE SE QUITE DE AQUI Y SE HACE UNICAMENTE EN EL BOTON DE SINCRONIZAR****/
                    //     //ELIMINAR REGISTRO
                    //     elimino = new Carpeta_archivos(!bandera_offline_online).eliminarCarpeta_archivos(this.item_carpeta.id_carpeta);
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
            }
            else
            {
                System.Windows.MessageBox.Show("imprimo" + item_carpeta.id_carpeta + "  " + item_carpeta.id_motivo + "  " + item_carpeta.id_nota + "  " + item_carpeta.id_paciente + "   " + item_carpeta.nombre_carpeta);
                System.Windows.MessageBox.Show("Esta carpeta esta asociada a una nota, no se puede eliminar");
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

        public List<string> nombreArchivosDirectorio(string ruta)
        {
            List<string> nombreArchivos = new List<string>();

            DirectoryInfo di = new DirectoryInfo(ruta);
            foreach (var fi in di.GetFiles())
            {
                nombreArchivos.Add(fi.Name);
            }
            return nombreArchivos;
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Actualizar_Nombre_Carpeta(item_carpeta.id_paciente, item_carpeta.id_carpeta, id_motivo);
            resultado = mensaje.ShowDialog();

            lvCarpetas.ItemsSource = null;
            lvCarpetas.ItemsSource = new ObservableCollection<Carpeta_archivosModel>(new Servicios.Carpeta_archivos(false).MostrarCarpeta_archivos_paciente(id_paciente,id_motivo));
        }
    }
}