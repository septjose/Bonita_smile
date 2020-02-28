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
    /// Lógica de interacción para Page4_Ingresar.xaml
    /// </summary>
    public partial class Page4_Ingresar : Page
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        bool bandera_online_offline = false;
        string alias_user;
        public Page4_Ingresar(string alias_user)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            InitializeComponent();
            llenar_Combo();
            this.alias_user = alias_user;
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {

            
            string nombre = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string alias = txtAlias.Text;
            string password = pwbPassword.Password;
            if (txtNombre.Text.Equals("") || txtApellido.Text.Equals("") || txtAlias.Text.Equals("") || pwbPassword.Password.Equals("") || cmbRol.SelectedIndex.Equals(-1))
            {
                System.Windows.Forms.MessageBox.Show("Falta llenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string valor = cmbRol.SelectedItem.ToString();
                int id_rol = obtener_id_rol(valor);
                

                try
                {
                    Usuarios user = new Usuarios(bandera_online_offline);
                    bool inserto = user.insertarUsuario(alias, nombre, apellidos, password, id_rol , alias_user);


                    if (inserto)
                    {
                       // System.Windows.Forms.MessageBox.Show("Se Ingreso  el Usuario", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        /*-----------------------------------------------*/
                        //user = new Usuarios(!bandera_online_offline);
                        //inserto = user.insertarUsuario(alias, nombre, apellidos, password, id_rol);

                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                            //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                            admin.Main.Content = new Page4(alias_user);
                    }
                    else
                    {
                       //System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar el Usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
