using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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

        public MySqlConnection conexion(bool online)
        {
            //string ruta = "E:\\PortableGit\\programs_c#\\bs_v1.4\\Bonita_smile\\bonita_smile_v1\\Assets\\Configuracion.cfg";
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.cfg");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion= ab.Cargar(ruta);
            if (online)
            {
                Test_Internet ti = new Test_Internet();
                if (ti.Test())
                {
                    //MessageBox.Show("Estas Online");

                    servidor = configuracion.servidor_externo.servidor_local;
                    puerto = configuracion.servidor_externo.puerto_local;
                    usuario = configuracion.servidor_externo.usuario_local;
                    password = configuracion.servidor_externo.password_local;
                    database = configuracion.servidor_externo.database_local;
                }
                else
                {
                    //MessageBox.Show("Estas Offline");
                    //aqui estas
                    servidor = configuracion.servidor_interno.servidor_local;
                    puerto = configuracion.servidor_interno.puerto_local;
                    usuario = configuracion.servidor_interno.usuario_local;
                    password = configuracion.servidor_interno.password_local;
                    database = configuracion.servidor_interno.database_local; 
                }
            }
            else
            {
                servidor = configuracion.servidor_interno.servidor_local;
                puerto = configuracion.servidor_interno.puerto_local;
                usuario = configuracion.servidor_interno.usuario_local;
                password = configuracion.servidor_interno.password_local;
                database = configuracion.servidor_interno.database_local;
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
