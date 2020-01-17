﻿using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Servicios
{
    class Paciente
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Paciente()
        {
            this.conexionBD = obj.conexion();
        }

        public List<PacienteModel> MostrarPaciente()
        {
            List<PacienteModel> listaPaciente = new List<PacienteModel>();
            query = "SELECT * FROM paciente inner join clinica on clinica.id_clinica=paciente.id_clinica";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PacienteModel pacienteModel = new PacienteModel();
                    ClinicaModel clinicaModel = new ClinicaModel();

                    pacienteModel.id_paciente = reader[0].ToString();
                    pacienteModel.nombre = reader[1].ToString();
                    pacienteModel.apellidos = reader[2].ToString();
                    pacienteModel.direccion = reader[3].ToString();
                    pacienteModel.telefono = reader[4].ToString();
                    pacienteModel.foto = reader[5].ToString();
                    pacienteModel.imagen = LoadImage(@"C:\bs\" + reader[5].ToString());
                    pacienteModel.email = reader[6].ToString();
                    if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                    //pacienteModel.marketing = reader[6].ToString();

                    pacienteModel.antecedente = reader[9].ToString();
                    clinicaModel.id_clinica = reader[11].ToString();
                    clinicaModel.nombre_sucursal = reader[12].ToString();
                    clinicaModel.color = reader[13].ToString();
                    pacienteModel.clinica = clinicaModel;


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

        public List<PacienteModel> MostrarPaciente_Clinica(string id)
        {

            List<PacienteModel> listaPaciente = new List<PacienteModel>();
            query = "SELECT * FROM paciente inner join clinica on clinica.id_clinica=paciente.id_clinica where clinica.id_clinica='" + id + "';";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PacienteModel pacienteModel = new PacienteModel();
                    ClinicaModel clinicaModel = new ClinicaModel();

                    pacienteModel.id_paciente = reader[0].ToString();
                    pacienteModel.nombre = reader[1].ToString();
                    pacienteModel.apellidos = reader[2].ToString();
                    pacienteModel.direccion = reader[3].ToString();
                    pacienteModel.telefono = reader[4].ToString();
                    pacienteModel.foto = reader[5].ToString();
                    pacienteModel.imagen = LoadImage(@"C:\bs\" + reader[5].ToString());
                    pacienteModel.email = reader[6].ToString();
                    if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                    //pacienteModel.marketing = reader[6].ToString();

                    pacienteModel.antecedente = reader[9].ToString();
                    clinicaModel.id_clinica = reader[10].ToString();
                    clinicaModel.nombre_sucursal = reader[11].ToString();
                    clinicaModel.color = reader[12].ToString();
                    pacienteModel.clinica = clinicaModel;


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

        //public List<PacienteModel> MostrarPaciente_unico(string nombre)
        //{
        //    List<PacienteModel> listaPaciente = new List<PacienteModel>();
        //    query = "SELECT * FROM paciente inner join clinica on clinica.id_clinica=paciente.id_clinica where paciente.nombre='" + nombre + "'";

        //    try
        //    {
        //        conexionBD.Open();
        //        MySqlCommand cmd = new MySqlCommand(query, conexionBD);

        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            PacienteModel pacienteModel = new PacienteModel();
        //            ClinicaModel clinicaModel = new ClinicaModel();

        //            pacienteModel.id_paciente = int.Parse(reader[0].ToString());
        //            pacienteModel.nombre = reader[1].ToString();
        //            pacienteModel.apellidos = reader[2].ToString();
        //            pacienteModel.direccion = reader[3].ToString();
        //            pacienteModel.telefono = reader[4].ToString();

        //            string ruta = reader[5].ToString();

        //            pacienteModel.imagen = LoadImage(@"C:\bs\" + ruta);
        //            pacienteModel.email = reader[6].ToString();
        //            if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
        //            //pacienteModel.marketing = reader[6].ToString();

        //            pacienteModel.antecedente = reader[9].ToString();
        //            clinicaModel.id_clinica = int.Parse(reader[10].ToString());
        //            clinicaModel.nombre_sucursal = reader[11].ToString();
        //            clinicaModel.color = reader[12].ToString();
        //            pacienteModel.clinica = clinicaModel;


        //            listaPaciente.Add(pacienteModel);
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    conexionBD.Close();
        //    return listaPaciente;
        //}

        public bool eliminarPaciente(string id_paciente)
        {
            query = "DELETE FROM paciente where id_paciente='" + id_paciente+"'";
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
                if (!ti.Test())
                {
                    //Escribir_Archivo ea = new Escribir_Archivo();
                    //ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                }
                return false;
            }
        }

        public bool insertarPaciente(string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica)
        {
            string auxiliar_identificador="";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(nombre + apellidos + direccion + telefono + foto + antecedente + email + marketing + id_clinica);
            bool internet = ti.Test();
            if (!internet)
            {
                
                query = "INSERT INTO paciente (id_paciente,nombre,apellidos,direccion,telefono,foto,antecedente,email,marketing,id_clinica,auxiliar_identificador) VALUES('"+auxiliar_identificador+"','" + nombre + "','" + apellidos + "','" + direccion + "','" + telefono + "','" + foto + "','" + antecedente + "','" + email + "'," + marketing + ",'" + id_clinica + "','<!--" + auxiliar_identificador + "-->')";
            }
            else
            {
                query = "INSERT INTO paciente (id_paciente,nombre,apellidos,direccion,telefono,foto,antecedente,email,marketing,id_clinica) VALUES('" + auxiliar_identificador + "','" + nombre + "','" + apellidos + "','" + direccion + "','" + telefono + "','" + foto + "','" + antecedente + "','" + email + "'," + marketing + ",'" + id_clinica + "')";
            }
            MessageBox.Show(query);
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

        public bool actualizarPaciente(string id_paciente, string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // seguridad.Encriptar(nombre + apellidos + direccion + telefono + foto + antecedente + email + marketing + id_clinica);
                string auxiliar_identificador = MostrarPaciente_Update(id_paciente);
                query = "UPDATE paciente set nombre = '" + nombre + "',apellidos = '" + apellidos + "',direccion = '" + direccion + "',telefono = '" + telefono + "',foto = '" + foto + "',email = '" + email + "',marketing = " + marketing + ",id_clinica =' " + id_clinica + "',antecedente='" + antecedente + ",auxiliar_identificador = '" + auxiliar_identificador + "' where id_paciente = '" + id_paciente+"'";
            }
            else
            {
                query = "UPDATE paciente set nombre = '" + nombre + "',apellidos = '" + apellidos + "',direccion = '" + direccion + "',telefono = '" + telefono + "',foto = '" + foto + "',email = '" + email + "',marketing = " + marketing + ",id_clinica = '" + id_clinica + "',antecedente='" + antecedente + "' where id_paciente = '" + id_paciente+"'";
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

        public string MostrarPaciente_Update(string id_paciente)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from paciente where id_paciente='" + id_paciente+"'";

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
        private BitmapImage LoadImage(string filename)
        {
            BitmapImage bi;

            if (File.Exists(filename))
            {
                bi = new BitmapImage(new Uri(filename));
            }
            else
            {
                bi = new BitmapImage(new Uri(@"C:\bs\img1.jpg"));
            }
            return bi;
        }
    }
}