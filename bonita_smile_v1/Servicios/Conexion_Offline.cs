using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Conexion_Offline
    {
        string servidor = "localhost";
        string puerto = "3306";
        string usuario = "root";
        string password = "";
        string database = "bs";

        public MySqlConnection conexion_offline()
        {
            string cadena = "server=" + servidor + ";port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + database;
            MySqlConnection conexionBD = new MySqlConnection(cadena);
            return conexionBD;
        }
    }
}
