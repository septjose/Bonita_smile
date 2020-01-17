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

        public List<Motivo_citaModel> Mostrar_MotivoCita(string id_paciente)
        {
            List<Motivo_citaModel> listaMotivo_cita = new List<Motivo_citaModel>();
            query = "select * from motivo_cita inner join paciente on paciente.id_paciente=motivo_cita.id_paciente where paciente.id_paciente='" + id_paciente+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo_citaModel motivo_CitaModel = new Motivo_citaModel();
                    PacienteModel pacienteModel = new PacienteModel();

                    motivo_CitaModel.id_motivo = reader[0].ToString();
                    motivo_CitaModel.descripcion = reader[1].ToString();
                    motivo_CitaModel.costo = double.Parse(reader[2].ToString());
                    pacienteModel.id_paciente = reader[5].ToString();
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

        public bool eliminarMotivo_cita(string id_motivo)
        {
            query = "DELETE FROM motivo_cita where id_motivo='" + id_motivo+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                   // Escribir_Archivo ea = new Escribir_Archivo();
                    //ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
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

        public bool insertarMotivo_cita(string descripcion, double costo, string id_paciente)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(descripcion + costo + id_paciente);
            bool internet = ti.Test();
            if (!internet)
            {
                
                query = "INSERT INTO motivo_cita (id_motivo,descripcion,costo,id_paciente,auxiliar_identificador) VALUES('"+auxiliar_identificador+"','" + descripcion + "'," + costo + ",'" + id_paciente + "','<!--" + auxiliar_identificador + "-->')";
            }
            else
            {
                query = "INSERT INTO motivo_cita (id_motivo,descripcion,costo,id_paciente) VALUES('" + auxiliar_identificador + "','" + descripcion + "'," + costo + ",'" + id_paciente + "')";
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

        public bool actualizarMotivo_cita(string id_motivo, string descripcion, double costo, string id_paciente)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // seguridad.Encriptar(descripcion + costo + id_paciente);
                string auxiliar_identificador = MostrarMotivo_Cita_Update(id_motivo);
                query = "UPDATE motivo_cita set descripcion = '" + descripcion + "',costo = " + costo + ",id_paciente =' " + id_paciente + "',auxiliar_identificador = '" + auxiliar_identificador + "' where id_motivo = '" + id_motivo+"'";
            }
            else
            {
                query = "UPDATE motivo_cita set descripcion = '" + descripcion + "',costo = " + costo + ",id_paciente = '" + id_paciente + "' where id_motivo = '" + id_motivo+"'";
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

        public string MostrarMotivo_Cita_Update(string id_motivo)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from motivo_cita where id_motivo='" + id_motivo+"'";

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
