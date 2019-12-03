using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using bonita_smile_v1.Servicios;
using MySql.Data.MySqlClient;

namespace bonita_smile_v1.Interfaz.Administrador.Usuario
{
    /// <summary>
    /// Lógica de interacción para Insertar_Usuario.xaml
    /// </summary>
    public partial class Insertar_Usuario : MetroWindow
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        public Insertar_Usuario()
        {
            this.conexionBD = obj.conexion();
            InitializeComponent();
            llenar_Combo();
        }

        public void llenar_Combo()
        {
            query = "SELECT * FROM rol";

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

                    string rol = reader[1].ToString();
                    cmbRol.Items.Add(rol);

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            valor = cmbRol.SelectedItem.ToString();
            int id_rol = obtener_id_rol(valor);
            string nombre = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string alias = txtAlias.Text;
            string password = pwbPassword.Password;
           
            Usuarios user = new Usuarios();
            bool inserto = user.insertarUsuario(alias, nombre, apellidos, password, id_rol);
            if(inserto)
            {
                MessageBox.Show("si");
            }
            else
            {
                MessageBox.Show("no");
            }

        }
        public int obtener_id_rol(string descripcion)
        {
            int id = 0;
            query = "SELECT id_rol FROM rol where descripcion='" + descripcion + "'";

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

        private void cmbRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
