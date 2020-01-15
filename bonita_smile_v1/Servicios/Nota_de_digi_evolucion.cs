﻿using bonita_smile_v1.Modelos;
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
        Test_Internet ti = new Test_Internet();

        public Nota_de_digi_evolucion()
        {
            this.conexionBD = obj.conexion();
        }

        public List<Nota_de_digi_evolucionModel> MostrarNota_de_digi_evolucion(int id_motivo, int id_paciente)
        {
            List<Nota_de_digi_evolucionModel> listaNota_de_digi_evolucion = new List<Nota_de_digi_evolucionModel>();
            query = "SELECT id_nota,id_paciente,id_motivo,descripcion,date_format(fecha, '%d/%m/%Y') as fecha FROM nota_de_digi_evolucion where id_paciente=" + id_paciente + " and id_motivo=" + id_motivo;

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
                    nota_De_Digi_EvolucionModel.fecha = reader[4].ToString();

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
            query = "DELETE FROM nota_de_digi_evolucion where id_nota=" + id_nota;
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

        public bool insertarNota_de_digi_evolucion(int id_paciente, int id_motivo, string descripcion, string fecha)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                Seguridad seguridad = new Seguridad();
                string auxiliar_identificador = seguridad.Encriptar(id_paciente + id_motivo + descripcion + fecha);
                query = "INSERT INTO nota_de_digi_evolucion (id_paciente,id_motivo,descripcion,fecha,auxiliar_identificador) VALUES(" + id_paciente + "," + id_motivo + ",'" + descripcion + "','" + fecha + "','" + auxiliar_identificador + "')";
            }
            else
            {
                query = "INSERT INTO nota_de_digi_evolucion (id_paciente,id_motivo,descripcion,fecha) VALUES(" + id_paciente + "," + id_motivo + ",'" + descripcion + "','" + fecha + "')";
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

        public bool actualizarNota_de_digi_evolucion(int id_nota, int id_paciente, int id_motivo, string descripcion, string fecha)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // seguridad.Encriptar(id_paciente + id_motivo + descripcion + fecha);
                string auxiliar_identificador = MostrarNotas_Update(id_nota);
                query = "UPDATE nota_de_digi_evolucion set id_paciente = " + id_paciente + ",id_motivo = " + id_motivo + "',descripcion = '" + descripcion + "',fecha = " + fecha + ",auxiliar_identificador = '<!--" + auxiliar_identificador + "-->' where id_nota = " + id_nota;
            }
            else
            {
                query = "UPDATE nota_de_digi_evolucion set id_paciente = " + id_paciente + ",id_motivo = " + id_motivo + "',descripcion = '" + descripcion + "',fecha = " + fecha + " where id_nota = " + id_nota;
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

        public string MostrarNotas_Update(int id_nota)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from nota_de_digi_evolucion where id_nota=" + id_nota;

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
