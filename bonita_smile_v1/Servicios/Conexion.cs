using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Conexion
    {
        string servidor = "162.241.60.126";
        string puerto = "3306";
        string usuario = "jjdevelo_dentist";
        string password = "jjpd1996";
        string database = "jjdevelo_dentist";

        public MySqlConnection conexion()
        {
            string cadena = "server=" + servidor + ";port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + database;
            MySqlConnection conexionBD = new MySqlConnection(cadena);
            return conexionBD;
        }
    }
}
