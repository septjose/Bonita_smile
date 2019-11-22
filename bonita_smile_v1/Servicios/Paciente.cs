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
    class Paciente
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();

        public Paciente()
        {
            this.conexionBD = obj.conexion();
        }

        public List<PacienteModel> MostrarPaciente()
        {
            List<PacienteModel> listaPaciente = new List<PacienteModel>();
            query = "SELECT * FROM paciente";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PacienteModel pacienteModel = new PacienteModel();

                    pacienteModel.id_paciente = int.Parse(reader[0].ToString());
                    pacienteModel.nombre = reader[1].ToString();
                    pacienteModel.apellidos = reader[2].ToString();
                    pacienteModel.direccion = reader[3].ToString();
                    pacienteModel.telefono = reader[4].ToString();
                    pacienteModel.foto = reader[5].ToString();
                    //pacienteModel.antecedente = int.Parse(reader[6].ToString());
                    pacienteModel.email = reader[7].ToString();
                    pacienteModel.marketing = int.Parse(reader[8].ToString());

                    listaPaciente.Add(pacienteModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaPaciente;
        }

        public bool eliminarPaciente(int id_paciente)
        {
            query = "DELETE FROM paciente where id_paciente=" + id_paciente;
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

        public bool insertarPaciente(string nombre, string apellidos, string direccion, string telefono, string foto, int id_antecedentes, string email, int marketing, int id_clinica)
        {
            query = "INSERT INTO paciente (nombre,apellidos,direccion,telefono,foto,id_antecedentes,email,marketing,id_clinica) VALUES('" + nombre + "','" + apellidos + "','" + direccion + "','" + telefono + "','" + foto + "'," + id_antecedentes + ",'" + email + "'," + marketing + "," + id_clinica + ")";
            MessageBox.Show(query);
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

        public bool actualizarPaciente(int id_paciente, string nombre, string apellidos, string direccion, string telefono, string foto, int id_antecedentes, string email, int marketing)
        {
            query = "UPDATE paciente set nombre = '" + nombre + "',apellidos = '" + apellidos + "',direccion = '" + direccion + "',telefono = '" + telefono + "',foto = '" + foto + "',id_antecedentes = " + id_antecedentes + ",email = '" + email + "',marketing = '" + marketing + "' where paciente = " + id_paciente;
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