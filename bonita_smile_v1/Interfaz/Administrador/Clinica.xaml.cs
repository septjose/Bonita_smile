using bonita_smile_v1.Interfaz.Administrador;
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
    /// Lógica de interacción para Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        ObservableCollection<ClinicaModel> Gclinica;
        bool bandera_online_offline = false;
        string alias;
         string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        public Page5(string alias)
        {

            InitializeComponent();
            llenar_list_view();
            this.alias = alias;
        }
        void llenar_list_view()
        {
            //Usuarios user = new Usuarios();
            //List<UsuarioModel> items = new List<UsuarioModel>();
            //items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/

            //ObservableCollection<UsuarioModel> Gusuario;
            var clinicas = new ObservableCollection<ClinicaModel>((new Clinicas(bandera_online_offline).MostrarClinica()));

            lv_Clinica.ItemsSource = clinicas;
            //lv_aux = lv_Users;
            Gclinica = clinicas;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClinicaModel clinica = (ClinicaModel)lv_Clinica.SelectedItem;
            if (lv_Clinica.SelectedItems.Count > 0)
            {
                string id_clinica = clinica.id_clinica;
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                    admin.Main.Content = new Page5_Actualizar(clinica,alias); 
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            ClinicaModel clinica = (ClinicaModel)lv_Clinica.SelectedItem;
            Escribir_Archivo ea = new Escribir_Archivo();
            if (lv_Clinica.SelectedItems.Count > 0)
            {
                string id_clinica = clinica.id_clinica;
                string nombre_sucursal = clinica.nombre_sucursal;
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro que desea borrar la clínica "+nombre_sucursal+"? , se eliminarán todos los pacientes y sus estudios. Ya no se podrán recuperar", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Clinicas clin = new Clinicas(bandera_online_offline);
                    //OBTENER IMAGENES DE LA CLINICA CORRESPONDIENTE, INCLUYENDO SUS FOTOGRAFIAS DE PACIENTES, INCLUYENDO SU FOTOGRAFIS
                    var listaNombreArchivos = new Fotos_estudio_carpeta(false).MostrarFoto_Clinica(id_clinica);
                    bool elimino = clin.eliminarClinica(id_clinica,alias);
                    if (elimino)
                    {
                        /*-----------ELIMINAR FOTOS DE LOCAL--------------------------------------*/
                        if (listaNombreArchivos.Count == 0)
                        {
                            ea.escribir_imagen_eliminar("", @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt");
                        }
                        else
                        {
                            foreach (var nombre in listaNombreArchivos)
                            {
                                System.Windows.MessageBox.Show("escribio en archivo");

                                //PASAR LOS NOMBRES DE LOS ARCHIVOS DE LA CARPETA EN UN ARCHIVO
                                ea.escribir_imagen_eliminar(nombre, @configuracion.carpetas.ruta_eliminar_carpeta + "\\eliminar_imagen_temporal_"+alias+".txt");
                                //ELIMINAR FOTOS
                                if(File.Exists(@configuracion.carpetas.ruta_imagenes_carpeta+"\\" + nombre))
                                {
                                    File.Delete(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + nombre);
                                }
                                else
                                {

                                }
                               
                            }
                        }
                        Gclinica.Remove((ClinicaModel)lv_Clinica.SelectedItem);
                        System.Windows.Forms.MessageBox.Show("Se elimino la clinica correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        /*-------------------------------------------------------------------------*/

                        /*PARTE ONLINE*/
                        //clin = new Clinicas(!bandera_online_offline);
                        //clin.eliminarClinica(id_clinica);

                        //ELIMINA TODOS LOS ARCHIVOS DEL SERVIDOR QUE SE ENCUENTRAN EN EL ARCHIVO
                        //if(elimino)
                        //{
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

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo eliminar la  clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page5_Ingresar(alias); ;
        }
    }
}
