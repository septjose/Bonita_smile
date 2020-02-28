using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;
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

namespace bonita_smile_v1.Interfaz
{
    /// <summary>
    /// Lógica de interacción para Page4_Actualizar.xaml
    /// </summary>
    public partial class Actualizar_Perfil : Page
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        public string id_usu = "";
        UsuarioModel usu;
        System.Windows.Controls.ListView lv_aux;
        bool bandera_online_offline = false;
        List<string> lista = new List<string>();
        string alias = "";
        string alias_user;
        int id_rol;
        string id;
        public Actualizar_Perfil(UsuarioModel usu,string id, List<string> lista, string alias)
        {

            this.conexionBD = obj.conexion(bandera_online_offline);
            InitializeComponent();
            this.usu = usu;
            this.lista = lista;
            this.alias = alias;
            this.alias_user = alias;
            txtAlias.Text = usu.alias;
            txtApellido.Text = usu.apellidos;
            txtNombre.Text = usu.nombre;
            pwbPassword.Password = usu.password;
            this.id = id;
            

            //System.Windows.MessageBox.Show("imprimir :" + usu.rol.descripcion);
            id_usu = usu.id_usuario;
            this.id_rol = usu.rol.id_rol;

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
                    
                    
                    string nombre = txtNombre.Text;
                    string apellidos = txtApellido.Text;
                    string alias = txtAlias.Text;
                    string password = pwbPassword.Password;
                    //System.Windows.MessageBox.Show(id_usu.ToString() + " " + nombre + " " + apellidos + " " + alias + " " + password + "" + " " + id_rol);
                    Usuarios user = new Usuarios(bandera_online_offline);
                    string pass_tabla = obtener_password(id_usu);
                    bool inserto = false;
                    if (password.Equals(pass_tabla))
                    {
                        inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol, alias_user);
                        if (inserto)
                        {

                            //System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //user = new Usuarios(!bandera_online_offline);
                            //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                            usu.alias = alias;
                            usu.apellidos = apellidos;
                            usu.id_usuario = id_usu;
                            usu.nombre = nombre;
                            usu.password = password;
                            //rolModel.id_rol = id_rol;
                            rolModel.descripcion = valor;

                            usu.rol = rolModel;
                            Clin clinica = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            if (recep != null)
                            {
                                recep.Main3.Content = new Recepcionista_Principal(id, alias + "_" + usu.id_usuario);
                            }
                            else
                            if(clinica!=null)
                            {
                                clinica.Main2.Content = new Pagina_Clinica(id, nombre + apellidos, alias + "_" + usu.id_usuario);
                            }
                                //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                
                        }
                        else
                        {
                           // System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        Seguridad secure = new Seguridad();
                        string new_pass = secure.Encriptar(password);
                        inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                        if (inserto)
                        {
                            //user = new Usuarios(!bandera_online_offline);
                            //user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol);
                            usu.alias = alias;
                            usu.apellidos = apellidos;
                            usu.id_usuario = id_usu;
                            usu.nombre = nombre;
                            usu.password = password;
                            //rolModel.id_rol = id_rol;
                            rolModel.descripcion = valor;

                            usu.rol = rolModel;

                            //System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //vu.refrescar_listview(this.usu, usu, lv_aux);
                            Clin clinica = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                            if (recep != null)
                            {
                                recep.Main3.Content = new Recepcionista_Principal(id, alias + "_" + usu.id_usuario);
                            }
                            else
                            if (clinica != null)
                            {
                                clinica.Main2.Content = new Pagina_Clinica(id, nombre + apellidos, alias + "_" + usu.id_usuario);
                            }

                        }
                        else
                        {

                           // System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            conexionBD.Close();

            return password;
        }
    }
}
