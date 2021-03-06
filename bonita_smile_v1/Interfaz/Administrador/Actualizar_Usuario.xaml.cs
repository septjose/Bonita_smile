﻿using bonita_smile_v1.Interfaz.Administrador;
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
        bool bandera_online_offline = false;
        string alias_user;

        public Page4_Actualizar(UsuarioModel usu, System.Windows.Controls.ListView lv_aux,string alias_user)
        {

            this.conexionBD = obj.conexion(bandera_online_offline);
            InitializeComponent();
            this.usu = usu;
            this.lv_aux = lv_aux;
            txtAlias.Text = usu.alias;
            txtApellido.Text = usu.apellidos;
            txtNombre.Text = usu.nombre;
            pwbPassword.Password = usu.password;
            cmbRol.SelectedItem = usu.rol.descripcion;
            this.alias_user = alias_user;
            
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
                 
                    valor = cmbRol.SelectedItem.ToString();
                    int id_rol = obtener_id_rol(valor);
                    string nombre = txtNombre.Text;
                    string apellidos = txtApellido.Text;
                    string alias = txtAlias.Text;
                    string password = pwbPassword.Password;
                    //System.Windows.MessageBox.Show(id_usu.ToString() + " " + nombre + " " + apellidos + " " + alias + " " + password + "" + " " + id_rol);
                    Usuarios user = new Usuarios(bandera_online_offline);
                    string pass_tabla = obtener_password(id_usu);
                    bool actualizo = false;
                    bool borro = false;
                    bool inserto = false;
                    Clinicas cli = new Clinicas(bandera_online_offline);
                    if (password.Equals(pass_tabla))
                    {
                        //primero verifico que es ese usuario y a que quiere cambiar
                        //1.-si es socio y cambia a otro rol diferente de socio 
                        //1.1-se actualiza el usuario
                        //1.2-se borran los permisos
                        //si no solo se actualiza y listo
                        if (usu.rol.id_rol == 5 && id_rol != 5)
                        {
                            actualizo = user.actualizarUsuarioSocio(id_usu, alias, nombre, apellidos, password, id_rol,alias_user);
                            if(actualizo)
                            {
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuarioSocio(id_usu, alias, nombre, apellidos, password, id_rol);
                                //System.Windows.Forms.MessageBox.Show("Se actualizao correctamente ", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    
                                    admin.Main.Content = new Page4(alias);
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias);
                            }
                        }
                        else

                         if ((usu.rol.id_rol == 2 && id_rol ==3||id_rol==4 ) || (usu.rol.id_rol == 1 &&  id_rol == 3 || id_rol == 4) || (usu.rol.id_rol == 5 && id_rol == 3 || id_rol == 4))
                        {
                            actualizo= user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol,alias_user);
                            if(actualizo)
                            {
                                borro = user.eliminarDoctor(id_usu, alias_user);
                                if(borro)
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }
                                else
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }
                              
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                        }
                        else
                        if((usu.rol.id_rol == 3 && id_rol == 1 || id_rol == 2 || id_rol == 5) || (usu.rol.id_rol == 4 && id_rol == 1 || id_rol == 2 || id_rol == 5))
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol, alias_user);
                            if (actualizo)
                            {
                                inserto = user.insertar_solo_doctor(id_usu,alias_user,"");
                                if (inserto)
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }
                                else
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
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
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                        }
                        else
                        {
                            actualizo = user.actualizarDoctor(id_usu, alias, nombre, apellidos, password, id_rol, alias_user,"");
                            if (actualizo)
                            {
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                                // System.Windows.Forms.MessageBox.Show("Se actualizao correctamente ", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                        }
                        
                    }
                    //////////////////////////////////////////////
                    else
                    {
                        Seguridad secure = new Seguridad();
                        string new_pass = secure.Encriptar(password);
                        if (usu.rol.id_rol == 5 && id_rol != 5)
                        {
                            actualizo = user.actualizarUsuarioSocio(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                            if (actualizo)
                            {
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuarioSocio(id_usu, alias, nombre, apellidos, new_pass, id_rol);

                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else

                         if ((usu.rol.id_rol == 2 && id_rol == 3 || id_rol == 4) || (usu.rol.id_rol == 1 && id_rol == 3 || id_rol == 4) || (usu.rol.id_rol == 5 && id_rol == 3 || id_rol == 4))
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                            if (actualizo)
                            {
                                borro = user.eliminarDoctor(id_usu, alias_user);
                                if (borro)
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }
                                else
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }

                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                        }
                        else
                        if ((usu.rol.id_rol == 3 && id_rol == 1 || id_rol == 2 || id_rol == 5) || (usu.rol.id_rol == 4 && id_rol == 1 || id_rol == 2 || id_rol == 5))
                        {
                            actualizo = user.actualizarUsuario(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user);
                            if (actualizo)
                            {
                                inserto = user.insertar_solo_doctor(id_usu, alias_user, "");
                                if (inserto)
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
                                }
                                else
                                {
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    if (admin != null)
                                        admin.Main.Content = new Page4(alias_user);
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
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                        }
                        else
                        {
                            actualizo = user.actualizarDoctor(id_usu, alias, nombre, apellidos, new_pass, id_rol, alias_user, "");
                            if (actualizo)
                            {
                                //user = new Usuarios(!bandera_online_offline);
                                //user.actualizarUsuario(id_usu, alias, nombre, apellidos, password, id_rol);
                                // System.Windows.Forms.MessageBox.Show("Se actualizao correctamente ", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
                            else
                            {
                                //System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                if (admin != null)
                                    admin.Main.Content = new Page4(alias_user);
                            }
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            conexionBD.Close();

            return password;
        }
    }
}
