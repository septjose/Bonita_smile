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
    class Abonos
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();

        public Abonos()
        {
            conexionBD = obj.conexion();
        }

        public List<AbonosModel> MostrarAbonos(string id_motivo, string id_paciente)
        {
            List<AbonosModel> listaAbonos = new List<AbonosModel>();
            query = "SELECT id_abono,id_paciente,id_motivo,date_format(fecha, '%d/%m/%Y') as fecha,monto,comentario FROM abonos where id_paciente='" + id_paciente + "' and id_motivo='" + id_motivo+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AbonosModel abonosModel = new AbonosModel();

                    abonosModel.id_abono = reader[0].ToString();
                    abonosModel.id_paciente = reader[1].ToString();
                    abonosModel.id_motivo = reader[2].ToString();
                    abonosModel.fecha = reader[3].ToString();
                    abonosModel.monto = double.Parse(reader[4].ToString());
                    abonosModel.comentario = reader[5].ToString();

                    listaAbonos.Add(abonosModel);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return listaAbonos;
        }

        public string MostrarAbonos_Update(string id_abono)
        {
            string aux_identi = "";
            query = "SELECT auxiliar_identificador from abonos where id_abono='" + id_abono+"'";

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

        public double Abonados(string id_motivo)
        {
            double abonado = 0.0;

            query = "select  IFNULL(sum(monto),0)as abonado from abonos where id_motivo = '" + id_motivo+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    abonado = double.Parse(reader[0].ToString());



                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            if (!ti.Test())
            {
                Escribir_Archivo ea = new Escribir_Archivo();
                ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
            }
            return abonado;
        }

        public double Restante(string id_motivo)
        {
            double restante = 0.0;

            query = "select IFNULL(((select costo from motivo_cita where id_motivo='" + id_motivo + "')-(select sum(monto) from abonos where id_motivo ='" + id_motivo + "')),0) as restante";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    restante = double.Parse(reader[0].ToString());



                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return restante;
        }
        public bool eliminarAbono(string id_abono)
        {
            MySqlCommand cmd;
            query = "DELETE FROM abonos where id_abono='" + id_abono+"'";
            try
            {
                conexionBD.Open();
                if (!ValidarExistencia(id_abono))
                {
                    MessageBox.Show("El registro que esta tratando de eliminar no existe");
                    return false;
                }
                else
                {
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();
                    if (!ti.Test())
                    {
                        //Escribir_Archivo ea = new Escribir_Archivo();
                        //ea.escribir(@"c:\offline\script_temporal.txt", query + ";");
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarAbono(string id_paciente, string id_motivo, string fecha, double monto, string comentario)
        {
            bool internet = ti.Test();
            Seguridad seguridad = new Seguridad();
            string id_abono = "";
            id_abono = seguridad.SHA1(id_paciente + id_motivo + fecha + monto + comentario);
            if (!internet)
            {
                
                 
                //string id_abono=seguridad.Encriptar()
                query = "INSERT INTO abonos (id_abono,id_paciente,id_motivo,fecha,monto,comentario,auxiliar_identificador) VALUES('"+id_abono +"','"+ id_paciente + "','" + id_motivo + "','" + fecha + "'," + monto + ",'" + comentario + "','<!--" + id_abono + "-->')";
            }
            else
            {
                
                query = "INSERT INTO abonos (id_abono,id_paciente,id_motivo,fecha,monto,comentario) VALUES('" + id_abono + "'," + id_paciente + "," + id_motivo + ",'" + fecha + "'," + monto + ",'" + comentario + "')";
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

        public bool actualizarAbono(string id_abono, string id_paciente, string id_motivo, string fecha, double monto)
        {
            bool internet = ti.Test();
            if (!internet)
            {
                string auxiliar_identificador = MostrarAbonos_Update(id_abono);
                Seguridad seguridad = new Seguridad();
                //string auxiliar_identificador = seguridad.Encriptar(id_paciente.ToString()+id_motivo+fecha+monto);
                query = "UPDATE abonos set id_paciente =' " + id_paciente + "',id_motivo = '" + id_motivo + "',fecha = '" + fecha + "',monto = " + monto + ",auxiliar_identificador = '" + auxiliar_identificador + "'where id_abono = '" + id_abono+"'";
            }
            else
            {
                query = "UPDATE abonos set id_paciente = '" + id_paciente + "',id_motivo = '" + id_motivo + "',fecha = '" + fecha + "',monto = " + monto + "where id_abono = '" + id_abono+"'";
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

        
        private bool ValidarExistencia(string id_abono)
        {
            MySqlCommand cmd;
            string query = "SELECT * FROM rol where id_abono='" + id_abono+"'";
            try
            {
                cmd = new MySqlCommand(query, conexionBD);
                int existe = Convert.ToInt32(cmd.ExecuteScalar());
                if (existe == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
