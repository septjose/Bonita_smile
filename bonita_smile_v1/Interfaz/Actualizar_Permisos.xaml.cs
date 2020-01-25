using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Servicios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Pagina_Actualizar_Permisos.xaml
    /// </summary>
    public partial class Pagina_Actualizar_Permisos : Page
    {
        private MySqlDataReader reader = null, reader2 = null;
        private string query, query2;
        private MySqlConnection conexionBD, conexionBD2;
        Conexion obj = new Conexion();
        string valor = "", valor2 = "";
        string permiso = "";
        public Pagina_Actualizar_Permisos(string alias,string nombre_sucursal,string id_permiso)
        {
            this.conexionBD = obj.conexion();
            this.conexionBD2 = obj.conexion();
            InitializeComponent();
            llenar_Combo_Clinica();
            llenar_Combo_Usuario();
            cmbClinica.SelectedItem = nombre_sucursal;
            cmbUsuario.SelectedItem = alias;
            this.permiso = id_permiso;

        }

        public void llenar_Combo_Clinica()
        {
            query2 = "SELECT * FROM clinica";

            try
            {
                conexionBD2.Open();
                MySqlCommand cmd = new MySqlCommand(query2, conexionBD2);

                reader2 = cmd.ExecuteReader();

                while (reader2.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    string clinica = reader2[1].ToString();
                    cmbClinica.Items.Add(clinica);

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD2.Close();
        }

        public void llenar_Combo_Usuario()
        {
            query = "SELECT * FROM usuario where usuario.id_rol=2 ";

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

                    string usuario = reader[1].ToString();
                    cmbUsuario.Items.Add(usuario);

                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }
        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                valor = cmbUsuario.SelectedItem.ToString();
                valor2 = cmbClinica.SelectedItem.ToString();
                string id_usuario = obtener_id_usuario(valor);
                string id_clinica = obtener_id_Clinica(valor2);
                string id_permiso = permiso;

                // MessageBox.Show(id_usuario + "     " + id_clinica+" id_permiso=   "+ id_permiso);

                Clinicas c = new Clinicas();
                bool inserto = c.actualizar_Permisos(id_usuario, id_clinica, id_permiso);
                if (inserto)
                {
                    System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                    if (admin != null)
                        admin.Main.Content = new Page5();

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No selecciono Nada en el combobox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void cmbUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public string obtener_id_usuario(string descripcion)
        {
           string id = "";
            query = "SELECT id_usuario FROM usuario where alias='" + descripcion + "'";

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

                    id = reader[0].ToString();
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

        public string obtener_id_Clinica(string descripcion)
        {
             string id = "";
            query = "SELECT id_clinica FROM clinica where nombre_sucursal='" + descripcion + "'";

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

                    id = reader[0].ToString();
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
    }
}
