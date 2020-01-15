using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bonita_smile_v1.Servicios
{
    class Sincronizar
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion_Offline obj = new Conexion_Offline();
        Test_Internet ti = new Test_Internet();
        Conexion obj2 = new Conexion();

        public Sincronizar()
        {
            this.conexionBD = obj.conexion_offline();
        }
        public void Backup()
        {
            /*servidor = "162.241.60.126";
            puerto = "3306";
            usuario = "jjdevelo_dentist";
            password = "jjpd1996";
            database = "jjdevelo_dentist";
            */
            string constring = "server=162.241.60.126;user=jjdevelo_dentist;pwd=jjpd1996;database=jjdevelo_dentist;";
            constring += "charset=utf8;convertzerodatetime=true;";
            string file = @"C:\backup_bs\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(file);
                        MessageBox.Show("Se hizo el respaldo correctamente");
                        conn.Close();
                    }
                }
            }
        }

        public void Restore()
        {
            string constring = "server=localhost;user=root;pwd=;database=dentista;";
            string file = @"C:\backup_bs\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(file);
                        MessageBox.Show("Se restauro correctamente");
                        conn.Close();
                    }
                }
            }
        }

        public bool borrar_bd()
        {
            query = "drop database dentista";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool crear_bd()
        {
            query = "create database dentista;";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
               
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool SincronizarLocalServidor()
        {
            conexionBD = obj2.conexion();
            Escribir_Archivo ea = new Escribir_Archivo();
            bool internet = ti.Test();
            List<String> lQuery = new List<string>();
            lQuery = ea.corregirArchivo();
            if (!internet)
            {
                MessageBox.Show("Intentelo más tarde, no cuenta con acceso a internet");
                return false;
            }
            else
            {
                //CREAR TRANSACCION
                foreach(var query in lQuery)
                {
                    if (!query.Equals(""))
                    {
                        conexionBD.Open();
                        MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                        cmd.ExecuteReader();
                        conexionBD.Close();
                    }
                }
                return true;
            }
        }

    }
}
