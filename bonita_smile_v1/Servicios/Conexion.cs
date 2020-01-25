using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bonita_smile_v1.Servicios
{
    class Conexion
    {
        string servidor;
        string puerto;
        string usuario;
        string password;
        string database;

        public MySqlConnection conexion()
        {
            
            Test_Internet ti = new Test_Internet();
            if (ti.Test())
            {
                //MessageBox.Show("Estas Online");
                
                servidor = "162.241.60.126";
                puerto = "3306";
                usuario = "jjdevelo_dentist";
                password = "jjpd1996";
                database = "jjdevelo_dentist";
            }
            else
            {
                //MessageBox.Show("Estas Offline");
                servidor = "localhost";
                puerto = "3306";
                usuario = "root";
                password = "";
                database = "dentist";
            }

            string cadena = "server=" + servidor + ";port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + database;
            MySqlConnection conexionBD = new MySqlConnection(cadena);
            return conexionBD;
        }

        public string verificar()
        {
            string ver = "";
            Test_Internet ti = new Test_Internet();
            if (ti.Test())
            {
                ver = "Online";
            }
            else
            {
                ver = "Offline";
            }
            return ver;

        }

    }
}
