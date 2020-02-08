using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;

namespace bonita_smile_v1.Servicios
{
    class Migrar_Archivo_Servidor
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        bool bandera_online_offline = false;
        public Migrar_Archivo_Servidor()
        {
            conexionBD = obj.conexion(bandera_online_offline);
        }

       /* public bool SincronizarLocalServidor()
        {
            bool internet = ti.Test();
            if (!internet)
            {
                MessageBox.Show("Intentelo más tarde, no cuenta con acceso a internet");
            }
            else
            {
                query = "INSERT INTO abonos (id_paciente,id_motivo,fecha,monto,comentario) VALUES(" + id_paciente + "," + id_motivo + ",'" + fecha + "'," + monto + ",'" + comentario + "')";
            }

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!internet)
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(@"\\DESKTOP-ED8E774\offline\script_temporal.txt", query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }*/
    }
}
