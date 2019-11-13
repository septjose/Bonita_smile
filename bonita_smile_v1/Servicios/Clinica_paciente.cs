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
    class Clinica_paciente
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Clinica_paciente()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Clinica_pacienteModel> MostrarClinica()
        {
            List<Clinica_pacienteModel> listaClinica_paciente = new List<Clinica_pacienteModel>();
            query = "SELECT * FROM clinica_paciente";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Clinica_pacienteModel clinica_PacienteModel = new Clinica_pacienteModel();

                    clinica_PacienteModel.id_clinica = int.Parse(reader[0].ToString());
                    clinica_PacienteModel.id_paciente = int.Parse(reader[1].ToString());

                    listaClinica_paciente.Add(clinica_PacienteModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaClinica_paciente;

        }
    }
}
