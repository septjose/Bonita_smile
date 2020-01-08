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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        private bool HayDispositivos;
        private string ruta = @"E:\PortableGit\programs_c#\ftp_v1.0\ftp_camara\";
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        string antecedentes = "";
        int id_pacientes = 0;
        string foto = "";
        public Page6_Actualizar(PacienteModel paciente)
        {
            this.conexionBD = obj.conexion();
            InitializeComponent();
            
            llenar_Combo();
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
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            valor = cmbClinica.SelectedItem.ToString();
            int id_clinica = obtener_id_clinica(valor);
            PacienteModel pacienteModel = new PacienteModel();
            ClinicaModel clinicaModel = new ClinicaModel();

            pacienteModel.apellidos = txtApellidos.Text;
            pacienteModel.nombre = txtNombre.Text;
            pacienteModel.direccion = txtDireccion.Text;
            pacienteModel.telefono = txtTelefono.Text;
            pacienteModel.foto = foto;
            pacienteModel.email = txtEmail.Text;
            pacienteModel.marketing = 0;
            pacienteModel.antecedente = antecedentes;
            pacienteModel.id_paciente = id_pacientes;
            clinicaModel.id_clinica = id_clinica;
            pacienteModel.clinica = clinicaModel;
            // new Actualizar_Antecedentes(pacienteModel).ShowDialog();
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page7_Actualizar(pacienteModel); ;

            //MessageBox.Show(nombre + " " + apellidos + " " + direccion + " " + telefono + " " + email);

        }

        public int obtener_id_clinica(string nombre_sucursal)
        {
            int id = 0;
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

                    id = int.Parse(reader[0].ToString());
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
            conexionBD.Close();

            return id;
        }

       

        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
