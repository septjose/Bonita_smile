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
    class Motivo_cita
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Motivo_cita()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Motivo_citaModel> Mostrar_MotivoCita(int id_paciente)
        {
            List<Motivo_citaModel> listaMotivo_cita = new List<Motivo_citaModel>();
            query = "select * from motivo_cita inner join paciente on paciente.id_paciente=motivo_cita.id_paciente where paciente.id_paciente=" + id_paciente;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo_citaModel motivo_CitaModel = new Motivo_citaModel();
                    PacienteModel pacienteModel = new PacienteModel();

                    motivo_CitaModel.id_motivo = int.Parse(reader[0].ToString());
                    motivo_CitaModel.descripcion = reader[1].ToString();
                    motivo_CitaModel.costo = double.Parse(reader[2].ToString());
                    pacienteModel.id_paciente = int.Parse(reader[5].ToString());
                    motivo_CitaModel.paciente = pacienteModel;
                    listaMotivo_cita.Add(motivo_CitaModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaMotivo_cita;
        }

        public bool eliminarMotivo_cita(int id_motivo)
        {
            query = "DELETE FROM motivo_cita where id_motivo=" + id_motivo;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarMotivo_cita(string descripcion, double costo, int id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                Seguridad seguridad = new Seguridad();
                string auxiliar_identificador = seguridad.Encriptar(descripcion + costo + id_paciente);
                query = "INSERT INTO motivo_cita (descripcion,costo,id_paciente,auxiliar_identificador) VALUES('" + descripcion + "'," + costo + "," + id_paciente + ",'" + auxiliar_identificador + "')";
            }
            else
            {
                query = "INSERT INTO motivo_cita (descripcion,costo,id_paciente) VALUES('" + descripcion + "'," + costo + "," + id_paciente + ")";
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
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarMotivo_cita(int id_motivo, string descripcion, double costo, int id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // seguridad.Encriptar(descripcion + costo + id_paciente);
                string auxiliar_identificador = MostrarMotivo_Cita_Update(id_motivo);
                query = "UPDATE motivo_cita set descripcion = '" + descripcion + "',costo = " + costo + ",id_paciente = " + id_paciente + ",auxiliar_identificador = '<!--" + auxiliar_identificador + "-->' where id_motivo = " + id_motivo;
            }
            else
            {
                query = "UPDATE motivo_cita set descripcion = '" + descripcion + "',costo = " + costo + ",id_paciente = " + id_paciente + " where id_motivo = " + id_motivo;
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
                    ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                return true;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public string MostrarMotivo_Cita_Update(int id_motivo)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from motivo_cita where id_motivo=" + id_motivo;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    aux_identi = reader[0].ToString();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return aux_identi;
        }
    }
}
