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

namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Page6.xaml
    /// </summary>
    public partial class Pacientes_socio : Page
    {
        ObservableCollection<PacienteModel> GPaciente;
        bool bandera_online_offline = false;
        List<string> lista = new List<string>();
        string alias = "";
        public Pacientes_socio(List<string>lista,string alias)
        {
            InitializeComponent();
            this.lista = lista;
            this.alias = alias;
            llenar_list_view(lista);
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
            string rutaArchivoEliminar = @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt";
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
                            ea.escribir_imagen_eliminar(paciente.foto, @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
                            //ELIMINAR FOTO
                            File.Delete(@"\\DESKTOP-ED8E774\bs\" + paciente.foto);
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
                                    File.Delete(@"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
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
                    //                File.Delete(@"\\DESKTOP-ED8E774\bs\" + paciente.foto);
                    //                GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                    //                System.Windows.Forms.MessageBox.Show("Se elimino el paciente correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //            }
                    //        }
                    //        else
                    //        {
                    //            Escribir_Archivo ea = new Escribir_Archivo();

                    //            ea.escribir_fotos_borrar(paciente.foto);
                    //            File.Delete(@"\\DESKTOP-ED8E774\bs\" + paciente.foto);
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


        private void Actualizar(object sender, RoutedEventArgs e)
        {
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {

                //ActualizarPaciente ap = new ActualizarPaciente(paciente);
                //ap.ShowDialog()
                string destFile = System.IO.Path.Combine();

                //Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                if (socio != null)
                {
                    socio.Main4.Content = new Actualizar_paciente_socio(paciente,this.lista,this.alias);
                }


                GPaciente.Remove((PacienteModel)lv_Paciente.SelectedItem);
                lv_Paciente.ItemsSource = null;
                lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente_socio(this.lista));


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void llenar_list_view(List<string> lista)
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente_socio(lista));

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();

            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
            if (socio != null)
            {
                socio.Main4.Content = new Ingresar_paciente_socio(this.lista,this.alias);
            }



            lv_Paciente.ItemsSource = null;
            lv_Paciente.ItemsSource = new ObservableCollection<PacienteModel>(new Servicios.Paciente(bandera_online_offline).MostrarPaciente_socio(this.lista));


        }
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
