using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace bonita_smile_v1.Servicios
{
    class Abonos_Membresia
    {
        private MySqlDataReader reader = null;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        Configuracion_Model configuracion;
        CultureInfo culture = new CultureInfo("en-US");
        NumberFormatInfo nfi = new CultureInfo("en-US", true).NumberFormat;

        public Abonos_Membresia(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
        }

        public List<abonos_membresiaModel> MostrarAbonosMembresia(string id_membresia, string id_paciente, string id_clinica)
        {
          
            List<abonos_membresiaModel> listaAbonosMembresia = new List<abonos_membresiaModel>();
            string query = "SELECT id_abono_membresia,date_format(fecha, '%d/%m/%Y') as fecha,monto,comentario from abonos_membresia where id_membresia= '" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
            Console.WriteLine(query);
            try
            {
                using (MySqlConnection conexion1 = obj.conexion(online))
                {
                    conexion1.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion1))
                    {
                        //conexionBD.Close();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                abonos_membresiaModel abonosMembresia = new abonos_membresiaModel();

                                MembresiaModel membresia = new MembresiaModel();
                                PacienteModel paciente = new PacienteModel();
                                ClinicaModel clinica = new ClinicaModel();

                                abonosMembresia.id_abono_membresia = reader[0].ToString();
                                abonosMembresia.fecha = reader[1].ToString();
                                double attemp4 = Convert.ToDouble(reader[2].ToString());

                                abonosMembresia.monto = attemp4.ToString("n", nfi);
                                abonosMembresia.montito = double.Parse(reader[2].ToString());
                                abonosMembresia.comentario = reader[3].ToString();

                                // ----------CONSULTA PARA OBTENER SUBOBJETOS DE LA BS-----------------//
                                //1.-OBTENER SUBOBJETO PACIENTE CON RESPECTO A LA MEMBRESIA PREVIAMENTE EXTRAIDA

                                //conexionBD.Open();

                                string query2 = "SELECT * from membresia where id_membresia='" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
                                Console.WriteLine(query2);
                                using (MySqlConnection conexion2 = obj.conexion(online))
                                {
                                    conexion2.Open();

                                    //conexionBD.Close();
                                    using (MySqlCommand cmd2 = new MySqlCommand(query2, conexion2))
                                    {
                                        using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                membresia.id_membresia = reader2[0].ToString();
                                                membresia.membresia = reader2[2].ToString();
                                                membresia.costo = reader2[4].ToString();
                                                //conexionBD.Open();
                                                //2.-OBTENER SUBOBJETO PACIENTE CON RESPECTO AL MOTIVO_CITA PREVIAMENTE EXTRAIDO
                                                string query3 = "select * from paciente where id_paciente='" + id_paciente + "' and id_clinica= '" + id_clinica + "'";
                                                Console.WriteLine(query3);
                                                //conexionBD.Close();
                                                using (MySqlConnection conexion3 = obj.conexion(online))
                                                {
                                                    conexion3.Open();
                                                    using (MySqlCommand cmd3 = new MySqlCommand(query3, conexion3))
                                                    {
                                                        using (MySqlDataReader reader3 = cmd3.ExecuteReader())
                                                        {
                                                            while (reader3.Read())
                                                            {
                                                                paciente.id_paciente = reader3[0].ToString();
                                                                paciente.nombre = reader3[1].ToString();
                                                                paciente.apellidos = reader3[2].ToString();
                                                                paciente.direccion = reader3[3].ToString();
                                                                paciente.telefono = reader3[4].ToString();
                                                                paciente.foto = reader3[5].ToString();
                                                                paciente.email = reader3[6].ToString();
                                                                if (reader3[7].ToString() == "False") { paciente.marketing = 0; } else { paciente.marketing = 1; }
                                                                paciente.antecedente = reader3[9].ToString();
                                                                //paciente.membresia = reader3[11].ToString();

                                                                string query4 = "select * from clinica where id_clinica='" + id_clinica + "'";
                                                                Console.WriteLine(query4);
                                                                using (MySqlConnection conexion4 = obj.conexion(online))
                                                                {
                                                                    conexion4.Open();
                                                                    using (MySqlCommand cmd4 = new MySqlCommand(query4, conexion4))
                                                                    {
                                                                        using (MySqlDataReader reader4 = cmd4.ExecuteReader())
                                                                        {
                                                                            while (reader3.Read())
                                                                            {
                                                                                clinica.id_clinica = reader4[0].ToString();
                                                                                clinica.nombre_sucursal = reader4[1].ToString();
                                                                                clinica.color = reader4[2].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                paciente.clinica = clinica;
                                membresia.paciente = paciente;
                                abonosMembresia.membresia = membresia;
                                listaAbonosMembresia.Add(abonosMembresia);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                conexionBD.Close();
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaAbonosMembresia;
        }
        public abonos_membresiaModel MostrarAbonoMembresia(string id_abono_membresia, string id_membresia, string id_paciente, string id_clinica)
        {
            string query = "SELECT * from abonos_membresia where id_abono_membresia= '" + id_abono_membresia + "' id_membresia= '" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
            abonos_membresiaModel abonosMembresia = new abonos_membresiaModel();

            MembresiaModel membresia = new MembresiaModel();
            PacienteModel paciente = new PacienteModel();
            ClinicaModel clinica = new ClinicaModel();
            try
            {
                using (MySqlConnection conexion1 = obj.conexion(online))
                {
                    conexion1.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion1))
                    {
                        //conexionBD.Close();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                abonosMembresia.id_abono_membresia = reader[0].ToString();
                                abonosMembresia.fecha = reader[1].ToString();
                                abonosMembresia.monto = reader[2].ToString();
                                abonosMembresia.comentario = reader[3].ToString();

                                // ----------CONSULTA PARA OBTENER SUBOBJETOS DE LA BS-----------------//
                                //1.-OBTENER SUBOBJETO PACIENTE CON RESPECTO A LA MEMBRESIA PREVIAMENTE EXTRAIDA

                                //conexionBD.Open();

                                string query2 = "SELECT * from membresia where id_membresia='" + id_membresia + "' id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
                                using (MySqlConnection conexion2 = obj.conexion(online))
                                {
                                    conexion2.Open();

                                    //conexionBD.Close();
                                    using (MySqlCommand cmd2 = new MySqlCommand(query2, conexion2))
                                    {
                                        using (MySqlDataReader reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                membresia.id_membresia = reader2[0].ToString();
                                                membresia.membresia = reader2[2].ToString();
                                                membresia.costo = reader2[4].ToString();
                                                //conexionBD.Open();
                                                //2.-OBTENER SUBOBJETO PACIENTE CON RESPECTO AL MOTIVO_CITA PREVIAMENTE EXTRAIDO
                                                string query3 = "select * from paciente where id_paciente='" + id_paciente + "' and id_clinica= '" + id_clinica + "'";
                                                //conexionBD.Close();
                                                using (MySqlConnection conexion3 = obj.conexion(online))
                                                {
                                                    conexion3.Open();
                                                    using (MySqlCommand cmd3 = new MySqlCommand(query3, conexion3))
                                                    {
                                                        using (MySqlDataReader reader3 = cmd3.ExecuteReader())
                                                        {
                                                            while (reader3.Read())
                                                            {
                                                                paciente.id_paciente = reader3[0].ToString();
                                                                paciente.nombre = reader3[1].ToString();
                                                                paciente.apellidos = reader3[2].ToString();
                                                                paciente.direccion = reader3[3].ToString();
                                                                paciente.telefono = reader3[4].ToString();
                                                                paciente.foto = reader3[5].ToString();
                                                                paciente.email = reader3[6].ToString();
                                                                if (reader2[7].ToString() == "False") { paciente.marketing = 0; } else { paciente.marketing = 1; }
                                                                paciente.antecedente = reader3[9].ToString();
                                                                paciente.membresia = reader3[10].ToString();

                                                                string query4 = "select * from clinica where id_clinica='" + id_clinica + "'";

                                                                using (MySqlConnection conexion4 = obj.conexion(online))
                                                                {
                                                                    conexion4.Open();
                                                                    using (MySqlCommand cmd4 = new MySqlCommand(query4, conexion4))
                                                                    {
                                                                        using (MySqlDataReader reader4 = cmd3.ExecuteReader())
                                                                        {
                                                                            while (reader3.Read())
                                                                            {
                                                                                clinica.id_clinica = reader3[0].ToString();
                                                                                clinica.nombre_sucursal = reader3[1].ToString();
                                                                                clinica.color = reader3[2].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                paciente.clinica = clinica;
                                membresia.paciente = paciente;
                                abonosMembresia.membresia = membresia;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                conexionBD.Close();
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return abonosMembresia;
        }
        public bool InsertarAbonoMembresia(string fecha, string monto, string comentario, string id_membresia, string id_paciente, string id_clinica, string alias)
        {
            Seguridad seguridad = new Seguridad();
            string id_abono_membresia = "";
            id_abono_membresia = seguridad.SHA1(fecha + monto + comentario + id_membresia + id_paciente + id_clinica + DateTime.Now);
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

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
                    string query = "INSERT INTO abonos_membresia (id_abono_membresia,fecha,monto,comentario,id_membresia,id_paciente,id_clinica) VALUES('" + id_abono_membresia + "','" + fecha + "','" + monto + "','" + comentario + "','" + id_membresia + "','" + id_paciente + "','" + id_clinica + "')";
                    Console.WriteLine(query);
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente el Abono de membresia: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar insertar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }
        public bool ActualizarAbonoMembresia(string id_abono_membresia, string fecha, string monto, string comentario, string id_membresia, string id_paciente, string id_clinica, string alias)
        {
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

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
                    string query = "UPDATE abonos_membresia set fecha ='" + fecha + "',monto = '" + monto + "',comentario = '" + comentario + "' where id_abono_membresia='" + id_abono_membresia + "' and id_membresia = '" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente el Abono de membresia: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar actualizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }
        public bool EliminarAbonoMembresia(string id_abono_membresia, string id_membresia, string id_paciente, string id_clinica, string alias)
        {
            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

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
                    string query = "DELETE FROM abonos_membresia where id_abono_membresia='" + id_abono_membresia + "' and id_membresia='" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se eliminó correctamente el Abono: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar eliminar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public double Abonados(string id_membresia, string id_paciente, string id_clinica)
        {
            double abonado = 0.0;

            string query = "select  IFNULL(sum(monto),0)as abonado from abonos_membresia where id_membresia = '" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";

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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();

            return abonado;
        }

        public double Restante(string id_membresia)
        {
            double restante = 0.0;
            string query = "select IFNULL(((select costo from membresia where id_membresia='" + id_membresia + "')-(select sum(monto) from abonos_membresia where id_membresia ='" + id_membresia + "')),(select costo from membresia where id_membresia='" + id_membresia + "')) as restante;";
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
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return restante;
        }


    }
}