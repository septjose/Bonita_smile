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

        public List<AbonosModel> MostrarAbonos(int id_motivo,int id_paciente)
        {
            List<AbonosModel> listaAbonos = new List<AbonosModel>();
            query = "SELECT id_abono,id_paciente,id_motivo,date_format(fecha, '%d/%m/%Y') as fecha,monto,comentario FROM abonos where id_paciente=" + id_paciente + " and id_motivo=" + id_motivo;

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AbonosModel abonosModel = new AbonosModel();

                    abonosModel.id_abono = int.Parse(reader[0].ToString());
                    abonosModel.id_paciente = int.Parse(reader[1].ToString());
                    abonosModel.id_motivo = int.Parse(reader[2].ToString());
                    abonosModel.fecha =reader[3].ToString();                 
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

        public double Abonados(int id_motivo)
        {
            double abonado = 0.0;
           
            query = "select  IFNULL(sum(monto),0)as abonado from abonos where id_motivo = "+id_motivo;

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
                ea.escribir(query + ";");
            }
            return abonado;
        }

        public double Restante(int id_motivo)
        {
            double restante = 0.0;

            query = "select IFNULL(((select costo from motivo_cita where id_motivo="+id_motivo+")-(select sum(monto) from abonos where id_motivo ="+ id_motivo+")),0) as restante";

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
        public bool eliminarAbono(int id_abono)
        {
            MySqlCommand cmd;
            query = "DELETE FROM abonos where id_abono=" + id_abono;
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

        public bool insertarAbono(int id_paciente, int id_motivo, string fecha, double monto,string comentario)
        {
            query = "INSERT INTO abonos (id_paciente,id_motivo,fecha,monto,comentario) VALUES("+ id_paciente +","+ id_motivo +",'"+ fecha +"',"+ monto +",'"+comentario+"')";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
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

        public bool actualizarAbono(int id_abono, int id_paciente, int id_motivo, string fecha, double monto)
        {
            query = "UPDATE abonos set id_paciente = "+ id_paciente +",id_motivo = "+ id_motivo +",fecha = '"+ fecha +"',monto = "+ monto +"where id_abono = "+ id_abono;
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                cmd.ExecuteReader();
                conexionBD.Close();
                if (!ti.Test())
                {
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
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

        private bool ValidarExistencia(int id_abono)
        {
            MySqlCommand cmd;
            string query = "SELECT * FROM rol where id_abono="+ id_abono;
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
