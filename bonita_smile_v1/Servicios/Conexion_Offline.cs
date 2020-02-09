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

        //solo aqui
        //comentario de prueba para ver si se esta haciendo en la pc remote: esto se esta haceindo desde mi propio visual studio y computadora..............ya se como 
        //trabajaremos para resolver estos errores jeje

            

        string servidor = "7.152.100.9";
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
