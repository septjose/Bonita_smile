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
        int id_rol = 0;
        bool bandera_online_offline = false;
        string clinica_anterior = "";
        public Pagina_Actualizar_Permisos(int id_rol,string alias,string nombre_sucursal,string id_clinica_anterior)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            this.conexionBD2 = obj.conexion(bandera_online_offline);
            
            InitializeComponent();
            llenar_Combo_Clinica();
            
            cmbClinica.SelectedItem = nombre_sucursal;
            cmbUsuario.Text = alias;
            this.clinica_anterior = id_clinica_anterior;
           
            this.id_rol = id_rol;

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

       
        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                valor = cmbUsuario.Text;
                valor2 = cmbClinica.SelectedItem.ToString();
                string id_usuario = obtener_id_usuario(valor);
                string id_clinica = obtener_id_Clinica(valor2);
                Clinicas c = new Clinicas(bandera_online_offline);
                System.Windows.MessageBox.Show(id_usuario + "     " + id_clinica);
                bool existe = new Clinicas(bandera_online_offline).Verificar_Tabla_Permisos(id_usuario);
                if (!existe)
                {
                    bool insertar = c.insertar_Permisos(id_usuario, id_clinica);
                    if (insertar)
                    {
                        System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c = new Clinicas(!bandera_online_offline);
                        c.insertar_Permisos(id_usuario, id_clinica);
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                            admin.Main.Content = new Pagina_Permisos(this.id_rol);
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                   
                }
                else
                {
                    bool actualizo = c.actualizar_Permisos(id_usuario, id_clinica, clinica_anterior);
                    if (actualizo)
                    {
                        System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c = new Clinicas(!bandera_online_offline);
                        c.actualizar_Permisos(id_usuario, id_clinica, clinica_anterior);
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                            admin.Main.Content = new Pagina_Permisos(this.id_rol);

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
