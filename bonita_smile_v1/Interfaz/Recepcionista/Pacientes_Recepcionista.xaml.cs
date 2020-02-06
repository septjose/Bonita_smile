using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Recepcionista;
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

namespace bonita_smile_v1.Interfaz.Recepcionista
{
    /// <summary>
    /// Lógica de interacción para Page6.xaml
    /// </summary>
    public partial class Pacientes_Recepcionista : Page
    {
        ObservableCollection<PacienteModel> GPaciente;
        bool bandera_online_offline = false;
        String id = "";
        public Pacientes_Recepcionista(string id)
        {
            InitializeComponent();
            llenar_list_view(id);
            this.id = id;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
                return true;
            else
                return ((item as PacienteModel).nombre.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0 || (item as PacienteModel).apellidos.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void Borrar(object sender, RoutedEventArgs e)
        {
            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @"C:\backup_bs\eliminar_imagen_temporal.txt";
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                string id_paciente = paciente.id_paciente;
                string nombre_paciente = paciente.nombre;

                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  paciente :" + nombre_paciente + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    bool elimino = new Paciente(bandera_online_offline).eliminarPaciente(id_paciente);
                    if (elimino)
                    {
                        if (paciente.foto.Equals(""))
                        {
                            new Paciente(!bandera_online_offline).eliminarPaciente(id_paciente);

                            System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                        }
                        else
                        {
                            //PASAR FOTO EN UN ARCHIVO
                            Escribir_Archivo ea = new Escribir_Archivo();
                            ea.escribir_imagen_eliminar(paciente.foto, @"C:\backup_bs\eliminar_imagen_temporal.txt");
                            //ELIMINAR FOTO
                            File.Delete(@"C:\bs\" + paciente.foto);
                            GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                            System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bool elimino_again = new Paciente(!bandera_online_offline).eliminarPaciente(id_paciente);
                            if (elimino_again)
                            {
                                var datos = ea.leer(rutaArchivoEliminar);

                                foreach (string imagen in datos)
                                {
                                    Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + imagen);
                                    bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");

                                    if (!verdad)
                                        eliminarArchivo = false;
                                }
                                if (eliminarArchivo)
                                {
                                    System.Windows.MessageBox.Show("elimino Archivo");
                                    ea.SetFileReadAccess(rutaArchivoEliminar, false);
                                    File.Delete(@"C:\backup_bs\eliminar_imagen_temporal.txt");
                                }
                            }
                            else
                            {
                                //NO HAY INTERNET, NO HACER NADA
                            }

                        }

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo eliminar la  clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    //bool elimino = clin.eliminarPaciente(id_paciente);
                    //if (elimino)
                    //{



                    //    clin = new Paciente(!bandera_online_offline);

                    //    clin.eliminarPaciente(id_paciente);
                    //    if (!paciente.foto.Equals(""))
                    //    {
                    //        if (ti.Test())
                    //        {
                    //            Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + paciente.foto);
                    //            bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");
                    //            if(verdad)
                    //            {
                    //                File.Delete(@"C:\bs\" + paciente.foto);
                    //                GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                    //                System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //            }
                    //        }
                    //        else
                    //        {
                    //            Escribir_Archivo ea = new Escribir_Archivo();

                    //            ea.escribir_fotos_borrar(paciente.foto);
                    //            File.Delete(@"C:\bs\" + paciente.foto);
                    //            GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                    //            System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                    //        System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }

                    //}
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show("No se pudo eliminar la  clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }

            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //private void Actualizar(object sender, RoutedEventArgs e)
        //{
        //    PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
        //    System.Windows.MessageBox.Show("el id de clinica es " + paciente.clinica.id_clinica);
        //    if (lv_Paciente.SelectedItems.Count > 0)
        //    {
        //        PacienteModel paciente_nuevo = new PacienteModel();
        //        ClinicaModel clinica = new ClinicaModel();
        //        paciente_nuevo.id_paciente = paciente.id_paciente;
        //        paciente_nuevo.nombre = paciente.nombre;
        //        paciente_nuevo.apellidos = paciente.apellidos;
        //        paciente_nuevo.direccion = paciente.direccion;
        //        paciente_nuevo.telefono = paciente.telefono;
        //        paciente_nuevo.foto = paciente.foto;
        //        paciente_nuevo.imagen = null;
        //        paciente_nuevo.antecedente = paciente.antecedente;
        //        paciente_nuevo.email = paciente.email;
        //        paciente_nuevo.marketing = paciente.marketing;
        //        clinica.id_clinica = paciente.clinica.id_clinica;
        //        clinica.nombre_sucursal = paciente.clinica.nombre_sucursal;
        //        clinica.color =paciente.clinica.color;
        //        paciente_nuevo.clinica = clinica;

        //        //ActualizarPaciente ap = new ActualizarPaciente(paciente);
        //        //ap.ShowDialog()
        //        string destFile = System.IO.Path.Combine();


        //        Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();

        //            if (recep != null)
        //        {
        //            recep.Main3.Content = new Actualizar_Paciente_Recepcionista(paciente_nuevo);
        //        }

        //        //GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
        //        //lv_Paciente.ItemsSource = null;
        //        //lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());


        //    }
        //    else
        //    {
        //        System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



        private void Actualizar(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {

                //ActualizarPaciente ap = new ActualizarPaciente(paciente);
                //ap.ShowDialog()
                string destFile = System.IO.Path.Combine();

                Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
               
                if (recep != null)
                {
                    recep.Main3.Content = new Actualizar_Paciente_Recepcionista(paciente, this.id);
                }


                //GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                //lv_Paciente.ItemsSource = null;
                //lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void llenar_list_view(string id_clinica)
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente_Clinica(id_clinica));

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();

                if (recep != null)
            {
                recep.Main3.Content = new Ingresar_Paciente_Recepcionista(this.id);
            }


            lv_Paciente.ItemsSource = null;
            lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente());


        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }
    }
}
