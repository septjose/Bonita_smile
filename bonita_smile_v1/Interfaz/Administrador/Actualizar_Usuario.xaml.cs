using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
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
    /// Lógica de interacción para Page4_Actualizar.xaml
    /// </summary>
    public partial class Page4_Actualizar : Page
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        public string id_usu = "";
        UsuarioModel usu;
        System.Windows.Controls.ListView lv_aux;

        public Page4_Actualizar(UsuarioModel usu, System.Windows.Controls.ListView lv_aux)
        {

            this.conexionBD = obj.conexion();
            InitializeComponent();
            this.usu = usu;
            this.lv_aux = lv_aux;
            txtAlias.Text = usu.alias;
            txtApellido.Text = usu.apellidos;
            txtNombre.Text = usu.nombre;
            pwbPassword.Password = usu.password;
            cmbRol.SelectedItem = usu.rol.descripcion;
            
            //System.Windows.MessageBox.Show("imprimir :" + usu.rol.descripcion);
            id_usu = usu.id_usuario;
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
                System.Windows.MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text.Equals("") || txtApellido.Text.Equals("") || txtAlias.Text.Equals("") || pwbPassword.Password.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Falta llenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Ventana_Usuario vu = new Ventana_Usuario();
                    UsuarioModel usu = new UsuarioModel();
                    RolModel rolModel = new RolModel();
                    valor = cmbRol.SelectedItem.ToString();
                    int id_rol = obtener_id_rol(valor);
                    string nombre = txtNombre.Text;
                    string apellidos = txtApellido.Text;
                    string alias = txtAlias.Text;
                    string password = pwbPassword.Password;
                    //System.Windows.MessageBox.Show(id_usu.ToString() + " " + nombre + " " + apellidos + " " + alias + " " + password + "" + " " + id_rol);
                    Usuarios user = new Usuarios();
                    string pass_tabla = obtener_password(id_usu);
                    bool inserto = false;
                    if (password.Equals(pass_tabla))
                    {
                        inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                        if (inserto)
                        {
                            System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            usu.alias = alias;
                            usu.apellidos = apellidos;
                            usu.id_usuario = id_usu;
                            usu.nombre = nombre;
                            usu.password = password;
                            rolModel.id_rol = id_rol;
                            rolModel.descripcion = valor;

                            usu.rol = rolModel;

                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            if (admin != null)
                                //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                admin.Main.Content = new Page4();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        Seguridad secure = new Seguridad();
                        string new_pass = secure.Encriptar(password);
                        inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol);
                        if (inserto)
                        {
                            usu.alias = alias;
                            usu.apellidos = apellidos;
                            usu.id_usuario = id_usu;
                            usu.nombre = nombre;
                            usu.password = password;
                            rolModel.id_rol = id_rol;
                            rolModel.descripcion = valor;

                            usu.rol = rolModel;

                            System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //vu.refrescar_listview(this.usu, usu, lv_aux);
                            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                            if (admin != null)
                                //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                admin.Main.Content = new Page4();

                        }
                        else
                        {

                            System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No selecciono Nada en el combobox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (txtNombre.Text.Equals("") || txtApellido.Text.Equals("") || txtAlias.Text.Equals("") || pwbPassword.Password.Equals(""))
                    {
                        System.Windows.Forms.MessageBox.Show("Falta llenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        public string obtener_password(string id_usuario)
        {
            string password = "";
            query = "SELECT password FROM usuario where id_usuario='" + id_usuario + "'";

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

                    password = reader[0].ToString();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            conexionBD.Close();

            return password;
        }
    }
}
