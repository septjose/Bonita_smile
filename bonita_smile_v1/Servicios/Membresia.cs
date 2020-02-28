using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace bonita_smile_v1.Servicios
{
    class Membresia
    {
        private MySqlDataReader reader = null;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        Configuracion_Model configuracion;

        public Membresia(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
        }
        public List<MembresiaModel> MostrarMembresias(string id_paciente, string id_clinica)
        {
            List<MembresiaModel> listaMembresia = new List<MembresiaModel>();
            string query = "SELECT * from membresia where id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";

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
                                MembresiaModel membresia = new MembresiaModel();
                                PacienteModel paciente = new PacienteModel();
                                ClinicaModel clinica = new ClinicaModel();

                                membresia.id_membresia = reader[0].ToString();
                                membresia.membresia = reader[2].ToString();
                                membresia.costo = reader[4].ToString();

                                // ----------CONSULTA PARA OBTENER SUBOBJETOS DE LA BS-----------------//
                                //1.-OBTENER SUBOBJETO PACIENTE CON RESPECTO A LA MEMBRESIA PREVIAMENTE EXTRAIDA

                                //conexionBD.Open();

                                string query2 = "select * from paciente where id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
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
                                                paciente.id_paciente = reader2[0].ToString();
                                                paciente.nombre = reader2[1].ToString();
                                                paciente.apellidos = reader2[2].ToString();
                                                paciente.direccion = reader2[3].ToString();
                                                paciente.telefono = reader2[4].ToString();
                                                paciente.foto = reader2[5].ToString();
                                                paciente.email = reader2[6].ToString();
                                                if (reader2[7].ToString() == "False") { paciente.marketing = 0; } else { paciente.marketing = 1; }
                                                paciente.antecedente = reader2[9].ToString();
                                                paciente.membresia = reader2[10].ToString();

                                                //conexionBD.Open();
                                                //2.-OBTENER SUBOBJETO PACIENTE CON RESPECTO AL MOTIVO_CITA PREVIAMENTE EXTRAIDO
                                                string query3 = "select * from clinica where id_clinica='" + id_clinica + "'";

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
                                                                clinica.id_clinica = reader3[0].ToString();
                                                                clinica.nombre_sucursal = reader3[1].ToString();
                                                                clinica.color = reader3[2].ToString();
                                                                //conexionBD.Open();
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
                                listaMembresia.Add(membresia);
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
            return listaMembresia;
        }
        public MembresiaModel MostrarMembresia(string id_membresia, string id_paciente, string id_clinica)
        {
            string query = "SELECT * from membresia where id_membresia='" + id_membresia + "' id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
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
                                membresia.id_membresia = reader[0].ToString();
                                membresia.membresia = reader[2].ToString();
                                membresia.costo = reader[4].ToString();

                                // ----------CONSULTA PARA OBTENER SUBOBJETOS DE LA BS-----------------//
                                //1.-OBTENER SUBOBJETO PACIENTE CON RESPECTO A LA MEMBRESIA PREVIAMENTE EXTRAIDA

                                //conexionBD.Open();

                                string query2 = "select * from paciente where id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
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
                                                paciente.id_paciente = reader2[0].ToString();
                                                paciente.nombre = reader2[1].ToString();
                                                paciente.apellidos = reader2[2].ToString();
                                                paciente.direccion = reader2[3].ToString();
                                                paciente.telefono = reader2[4].ToString();
                                                paciente.foto = reader2[5].ToString();
                                                paciente.email = reader2[6].ToString();
                                                if (reader2[7].ToString() == "False") { paciente.marketing = 0; } else { paciente.marketing = 1; }
                                                paciente.antecedente = reader2[9].ToString();
                                                paciente.membresia = reader2[10].ToString();

                                                //conexionBD.Open();
                                                //2.-OBTENER SUBOBJETO PACIENTE CON RESPECTO AL MOTIVO_CITA PREVIAMENTE EXTRAIDO
                                                string query3 = "select * from clinica where id_clinica='" + id_clinica + "'";

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
                                                                clinica.id_clinica = reader3[0].ToString();
                                                                clinica.nombre_sucursal = reader3[1].ToString();
                                                                clinica.color = reader3[2].ToString();
                                                                //conexionBD.Open();
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
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                //conexionBD.Close();
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //conexionBD.Close();
            return membresia;
        }
        public bool InsertarMembresia(string id_paciente, string membresia, string id_clinica, string costo, string alias)
        {
            Seguridad seguridad = new Seguridad();
            string id_membresia = "";
            id_membresia = seguridad.SHA1(id_paciente + membresia + id_clinica + costo + DateTime.Now);
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
                    string query = "INSERT INTO membresia (id_membresia,id_paciente,membresia,id_clinica,costo) VALUES('" + id_membresia + "','" + id_paciente + "','" + membresia + "','" + id_clinica + "','" + costo + "')";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente la Membresía: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        public bool ActualizarMembresia(string id_membresia, string id_paciente, string membresia, string id_clinica, string costo, string alias)
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
                    string query = "UPDATE membresia set membresia ='" + membresia + "',costo = '" + costo + "' where id_membresia = '" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt"); 
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente la Mmebresía: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public bool EliminarMembresia(string id_membresia, string id_paciente, string id_clinica, string alias)
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
                    string query = "DELETE FROM membresia where id_membresia='" + id_membresia + "' and id_paciente='" + id_paciente + "' and id_clinica='" + id_clinica + "'";

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
    }
}