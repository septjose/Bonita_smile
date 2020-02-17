using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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

        //string servidor = "192.168.0.8";
        //string puerto = "3306";
        //string usuario = "usuariochido";
        //string password = "12345";
        //string database = "bs";

        public MySqlConnection conexion_offline()
        {
            //string ruta = "E:\\PortableGit\\programs_c#\\bs_v1.4\\Bonita_smile\\bonita_smile_v1\\Assets\\configuracion.txt";
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");


            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);

            string cadena = "server=" + configuracion.servidor_interno.servidor_local + ";port=" + configuracion.servidor_interno.puerto_local + "; user id=" + configuracion.servidor_interno.usuario_local + "; password=" + configuracion.servidor_interno.password_local + "; database=" + configuracion.servidor_interno.database_local_aux;
            MySqlConnection conexionBD = new MySqlConnection(cadena);
            return conexionBD;
        }
    }
}
