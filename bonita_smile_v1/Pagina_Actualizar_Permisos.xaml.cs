﻿using bonita_smile_v1.Servicios;
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
        public Pagina_Actualizar_Permisos(string alias,string nombre_sucursal,string id_permiso)
        {
            this.conexionBD = obj.conexion();
            this.conexionBD2 = obj.conexion();
            InitializeComponent();
            llenar_Combo_Clinica();
            llenar_Combo_Usuario();
            cmbClinica.SelectedItem = nombre_sucursal;
            cmbUsuario.SelectedItem = alias;
            this.permiso = id_permiso;

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
                MessageBox.Show(ex.ToString());
            }
            conexionBD2.Close();
        }

        public void llenar_Combo_Usuario()
        {
            query = "SELECT * FROM usuario where usuario.id_rol=2 ";

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
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }
        private void cmbClinica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valor = cmbUsuario.SelectedItem.ToString();
            valor2 = cmbClinica.SelectedItem.ToString();
            int id_usuario = obtener_id_usuario(valor);
            int id_clinica = obtener_id_Clinica(valor2);
            int id_permiso = Convert.ToInt32(permiso);

            MessageBox.Show(id_usuario + "     " + id_clinica+" id_permiso=   "+ id_permiso);

            Clinicas c = new Clinicas();
            bool inserto = c.actualizar_Permisos(id_usuario, id_clinica,id_permiso);
            if (inserto)
            {
                MessageBox.Show("si");


            }
            else
            {
                MessageBox.Show("no");
            }
        }

        private void cmbUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public int obtener_id_usuario(string descripcion)
        {
            int id = 0;
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

        public int obtener_id_Clinica(string descripcion)
        {
            int id = 0;
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
    }
}
