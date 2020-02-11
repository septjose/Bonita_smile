using AForge.Video;
using AForge.Video.DirectShow;
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para Page6_Actualizar.xaml
    /// </summary>
    public partial class Page6_Actualizar : Page
    {

        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        string antecedentes = "";
        string id_pacientes = "";
        string foto = "";
        bool bandera_online_offline = false;
        PacienteModel paciente;
        public Page6_Actualizar(PacienteModel paciente)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            InitializeComponent();
            
            llenar_Combo();
            this.paciente = paciente;
            txtApellidos.Text = paciente.apellidos;
            txtDireccion.Text = paciente.direccion;
            txtEmail.Text = paciente.email;
            txtNombre.Text = paciente.nombre;
            txtTelefono.Text = paciente.telefono;
            cmbClinica.SelectedItem = paciente.clinica.nombre_sucursal;
            antecedentes = paciente.antecedente;
            id_pacientes = paciente.id_paciente;
            foto = paciente.foto;

        }

        private void Capturar_Click(object sender, RoutedEventArgs e)
        {

        }

        public void llenar_Combo()
        {
            query = "SELECT * FROM clinica";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    string clinica = reader[1].ToString();
                    cmbClinica.Items.Add(clinica);

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
        

        
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    valor = cmbClinica.SelectedItem.ToString();
                    string id_clinica = obtener_id_clinica(valor);
                    PacienteModel pacienteModel = new PacienteModel();
                    ClinicaModel clinicaModel = new ClinicaModel();
                    bool email_correcto = new Seguridad().email_bien_escrito(txtEmail.Text);
                    if (email_correcto)
                    {
                        if(new Seguridad().ValidarTelefonos7a10Digitos(txtTelefono.Text))
                        {
                            string tel = txtTelefono.Text;
                            pacienteModel.apellidos = txtApellidos.Text;
                            pacienteModel.nombre = txtNombre.Text;
                            pacienteModel.direccion = txtDireccion.Text;
                            pacienteModel.telefono = tel;
                            pacienteModel.foto = foto;
                            pacienteModel.imagen = null;
                            pacienteModel.email = txtEmail.Text;
                            pacienteModel.marketing = 0;
                            pacienteModel.antecedente = antecedentes;
                            pacienteModel.id_paciente = id_pacientes;
                            clinicaModel.id_clinica = id_clinica;
                            pacienteModel.clinica = clinicaModel;
                            string nombres_viejo = this.paciente.nombre + "_" + this.paciente.apellidos;
                            // new Actualizar_Antecedentes(pacienteModel).ShowDialog();
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            if (admin != null)
                                admin.Main.Content = new Page7_Actualizar(pacienteModel, nombres_viejo, null, ""); ;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono debe de tener 10 digitos", "Teléfono no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Correo no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No selecciono el comboBox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
             

            //MessageBox.Show(nombre + " " + apellidos + " " + direccion + " " + telefono + " " + email);

        }

        public string obtener_id_clinica(string nombre_sucursal)
        {
            string id = "";
            query = "SELECT id_clinica FROM clinica where nombre_sucursal='" + nombre_sucursal + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    id =reader[0].ToString();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            conexionBD.Close();

            return id;
        }

       

        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool eliminarArchivo = true;
            string rutaArchivoEliminar = @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt";

            if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    valor = cmbClinica.SelectedItem.ToString();
                    string id_clinica = obtener_id_clinica(valor);
                    Paciente pa = new Paciente(bandera_online_offline);
                    bool email_correcto = new Seguridad().email_bien_escrito(txtEmail.Text);
                    if (email_correcto)
                    {
                        if(new Seguridad().ValidarTelefonos7a10Digitos(txtTelefono.Text))
                        {
                            string tel = txtTelefono.Text;
                            string viejo = this.paciente.nombre + "_" + this.paciente.apellidos;
                            string nuevo = txtNombre.Text + "_" + txtApellidos.Text;
                            if (viejo.Equals(nuevo))
                            {
                                if (foto.Equals(""))
                                {
                                    bool inserto = pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                    if (inserto)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        pa = new Paciente(!bandera_online_offline);
                                        pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                        if (admin != null)
                                        {
                                            admin.Main.Content = new Page6();
                                        }

                                    }
                                }
                                else
                                {
                                    bool inserto = pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                    if (inserto)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        pa = new Paciente(!bandera_online_offline);
                                        pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                        if (admin != null)
                                        {
                                            admin.Main.Content = new Page6();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (foto.Equals(""))
                                {
                                    bool inserto = pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                    if (inserto)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        pa = new Paciente(!bandera_online_offline);
                                        pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                        if (admin != null)
                                        {
                                            admin.Main.Content = new Page6();
                                        }

                                    }
                                }
                                else
                                {
                                    string nombre_nuevo_foto = txtNombre.Text + "_" + txtApellidos.Text + "_" + id_pacientes + ".jpg";
                                    nombre_nuevo_foto = nombre_nuevo_foto.Replace(" ", "_");
                                    bool inserto = pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, nombre_nuevo_foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                    if (inserto)
                                    {
                                        renombrar(this.paciente.foto, nombre_nuevo_foto);
                                        if (File.Exists(@"\\DESKTOP-ED8E774\fotos_offline\" + this.paciente.foto))
                                        {
                                            File.Delete(@"\\DESKTOP-ED8E774\fotos_offline\" + this.paciente.foto);
                                        }
                                        string destFile2 = System.IO.Path.Combine(@"\\DESKTOP -ED8E774\fotos_offline\", nombre_nuevo_foto);
                                        System.IO.File.Copy(@"\\DESKTOP-ED8E774\bs\" + nombre_nuevo_foto, destFile2, true);
                                        Escribir_Archivo ea = new Escribir_Archivo();
                                        ea.escribir_imagen_eliminar(foto, @"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
                                        System.Windows.Forms.MessageBox.Show("Se actualizo el Paciente", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // pa = new Paciente(!bandera_online_offline);
                                        //bool actualizo= pa.actualizarPaciente(id_pacientes, txtNombre.Text, txtApellidos.Text, txtDireccion.Text, tel, nombre_nuevo_foto, antecedentes, txtEmail.Text, 0, id_clinica);
                                        // if(actualizo)
                                        // {
                                        //     var datos = ea.leer(rutaArchivoEliminar);

                                        //     foreach (string imagen in datos)
                                        //     {
                                        //         Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/" + imagen);
                                        //         bool verdad = DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");

                                        //         if (!verdad)
                                        //             eliminarArchivo = false;
                                        //     }

                                        //     if (eliminarArchivo)
                                        //     {
                                        //         System.Windows.MessageBox.Show("elimino Archivo");
                                        //         ea.SetFileReadAccess(rutaArchivoEliminar, false);
                                        //         File.Delete(@"\\DESKTOP-ED8E774\backup_bs\eliminar_imagen_temporal.txt");
                                        //         bool subir = SubirFicheroStockFTP(nombre_nuevo_foto, @"\\DESKTOP-ED8E774\bs\");

                                        //     }
                                        // }



                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono debe de tener 10 digitos", "Teléfono no válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                         
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Correo no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No selecciono el comboBox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (txtNombre.Text.Equals("") || txtApellidos.Text.Equals("") || txtDireccion.Text.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }       
        }

        public void renombrar (string nombre_viejo, string nombre_nuevo)
        {
            string sourceFile;
        
                sourceFile = @"\\DESKTOP-ED8E774\bs\";

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
                System.Windows.MessageBox.Show("se pudo bitches");
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
