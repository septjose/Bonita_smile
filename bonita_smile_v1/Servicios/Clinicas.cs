using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bonita_smile_v1.Servicios
{
    class Clinicas
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Clinicas()
        {
            this.conexionBD = obj.conexion();
        }

        public List<ClinicaModel> MostrarClinica()
        {
            List<ClinicaModel> listaClinica = new List<ClinicaModel>();
            query = "SELECT * FROM clinica ";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClinicaModel clinicaModel = new ClinicaModel();
                   

                    clinicaModel.id_clinica = int.Parse(reader[0].ToString());
                    clinicaModel.nombre_sucursal = reader[1].ToString();
                    clinicaModel.color = reader[2].ToString();
                    listaClinica.Add(clinicaModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaClinica;

        }

        public bool eliminarClinica(int id_clinica)
        {
            query = "DELETE FROM clinica where id_clinica="+id_clinica;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query+";");
                }

                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarClinica(string nombre_sucursal, string color)
        {
            
            query = "INSERT INTO clinica (nombre_sucursal,color) VALUES('"+nombre_sucursal+"','"+color+"')";
            //query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('Clinica Salamanca',1)";
            Console.WriteLine(query);
            try
            {               
                conexionBD.Open();        
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();

                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }

                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("E");
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertar_Permisos(int id_usuario, int id_clinica)
        {

            query = "INSERT INTO permisos (id_usuario,id_clinica) VALUES(" + id_usuario + "," + id_clinica + ")";
            //query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('Clinica Salamanca',1)";
            Console.WriteLine(query);
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("E");
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarClinica(int id_clinica, string nombre_sucursal, string color)
        {
            query = "UPDATE clinica set nombre_sucursal = '"+nombre_sucursal+"',color = '"+color+"' where id_clinica = "+ id_clinica;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizar_Permisos(int id_usuario,int id_clinica,int id_permiso)
        {
            query = "UPDATE permisos set id_usuario = " + id_usuario + ",id_clinica = " + id_clinica + " where id_permiso = " + id_permiso;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }


    }
}
