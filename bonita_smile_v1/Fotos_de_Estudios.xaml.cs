//verificar cuando una imagen no existe en la carpeta fisica....muestra una temporal en l aplicacion, error al seleccionarla(solucion:poderla eliminar, pero solo de la base de datos)
//POSIBLE ERROR DENTRO DE SERVICIO  foto_estudio_carpeta metodo loadImage

//modificar servicios

//pasar constructor escribir
//pasar delete ftp
//pasar LOADimage modificado
//pasar imagen para rellenar rectangulo

using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using bonita_smile_v1.Servicios;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Fotos_de_Estudios.xaml
    /// </summary>string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
    public partial class Fotos_de_Estudios : Page
    {
        //ObservableCollection<Imagen> GPaciente;
        string id_carpeta = "";
        string id_paciente = "";
        string ruta = "";
        bool bandera_online_offline = false;

          string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");

        Fotos_estudio_carpetaModel item_foto_carpeta;
        ObservableCollection<Fotos_estudio_carpetaModel> fotos;
        ObservableCollection<Fotos_estudio_carpetaModel> GFotos;
        Configuracion_Model configuracion;
        string alias;
        void llenar_list_view(string id_carpeta, string id_paciente)
        {
            Fotos_estudio_carpeta f_estudio = new Fotos_estudio_carpeta(bandera_online_offline);
            List<Fotos_estudio_carpetaModel> lista = f_estudio.MostrarFoto_estudio_carpeta(id_carpeta, id_paciente);

            var fotografos = new ObservableCollection<Fotos_estudio_carpetaModel>(lista);
            for (int i = 0; i < lista.Count; i++)
            {
                //lb_imagen.Items.Add(lista[i].foto);
                // MessageBox.Show("Lista es foto"+lista[i].foto);
                lb_imagen.Items.Add(fotografos[i]);
            }

            //fotos = new ObservableCollection<Fotos_estudio_carpetaModel>(new Servicios.Fotos_estudio_carpeta(bandera_online_offline).MostrarFoto_estudio_carpeta(id_carpeta, id_paciente));

            //lb_imagen.ItemsSource = fotos;
            //GFotos = fotos;

            //lb_imagen.ItemsSource = lista;

        }
        public Fotos_de_Estudios(Carpeta_archivosModel carpeta,string alias)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            Fotos_estudio_carpeta f_e_c = new Fotos_estudio_carpeta(bandera_online_offline);
            //f_e_c.fotos(carpeta.id_carpeta, carpeta.id_paciente);

            InitializeComponent();
            this.configuracion = configuracion;
            llenar_list_view(carpeta.id_carpeta, carpeta.id_paciente);
            id_carpeta = carpeta.id_carpeta;
            id_paciente = carpeta.id_paciente;
            this.alias = alias;
            //llenar_list_view2();
        }

        /* void llenar_list_view2()
         {
             List<Imagen> lista = new List<Imagen>();
             Imagen img1 = new Imagen();
             img1.foto = @"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_0.jpg";
             img1.imagen = LoadImage(@"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_0.jpg");
             Imagen img2 = new Imagen();
             img2.foto = @"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_1.jpg";
             img2.imagen = LoadImage(@"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_1.jpg");
             Imagen img3 = new Imagen();
             img3.foto = @"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_2.jpg";
             img3.imagen = LoadImage(@"\\DESKTOP-ED8E774\bs\Estudios_Brackets_imagen_2.jpg");
             lista.Add(img1);
             lista.Add(img2);
             lista.Add(img3);
             var pacientes = new ObservableCollection<Imagen>(lista);
             for (int i = 0; i < lista.Count; i++)
             {
                 //lb_imagen.Items.Add(lista[i].foto);
                 lb_imagen.Items.Add(pacientes[i]);
             }
             //lb_imagen.ItemsSource = lista;
             //GPaciente = pacientes;
         }*/
        private void lb_imagen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_imagen.SelectedItem != null)
            {

                Fotos_estudio_carpetaModel selectedOffer = (lb_imagen.SelectedItem as Fotos_estudio_carpetaModel);
                if (selectedOffer != null)
                {
                    //MessageBox.Show(selectedOffer.foto);
                    //System.Windows.MessageBox.Show(selectedOffer.foto_completa);
                    //Imagen(@"\\DESKTOP-ED8E774\bs\" + selectedOffer.foto);
                    //DialogResult resultado = new DialogResult();
                    this.ruta = @configuracion.carpetas.ruta_imagenes_carpeta + "\\" + selectedOffer.foto_completa;
                    Imagen(ruta);
                    // Form mensaje = new Form1(@"\\DESKTOP-ED8E774\bs\" + selectedOffer.foto);
                    //resultado = mensaje.ShowDialog();
                }
                //ruta = lista.SelectedItem.ToString();
                //Imagen(ruta);


            }
        }

        public void Imagen(string ruta)
        {
            var bitmap = new Fotos_estudio_carpeta(false).LoadImage(ruta);

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = bitmap;
            rt_imagen.Fill = ib;
        }

        private void lb_imagen_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            //Comprobamos el tipo de dato que se arrastra
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                e.Effects = System.Windows.DragDropEffects.All;
            else
                e.Effects = System.Windows.DragDropEffects.None;
        }

        private void lb_imagen_Drop(object sender, System.Windows.DragEventArgs e)
        {
            bool inserto = false;
            string result = "";
            string destFile;
            Test_Internet ti = new Test_Internet();

            string extension = "";
            Fotos_estudio_carpetaModel f = new Fotos_estudio_carpetaModel();
            //Recuperamos la lista de los elementos arrastrados y y los añadimos a la lista
            string[] s = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
            int i;

            for (i = 0; i < s.Length; i++)
            {
                extension = System.IO.Path.GetExtension(s[i]);
                if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".JPG") || extension.Equals(".PNG"))
                {
                    result = System.IO.Path.GetFileName(s[i]);

                    // ---------ACOMODAR ESTO-------- -/
                     //SUBIRLO TODO LOCAL
                     //REALIZAR INSERCION DEL REGISTRO
                     Fotos_estudio_carpeta fotos = new Fotos_estudio_carpeta(bandera_online_offline);
                    bool insertar_foto = fotos.insertarFoto_estudio_carpeta(id_carpeta, id_paciente, id_carpeta + "_" + result,alias);
                    //SI SE INSERTA PROCEDER A PASAR LA IMAGEN A CARPETA BS Y BS_OFFLINE
                    if (insertar_foto)
                    {
                        try
                        {
                            destFile = System.IO.Path.Combine(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\", result);
                            File.Copy(s[i], destFile, true);
                            renombrar(false, result, id_carpeta + "_" + result);

                            destFile = System.IO.Path.Combine(@configuracion.carpetas.ruta_imagenes_carpeta + "\\", result);
                            File.Copy(s[i], destFile, true);
                            renombrar(true, result, id_carpeta + "_" + result);
                        }
                        catch (Exception ex)
                        {
                           // System.Windows.MessageBox.Show(ex + "");
                            // ----------eliminar fotos insertadas en las carpetas y eliminar el registro de la base de datos-------------/
                            //revisar paridad entre carpetas offline y bs. si esta en offline forzosamente debe de estar en bs, de lo contrario eliminar imagen de offline =====> PROBABLEMENTE TAMBIÉN VERIFICAR ESTO AL ENTRAR A ESTE PAGE(INTERFAZ)
                            //POSIBLE ERROR : Que no exista una de las carpetas o servicio ocupado
                            //eliminar registro de BS Local
                           
                            System.Windows.Forms.MessageBox.Show("La imagen ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //SUBIR TODO AL SERVIDOR
                        //REALZAR INSERCION DEL REGISTRO EN EL SERVIDOR
                        //fotos = new Fotos_estudio_carpeta(!bandera_online_offline);
                        //insertar_foto = fotos.insertarFoto_estudio_carpeta(id_carpeta, id_paciente, id_carpeta + "_" + result);

                        if (insertar_foto)
                        {

                            //System.Windows.MessageBox.Show("ENTRO PARA SUBIR FOTO A SERVIDOR");
                            //PROCEDER A MIGRAR LA IMAGEN POR FTP
                            //inserto = SubirFicheroStockFTP(id_carpeta + "_" + result, s[i]);
                            /*if (inserto)
                            {
                                //ELIMINAR FOTO QUE SE SUBIO AL SERVIDOR DE CARPETA OFFLINE
                                if(File.Exists(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\"+ id_carpeta + "_" + result))
                                {
                                    File.Delete(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + id_carpeta + "_" + result);
                                }
                                
                            }*/
                        }
                        else
                        {
                            //NO HAY INTERNET, NO HACER NADA
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Formato No valido solo acepta PNG y JPG "+ s[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            lb_imagen.Items.Clear();

            Fotos_estudio_carpeta f_estudio = new Fotos_estudio_carpeta(bandera_online_offline);
            List<Fotos_estudio_carpetaModel> lista = f_estudio.MostrarFoto_estudio_carpeta(id_carpeta, id_paciente);

            var fotografos = new ObservableCollection<Fotos_estudio_carpetaModel>(lista);
            for (int j = 0; j < lista.Count; j++)
            {
                lb_imagen.Items.Add(fotografos[j]);
            }
        }

        public void renombrar_offline(bool online, string nombre_viejo, string nombre_nuevo)
        {

            string sourceFile = @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + nombre_viejo;
            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
            // Check if file is there  
            if (fi.Exists)
            {
                //System.Windows.MessageBox.Show("Si esta");
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + nombre_nuevo);
                string destFile = System.IO.Path.Combine(@configuracion.carpetas.ruta_imagenes_carpeta + "\\", nombre_nuevo);
                System.IO.File.Copy(@configuracion.carpetas.ruta_subir_servidor_carpeta + "\\" + nombre_nuevo, destFile, true);
                //System.Windows.MessageBox.Show("se pudo si");
            }
        }
        public void renombrar(bool online, string nombre_viejo, string nombre_nuevo)
        {
            string sourceFile;
            if (!online)
                sourceFile = @configuracion.carpetas.ruta_subir_servidor_carpeta + "\\";
            else
                sourceFile = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";

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
                //System.Windows.MessageBox.Show("se pudo si");
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

                // string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                //      ruta,
                //       foto);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(
                        "bonita_smile@jjdeveloperswdm.com",
                        "bonita_smile");

                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var fileStream = File.OpenRead(ruta))
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

        private void lb_imagen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Visualizador_Imagenes(this.ruta);
            resultado = mensaje.ShowDialog();
        }
        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.item_foto_carpeta = ((FrameworkElement)e.OriginalSource).DataContext as Fotos_estudio_carpetaModel;
            System.Windows.Controls.ContextMenu cm = this.FindResource("cmButton") as System.Windows.Controls.ContextMenu;
            cm.PlacementTarget = sender as System.Windows.Controls.Button;
            cm.IsOpen = true;
            e.Handled = true;
        }



        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt";
            // ELIMINARLA DE LA BS LOCAL/

             //ELIMINAR REGISTRO
             bool elimino = new Fotos_estudio_carpeta(bandera_online_offline).eliminarFoto_estudio_carpeta(this.item_foto_carpeta.id_foto,alias);
            //ELIMINAR IMAGEN
            File.Delete(item_foto_carpeta.foto_completa);
            if (elimino)
            {
                //PASAR FOTO EN UN ARCHIVO
                Escribir_Archivo ea = new Escribir_Archivo();
                ea.escribir_imagen_eliminar(this.item_foto_carpeta.foto_completa, @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt");
                //ELIMINAR FOTO
                item_foto_carpeta.imagen = null;
                rt_imagen.Fill = null;
                if(File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + item_foto_carpeta.foto_completa))
                {
                    File.Delete(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + item_foto_carpeta.foto_completa);
                }
                
                lb_imagen.Items.Remove(item_foto_carpeta);

                // ELIMINAR DEL SERVIDOR/
                 //ELIMINAR REGISTRO

                 /****POSIBLEMENTE SE QUITE DE AQUI Y SE HACE UNICAMENTE EN EL BOTON DE SINCRONIZAR****/
                // elimino = new Fotos_estudio_carpeta(!bandera_online_offline).eliminarFoto_estudio_carpeta(this.item_foto_carpeta.id_foto);
                //if (elimino)
                //{
                //    //ELIMINAR FOTO DE SERVIDOR, OBTENIENDO NOMBRE DEL ARCHIVO
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
                //    //NO HAY INTERNET, NO HACER NADA
                //}
                ///**********************************/
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
    }
}