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
        string servidor = "192.168.1.76";
        string puerto = "3306";
        string usuario = "usuariochido";
        string password = "12345";
        string database = "bs";

        public MySqlConnection conexion_offline()
        {
            string cadena = "server=" + servidor + ";port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + database;
            MySqlConnection conexionBD = new MySqlConnection(cadena);
            return conexionBD;
        }
    }
}
