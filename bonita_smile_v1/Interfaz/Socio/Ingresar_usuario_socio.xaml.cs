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

namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Page4_Ingresar.xaml
    /// </summary>
    public partial class Ingresar_usuario_socio : Page
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        bool bandera_online_offline = false;
        List<string> lista = new List<string>();
        string alias = "";

        public Ingresar_usuario_socio(List<string> lista,string alias)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            this.lista = lista;
            this.alias = alias;
            InitializeComponent();
            llenar_Combo();
        }

        public void llenar_Combo()
        {
            query = "SELECT * FROM rol WHERE rol.descripcion != 'Socio' and rol.descripcion!='Marketing' and rol.descripcion!='Administrador' ";

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
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {

            if (txtNombre.Text.Equals("") || txtApellido.Text.Equals("") || txtAlias.Text.Equals("") || pwbPassword.Password.Equals("") || cmbRol.SelectedIndex.Equals(-1))
            {
                System.Windows.Forms.MessageBox.Show("Falta llenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                string valor = cmbRol.SelectedItem.ToString();
                int id_rol = obtener_id_rol(valor);
                string nombre = txtNombre.Text;
                string apellidos = txtApellido.Text;
                string alias = txtAlias.Text;
                string password = pwbPassword.Password;
                try
                {
                    Usuarios user = new Usuarios(bandera_online_offline);
                    bool inserto = user.insertarUsuario(alias, nombre, apellidos, password, id_rol);

                    if (inserto)
                    {
                        System.Windows.Forms.MessageBox.Show("Se Ingreso  el Usuario", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        /*-----------------------------------------------*/
                        user = new Usuarios(!bandera_online_offline);
                        inserto = user.insertarUsuario(alias, nombre, apellidos, password, id_rol);

                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (socio != null)
                            //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                            socio.Main4.Content = new Socio_usuarios(this.lista,this.alias);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar el Usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex + "");
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
                System.Windows.MessageBox.Show(ex.ToString());
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
