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
    class Clinicas
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Clinicas()
        {
            this.conexionBD = obj.conexion();
        }

        public List<ClinicaModel> MostrarClinica()
        {
            List<ClinicaModel> listaClinica = new List<ClinicaModel>();
            query = "SELECT * FROM clinica ";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClinicaModel clinicaModel = new ClinicaModel();


                    clinicaModel.id_clinica = reader[0].ToString();
                    clinicaModel.nombre_sucursal = reader[1].ToString();
                    clinicaModel.color = reader[2].ToString();
                    listaClinica.Add(clinicaModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaClinica;

        }

        public bool eliminarClinica(string id_clinica)
        {
            query = "DELETE FROM clinica where id_clinica='" + id_clinica+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();

                if (!ti.Test())
                {
                   //Escribir_Archivo ea = new Escribir_Archivo();
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

        public bool insertarClinica(string nombre_sucursal, string color)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(nombre_sucursal + color);
            bool internet = ti.Test();
            if (!internet)
            {
               
                query = "INSERT INTO clinica (id_clinica,nombre_sucursal,color,auxiliar_identificador) VALUES('"+ auxiliar_identificador+"','"+ nombre_sucursal + "','" + color + "','<!--" + auxiliar_identificador + "-->')";
            }
            else
            {
                query = "INSERT INTO clinica (id_clinica,nombre_sucursal,color) VALUES('" + auxiliar_identificador + "','" + nombre_sucursal + "','" + color + "')";
            }
            //query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('Clinica Salamanca',1)";
            Console.WriteLine(query);
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
                MessageBox.Show("E");
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertar_Permisos(string id_usuario, string id_clinica)
        {
            string auxiliar_identificador = "";
            Seguridad seguridad = new Seguridad();
            auxiliar_identificador = seguridad.SHA1(id_usuario + id_clinica);
            bool internet = ti.Test();
            if (!internet)
            {
               
                query = "INSERT INTO permisos (id_permiso,id_usuario,id_clinica,auxiliar_identificador) VALUES('"+auxiliar_identificador+"','" + id_usuario + "','" + id_clinica + "','<!--" + auxiliar_identificador + "-->')";
            }
            else
            {
                query = "INSERT INTO permisos (id_permiso,id_usuario,id_clinica) VALUES('" + auxiliar_identificador + "','" + id_usuario + "','" + id_clinica + "')";
            }
            //query = "INSERT INTO clinica (nombre_sucursal,id_color) VALUES('Clinica Salamanca',1)";
            Console.WriteLine(query);
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
                MessageBox.Show("E");
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarClinica(string id_clinica, string nombre_sucursal, string color)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                //string auxiliar_identificador = seguridad.Encriptar(nombre_sucursal+ color);
                string auxiliar_identificador = MostrarClinica_Update(id_clinica);
                query = "UPDATE clinica set nombre_sucursal = '" + nombre_sucursal + "',color = '" + color + "',auxiliar_identificador = '" + auxiliar_identificador + "' where id_clinica = '" + id_clinica+"'";
                MessageBox.Show(query);
            }
            else
            {
                query = "UPDATE clinica set nombre_sucursal = '" + nombre_sucursal + "',color = '" + color + "' where id_clinica = '" + id_clinica+"'";
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

        public bool actualizar_Permisos(string id_usuario, string id_clinica, string id_permiso)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                //Seguridad seguridad = new Seguridad();
                // = seguridad.Encriptar(id_usuario.ToString() + id_clinica);
                string auxiliar_identificador = MostrarPermisos_Update(id_permiso);
                query = "UPDATE permisos set id_usuario = '" + id_usuario + "',id_clinica = '" + id_clinica + "',auxiliar_identificador = '" + auxiliar_identificador + "' where id_permiso = '" + id_permiso+"'";
            }
            else
            {
                query = "UPDATE permisos set id_usuario = '" + id_usuario + "',id_clinica = '" + id_clinica + "' where id_permiso = '" + id_permiso+"'";
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

        public string MostrarClinica_Update(string id_clinica)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from clinica where id_clinica='" + id_clinica+"'";

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

        public string MostrarPermisos_Update(string id_permiso)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from permisos where id_permiso='" + id_permiso+"'";

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
