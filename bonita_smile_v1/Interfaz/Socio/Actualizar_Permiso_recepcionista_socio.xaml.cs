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
        string id_clinica_viejo = "";
        List<String> lista = new List<string>();
        bool bandera_online_offline = false;
        string id_usuario;
        string alias;
        public Actualizar_Permiso_recepcionista_socio(int id_rol,string alias, string nombre_sucursal,string Al,List<string>lista,string id_clinica_anterior,string id_usuario)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            this.conexionBD2 = obj.conexion(bandera_online_offline);

            InitializeComponent();
            llenar_Combo_Clinica(Al);
            
            cmbClinica.SelectedItem = nombre_sucursal;
            cmbUsuario.Text = alias;
            
            this.Al = Al;
            this.lista = lista;
            this.id_rol = id_rol;
            this.id_clinica_viejo = id_clinica_anterior;
            this.alias = alias;
            this.id_usuario = id_usuario;

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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string id_usuario = obtener_id_usuario(valor+"_"+this.id_usuario);
                string id_clinica = obtener_id_Clinica(valor2);
                Clinicas c = new Clinicas(bandera_online_offline);
                //System.Windows.MessageBox.Show(id_usuario + "     " + id_clinica);
                bool existe = new Clinicas(bandera_online_offline).Verificar_Tabla_Permisos(id_usuario);
               // System.Windows.MessageBox.Show("EL VALOR DE EXISTE ES " + existe);
                if (!existe)
                {
                    bool insertar = c.insertar_Permisos(id_usuario, id_clinica ,Al);
                    if (insertar)
                    {
                        //System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //c = new Clinicas(!bandera_online_offline);
                        //c.insertar_Permisos(id_usuario, id_clinica);
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (socio != null)
                            socio.Main4.Content = new Permisos_Recepcionista_socio(this.lista, this.Al, this.id_rol);
                        else
                        {
                          //  System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    bool inserto = c.actualizar_Permisos(id_usuario, id_clinica, id_clinica_viejo ,Al);
                    if (inserto)
                    {
                       // System.Windows.Forms.MessageBox.Show("Se Actualizo correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //c = new Clinicas(!bandera_online_offline);
                        //c.actualizar_Permisos(id_usuario, id_clinica, id_clinica_viejo);
                        Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                        if (socio != null)
                            socio.Main4.Content = new Permisos_Recepcionista_socio(this.lista, this.Al, this.id_rol);

                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            conexionBD.Close();

            return id;
        }
    }
}
