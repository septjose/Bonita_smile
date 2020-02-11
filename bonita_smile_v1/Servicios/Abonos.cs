using System;
using System.Collections.Generic;
using System.Globalization;
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
        //se pudo
        //si se puede
        //lklklklk
        //hola
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        CultureInfo culture = new CultureInfo("en-US");
        NumberFormatInfo nfi = new CultureInfo("en-US", true).NumberFormat;
        public Abonos(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
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
                    double attemp4 = Convert.ToDouble(abonosModel.monto, culture);
                    abonosModel.costito = "$" + attemp4.ToString("n", nfi);
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
           
            return abonado;
        }

        public double Restante(string id_motivo)
        {
            double restante = 0.0;
            query = "select IFNULL(((select costo from motivo_cita where id_motivo='" + id_motivo + "')-(select sum(monto) from abonos where id_motivo ='" + id_motivo + "')),(select costo from motivo_cita where id_motivo='" + id_motivo + "')) as restante;";
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
            bool internet = ti.Test();
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "DELETE FROM abonos where id_abono='" + id_abono + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                    return true;
                }
               
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }


        public bool insertarAbono(string id_paciente, string id_motivo, string fecha, string monto, string comentario)
        {
            Seguridad seguridad = new Seguridad();
            string id_abono = "";
            id_abono = seguridad.SHA1(id_paciente + id_motivo + fecha + monto + comentario + DateTime.Now);           
            bool internet = ti.Test();
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO abonos (id_abono,id_paciente,id_motivo,fecha,monto,comentario,auxiliar_identificador) VALUES('" + id_abono + "','" + id_paciente + "','" + id_motivo + "','" + fecha + "'," + monto + ",'" + comentario + "','<!--" + id_abono + "-->')";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarAbono(string id_abono, string id_paciente, string id_motivo, string fecha, string monto,string comentario)
        {          
            bool internet = ti.Test();
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE abonos set id_paciente ='"+ id_paciente + "',id_motivo = '" + id_motivo + "',fecha = '" + fecha + "',monto = " + monto + ",comentario='" + comentario + "',auxiliar_identificador = '" + id_abono + "'where id_abono = '" + id_abono + "'";
                    Console.WriteLine(query);
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();
                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                    return true;
                } 
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        private List<string> Mostrar_ids_clinicas()
        {
            List<string> lista = new List<string>();
            query = "select id_clinica from clinica";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                  lista.Add( reader[0].ToString());   
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return lista;
        }


        public List<Ganancias> Mostrar_Ganancias()
        {
            List<string> lista = Mostrar_ids_clinicas();
            List<Ganancias> lista_ganancias = new List<Ganancias>();
            foreach(var id in lista)
            {
                query = "select c.nombre_sucursal,IFNULL(sum(monto), 0) as ganancia,IFNULL(date_format(a.fecha, '%d/%m/%Y'), '') as fecha from abonos a inner JOIN paciente p on a.id_paciente = p.id_paciente INNER join clinica c on c.id_clinica = p.id_clinica where c.id_clinica = '"+id+"'";
                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Ganancias ganancia = new Ganancias();

                        ganancia.clinica = reader[0].ToString();
                        ganancia.ganancia = double.Parse(reader[1].ToString());
                        ganancia.fecha = reader[2].ToString();

                        lista_ganancias.Add(ganancia);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conexionBD.Close();
            }
            
            return lista_ganancias;
        }


        public List<Ganancias> Mostrar_Ganancias_Socio(List<string>list)
        {
            List<string> lista = list;
            List<Ganancias> lista_ganancias = new List<Ganancias>();
            foreach (var id in lista)
            {
                query = "select c.nombre_sucursal,IFNULL(sum(monto), 0) as ganancia,IFNULL(date_format(a.fecha, '%d/%m/%Y'), '') as fecha from abonos a inner JOIN paciente p on a.id_paciente = p.id_paciente INNER join clinica c on c.id_clinica = p.id_clinica where c.id_clinica = '" + id + "'";
                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Ganancias ganancia = new Ganancias();

                        ganancia.clinica = reader[0].ToString();
                        ganancia.ganancia = double.Parse(reader[1].ToString());
                        ganancia.fecha = reader[2].ToString();

                        lista_ganancias.Add(ganancia);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conexionBD.Close();
            }

            return lista_ganancias;
        }


        public List<Ganancias> Ganacioas_c_clinica(string id)
        {
            List<Ganancias> lista_ganancias = new List<Ganancias>();
            query = "select c.nombre_sucursal,IFNULL(sum(monto), 0) as ganancia,IFNULL(date_format(a.fecha, '%d/%m/%Y'), '') as fecha from abonos a inner JOIN paciente p on a.id_paciente = p.id_paciente INNER join clinica c on c.id_clinica = p.id_clinica where c.id_clinica = '" + id + "'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ganancias ganancia = new Ganancias();

                    ganancia.clinica = reader[0].ToString();
                    ganancia.ganancia = double.Parse(reader[1].ToString());
                    ganancia.fecha = reader[2].ToString();

                    lista_ganancias.Add(ganancia);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return lista_ganancias;
        }

        public List<Ganancias> Ganacioas_c_clinica_fecha(string id,string fecha)
        {
            List<Ganancias> lista_ganancias = new List<Ganancias>();
            query = "select c.nombre_sucursal,IFNULL(sum(monto), 0) as ganancia,IFNULL(date_format(a.fecha, '%d/%m/%Y'),'"+fecha+"') as fecha from abonos a inner JOIN paciente p on a.id_paciente = p.id_paciente INNER join clinica c on c.id_clinica = p.id_clinica where c.id_clinica = '" + id + "' and a.fecha='"+fecha+"'";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ganancias ganancia = new Ganancias();

                    ganancia.clinica = reader[0].ToString();
                    ganancia.ganancia = double.Parse(reader[1].ToString());
                    ganancia.fecha = reader[2].ToString();

                    lista_ganancias.Add(ganancia);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return lista_ganancias;
        }

        public List<Ganancias> Ganacioas_c_clinica_fecha2(string id, string fecha,string fecha2)
        {
            List<Ganancias> lista_ganancias = new List<Ganancias>();
            query = "select c.nombre_sucursal,IFNULL(sum(monto), 0) as ganancia,IFNULL(date_format(a.fecha, '%d/%m/%Y'),'" + fecha + "'' - ''"+fecha2+"') as fecha from abonos a inner JOIN paciente p on a.id_paciente = p.id_paciente INNER join clinica c on c.id_clinica = p.id_clinica where c.id_clinica = '"+id+"' and (a.fecha BETWEEN '"+fecha+"' AND '"+fecha2+"')";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Ganancias ganancia = new Ganancias();

                    ganancia.clinica = reader[0].ToString();
                    ganancia.ganancia = double.Parse(reader[1].ToString());
                    ganancia.fecha = reader[2].ToString();

                    lista_ganancias.Add(ganancia);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexionBD.Close();
            return lista_ganancias;
        }

    }
}
