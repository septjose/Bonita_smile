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
    /// Lógica de interacción para Pagina_Actualizar_Permisos.xaml
    /// </summary>
    public partial class Actualizar_Permiso_recepcionista_socio : Page
    {
        private MySqlDataReader reader = null, reader2 = null;
        private string query, query2;
        private MySqlConnection conexionBD, conexionBD2;
        Conexion obj = new Conexion();
        string valor = "", valor2 = "";
        string permiso = "";
        string Al = "";
        int id_rol = 0;
        List<String> lista = new List<string>();
        bool bandera_online_offline = false;
        public Actualizar_Permiso_recepcionista_socio(int id_rol,string alias, string nombre_sucursal, string id_permiso,string Al,List<string>lista)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            this.conexionBD2 = obj.conexion(bandera_online_offline);

            InitializeComponent();
            llenar_Combo_Clinica(Al);
            llenar_Combo_Usuario(id_rol);
            cmbClinica.SelectedItem = nombre_sucursal;
            cmbUsuario.SelectedItem = alias;
            this.permiso = id_permiso;
            this.Al = Al;
            this.lista = lista;
            this.id_rol = id_rol;

        }

        public void llenar_Combo_Clinica(string alias)
        {
            query2 = "select clinica.id_clinica, clinica.nombre_sucursal from usuario left join permisos on usuario.id_usuario=permisos.id_usuario inner join clinica on clinica.id_clinica=permisos.id_clinica where usuario.alias='" + alias + "'";


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

        public void llenar_Combo_Usuario(int id_rol)
        {
            query = "SELECT * FROM usuario where usuario.id_rol="+id_rol+"";

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

                Clinicas c = new Clinicas(bandera_online_offline);
                bool inserto = c.actualizar_Permisos(id_usuario, id_clinica, id_permiso);
                if (inserto)
                {
                    System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c = new Clinicas(!bandera_online_offline);
                    c.actualizar_Permisos(id_usuario, id_clinica, id_permiso);
                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                    if (socio != null)
                        socio.Main4.Content = new Permisos_Recepcionista_socio(this.lista,this.Al,this.id_rol);

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
