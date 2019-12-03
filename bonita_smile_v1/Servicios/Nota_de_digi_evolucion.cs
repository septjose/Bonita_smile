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
    class Nota_de_digi_evolucion
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Nota_de_digi_evolucion()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Nota_de_digi_evolucionModel> MostrarNota_de_digi_evolucion(int id_motivo,int id_paciente)
        {
            List<Nota_de_digi_evolucionModel> listaNota_de_digi_evolucion = new List<Nota_de_digi_evolucionModel>();
            query = "SELECT * FROM nota_de_digi_evolucion where id_paciente=" + id_paciente+" and id_motivo="+id_motivo;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Nota_de_digi_evolucionModel nota_De_Digi_EvolucionModel = new Nota_de_digi_evolucionModel();

                    nota_De_Digi_EvolucionModel.id_nota = int.Parse(reader[0].ToString());
                    nota_De_Digi_EvolucionModel.id_paciente = int.Parse(reader[1].ToString());
                    nota_De_Digi_EvolucionModel.id_motivo = int.Parse(reader[2].ToString());
                    nota_De_Digi_EvolucionModel.descripcion = reader[3].ToString();
                    //nota_De_Digi_EvolucionModel.fecha =reader[4].ToString();
                    DateTime dt = DateTime.Parse("2019/05/02");
                    nota_De_Digi_EvolucionModel.fecha = dt.ToString("yyyy/MM/dd");

                    listaNota_de_digi_evolucion.Add(nota_De_Digi_EvolucionModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaNota_de_digi_evolucion;
        }

        public bool eliminarMotivo_cita(int id_nota)
        {
            query = "DELETE FROM nota_de_digi_evolucion where id_nota="+ id_nota;
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

        public bool insertarNota_de_digi_evolucion(int id_paciente, int id_motivo, string descripcion, string fecha)
        {
            query = "INSERT INTO nota_de_digi_evolucion (id_paciente,id_motivo,descripcion,fecha) VALUES("+ id_paciente +","+ id_motivo +",'"+ descripcion +"','"+ fecha +"')";
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

        public bool actualizarNota_de_digi_evolucion(int id_nota, int id_paciente, int id_motivo, string descripcion, string fecha)
        {
            query = "UPDATE nota_de_digi_evolucion set id_paciente = "+ id_paciente +",id_motivo = "+ id_motivo +"',descripcion = '"+ descripcion +"',fecha = "+ fecha +" where id_nota = "+ id_nota;
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
    }
}
