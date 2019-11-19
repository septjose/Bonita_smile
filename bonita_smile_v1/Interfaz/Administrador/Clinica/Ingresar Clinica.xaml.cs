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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using bonita_smile_v1.Servicios;
using MySql.Data.MySqlClient;
using bonita_smile_v1.Modelos;

namespace bonita_smile_v1.Interfaz.Administrador.Clinica
{
    /// <summary>
    /// Lógica de interacción para Ingresar_Clinica.xaml
    /// </summary>
    public partial class Ingresar_Clinica : MetroWindow
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        string valor = "";
        public Ingresar_Clinica()
        {
            this.conexionBD = obj.conexion();
            InitializeComponent();
            llenar_Combo();
        }

        public void llenar_Combo()
        {
            query = "SELECT * FROM colores";

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

                    string color = reader[1].ToString();
                    cmbColor.Items.Add(color);
                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            valor = cmbColor.SelectedItem.ToString();

            MessageBox.Show(valor);
            int id_color = obtener_id_color(valor);
            MessageBox.Show(id_color.ToString());
            string nombre_sucursal = txtNombre.Text;
            MessageBox.Show(nombre_sucursal);
            Clinicas c = new Clinicas();
            bool correcto = c.insertarClinica(nombre_sucursal, id_color);
            if(correcto)
            {
                MessageBox.Show("SI");
            }
            else
            {
                MessageBox.Show("No");
            }
            
           
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                
        }

        public int obtener_id_color(string descripcion)
        {
            int id = 0;
            query = "SELECT id_color FROM colores where descripcion='"+descripcion+"'";

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
