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
    class Nota_de_digi_evolucion
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        private string prueba_remota = "prueba";
        Configuracion_Model configuracion;

        public Nota_de_digi_evolucion(bool online)
        {
            //MessageBox.Show(prueba_remota);
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;

        }

        public List<Nota_de_digi_evolucionModel> MostrarNota_de_digi_evolucion(string id_motivo, string id_paciente)
        {
            List<Nota_de_digi_evolucionModel> listaNota_de_digi_evolucion = new List<Nota_de_digi_evolucionModel>();
            query = "SELECT nota_de_digi_evolucion.id_nota,nota_de_digi_evolucion.id_paciente,nota_de_digi_evolucion.id_motivo,nota_de_digi_evolucion.descripcion,date_format(nota_de_digi_evolucion.fecha, '%d/%m/%Y') as fecha,nota_de_digi_evolucion.nombre_doctor,carpeta_archivos.id_carpeta,carpeta_archivos.nombre_carpeta FROM nota_de_digi_evolucion inner join carpeta_archivos on carpeta_archivos.id_nota=nota_de_digi_evolucion.id_nota where nota_de_digi_evolucion.id_paciente='" + id_paciente + "' and nota_de_digi_evolucion.id_motivo='" + id_motivo + "' ";
            Console.WriteLine(query);
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Nota_de_digi_evolucionModel nota_De_Digi_EvolucionModel = new Nota_de_digi_evolucionModel();
                    Carpeta_archivosModel carpeta = new Carpeta_archivosModel();

                    nota_De_Digi_EvolucionModel.id_nota = reader[0].ToString();
                    nota_De_Digi_EvolucionModel.id_paciente = reader[1].ToString();
                    nota_De_Digi_EvolucionModel.id_motivo = reader[2].ToString();
                    nota_De_Digi_EvolucionModel.descripcion = reader[3].ToString();
                    nota_De_Digi_EvolucionModel.fecha = reader[4].ToString();
                    nota_De_Digi_EvolucionModel.nombre_doctor = reader[5].ToString();
                    carpeta.id_carpeta = reader[6].ToString();
                    carpeta.nombre_carpeta = reader[7].ToString();
                    carpeta.id_paciente = reader[1].ToString();
                    carpeta.id_motivo = reader[2].ToString();
                    carpeta.id_nota = reader[0].ToString();
                    nota_De_Digi_EvolucionModel.carpeta = carpeta;

                    listaNota_de_digi_evolucion.Add(nota_De_Digi_EvolucionModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            conexionBD.Close();
            return listaNota_de_digi_evolucion;
        }

        public bool eliminarNotaEvolucion(string id_nota, string id_paciente, string id_motivo, string alias)
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

                    query = "DELETE FROM nota_de_digi_evolucion WHERE id_nota = '" + id_nota + "' AND id_paciente ='" + id_paciente + "' AND id_motivo = '" + id_motivo + "'";
                    //query = "DELETE FROM nota_de_digi_evolucion where id_nota='" + id_nota + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se eliminó correctamente la Nota de evolución: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public bool insertarNota_de_digi_evolucion(string id_paciente, string id_motivo, string descripcion_motivo, string descripcion, string fecha, string nombre_doctor, string alias)
        {
            Seguridad seguridad = new Seguridad();
            string auxiliar_identificador_nota = seguridad.SHA1(id_paciente + id_motivo + descripcion + fecha + nombre_doctor + DateTime.Now);
            MySqlTransaction tr = null;
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
                    int no_carpeta = 0;
                    query = "select count(descripcion) from  nota_de_digi_evolucion where id_paciente='" + id_paciente + "' and id_motivo='" + id_motivo + "'";
                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        no_carpeta = Int32.Parse(reader[0].ToString());
                    }

                    conexionBD.Close();
                    string nombre_carpeta = "No_Carpeta_" + no_carpeta + "_" + DateTime.Now.ToString("dd/MM/yyyy");
                    string auxiliar_identificador_carpeta = seguridad.SHA1(nombre_carpeta + DateTime.Now);
                    // -----------HACER TRANSACCION-------- -/

                    query = "INSERT INTO nota_de_digi_evolucion (id_nota,id_paciente,id_motivo,descripcion,fecha,auxiliar_identificador,nombre_doctor) VALUES('" + auxiliar_identificador_nota + "','" + id_paciente + "','" + id_motivo + "','" + descripcion + "','" + fecha + "','<!--" + auxiliar_identificador_nota + "-->','" + nombre_doctor + "');";
                    query = query + "INSERT INTO carpeta_archivos (id_carpeta,nombre_carpeta,id_paciente,id_motivo,auxiliar_identificador,id_nota) VALUES('" + auxiliar_identificador_carpeta + "','" + nombre_carpeta + "','" + id_paciente + "','" + id_motivo + "','<!--" + auxiliar_identificador_carpeta + "-->','" + auxiliar_identificador_nota + "')";

                    conexionBD.Open();

                    //tr = conexionBD.BeginTransaction();

                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    //tr.Commit();

                    conexionBD.Close();
                    // ---------------------------------------/

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente la Nota de evolución: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar insertar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tr.Rollback();
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarNota_de_digi_evolucion(string id_nota, string id_paciente, string id_motivo, string descripcion, string fecha, string nombre_doctor, string alias)
        {

            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

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
                    query = "UPDATE nota_de_digi_evolucion set id_paciente ='" + id_paciente + "',id_motivo = '" + id_motivo + "',descripcion = '" + descripcion + "',fecha = '" + fecha + "',auxiliar_identificador = '" + id_nota + "',nombre_doctor='" + nombre_doctor + "' where id_nota = '" + id_nota + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente la Nota de evolución: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


    }
}
