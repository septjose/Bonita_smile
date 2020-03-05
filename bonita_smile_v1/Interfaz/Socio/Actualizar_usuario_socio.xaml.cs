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

namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Page4_Actualizar.xaml
    /// </summary>
    public partial class Actualizar_usuario_socio : Page
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
        public Actualizar_usuario_socio(UsuarioModel usu,List<string> lista,string alias)
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
            cmbRol.SelectedItem = usu.rol.descripcion;

            //System.Windows.MessageBox.Show("imprimir :" + usu.rol.descripcion);
            id_usu = usu.id_usuario;
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                   // UsuarioModel usu = new UsuarioModel();
                    RolModel rolModel = new RolModel();
                    valor = cmbRol.SelectedItem.ToString();
                   
                    int id_rol = obtener_id_rol(valor);
                    
                    string nombre = txtNombre.Text;
                    string apellidos = txtApellido.Text;
                    string alias = txtAlias.Text;
                    string password = pwbPassword.Password;
                    //System.Windows.MessageBox.Show(id_usu.ToString() + " " + nombre + " " + apellidos + " " + alias + " " + password + "" + " " + id_rol);
                    Usuarios user = new Usuarios(bandera_online_offline);
                    string pass_tabla = obtener_password(id_usu);
                    bool inserto = false;
                    bool actualizo = false;
                    bool borro = false;
                    if (password.Equals(pass_tabla))
                    {
                        if(usu.rol.id_rol==2 && id_rol==4)
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol, alias_user);
                            if (actualizo)
                            {
                                borro = user.eliminarDoctor(id_usu, alias_user);
                                if (borro)
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                else
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }

                            }
                            else
                            {
                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                            }
                        }
                        else if(usu.rol.id_rol==4 && id_rol==2)
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol, alias_user);
                            if (actualizo)
                            {
                                inserto = user.insertar_solo_doctor(id_usu, alias_user, "");
                                if (inserto)
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                else
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                                // System.Windows.Forms.MessageBox.Show("Se actualizao correctamente ", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                //if (admin != null)
                                //    admin.Main.Content = new Page4(alias_user);
                            }
                            else
                            {
                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                            }
                        }
                        else
                        {
                            inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol, alias_user);
                            if (inserto)
                            {

                                // System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                                usu.alias = alias;
                                usu.apellidos = apellidos;
                                usu.id_usuario = id_usu;
                                usu.nombre = nombre;
                                usu.password = password;
                                rolModel.id_rol = id_rol;
                                rolModel.descripcion = valor;

                                usu.rol = rolModel;

                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                       

                    }
                    else
                    {
                        Seguridad secure = new Seguridad();
                        string new_pass = secure.Encriptar(password);
                        inserto = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                        if (usu.rol.id_rol == 2 && id_rol == 4)
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                            if (actualizo)
                            {
                                borro = user.eliminarDoctor(id_usu, alias_user);
                                if (borro)
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                else
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }

                            }
                            else
                            {
                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                            }
                        }
                        else if (usu.rol.id_rol == 4 && id_rol == 2)
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                            if (actualizo)
                            {
                                inserto = user.insertar_solo_doctor(id_usu, alias_user, "");
                                if (inserto)
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                else
                                {
                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    if (socio != null)
                                        //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                        socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                                }
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                                // System.Windows.Forms.MessageBox.Show("Se actualizao correctamente ", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                //if (admin != null)
                                //    admin.Main.Content = new Page4(alias_user);
                            }
                            else
                            {
                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);
                            }
                        }
                        else
                        {
                            if (inserto)
                            {
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol);
                                usu.alias = alias;
                                usu.apellidos = apellidos;
                                usu.id_usuario = id_usu;
                                usu.nombre = nombre;
                                usu.password = password;
                                rolModel.id_rol = id_rol;
                                rolModel.descripcion = valor;

                                usu.rol = rolModel;

                                //System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //vu.refrescar_listview(this.usu, usu, lv_aux);
                                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                if (socio != null)
                                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                                    socio.Main4.Content = new Socio_usuarios(this.lista, this.alias);

                            }
                            else
                            {

                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No seleccionó Nada en el combobox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            conexionBD.Close();

            return password;
        }
    }
}
