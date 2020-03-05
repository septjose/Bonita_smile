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
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Servicios
{
    class Motivo_cita
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        Test_Internet ti = new Test_Internet();
        private bool online;
        Configuracion_Model configuracion;

        public Motivo_cita(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
            string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
        }

        public List<Motivo_citaModel> Mostrar_MotivoCita(string id_paciente)
        {
            CultureInfo culture = new CultureInfo("en-US");
            NumberFormatInfo nfi = new CultureInfo("en-US", true).NumberFormat;
            List<Motivo_citaModel> listaMotivo_cita = new List<Motivo_citaModel>();
            query = "select motivo_cita.id_motivo,motivo_cita.descripcion, costo,motivo_cita.id_paciente,motivo_cita.id_clinica,IFNULL(sum(abonos.monto),0.0) as abonado, IFNULL(((motivo_cita.costo)-(sum(abonos.monto))),(motivo_cita.costo)) as restante from motivo_cita LEFT join abonos on motivo_cita.id_motivo=abonos.id_motivo inner JOIN paciente on paciente.id_paciente=motivo_cita.id_paciente WHERE paciente.id_paciente='"+id_paciente+"' GROUP by motivo_cita.id_motivo";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Motivo_citaModel motivo_CitaModel = new Motivo_citaModel();
                    

                    motivo_CitaModel.id_motivo = reader[0].ToString();
                    motivo_CitaModel.descripcion = reader[1].ToString();
                    motivo_CitaModel.costo = double.Parse(reader[2].ToString());
                    double attemp4 = Convert.ToDouble(motivo_CitaModel.costo, culture);
                    motivo_CitaModel.costito = "$" + attemp4.ToString("n", nfi);
                    motivo_CitaModel.id_paciente = reader[3].ToString();
                    motivo_CitaModel.id_clinica = reader[4].ToString();
                    double attemp5 = Convert.ToDouble(reader[5].ToString());
                    motivo_CitaModel.abonado = "$" + attemp5.ToString("n", nfi);
                    double attemp6 = Convert.ToDouble(reader[6].ToString());
                    motivo_CitaModel.restante = "$" + attemp6.ToString("n", nfi);
                    if (attemp6!=0.0)
                    {
                        motivo_CitaModel.imagen_status = null;
                    }
                    else
                    {
                        motivo_CitaModel.imagen_status = LoadImage_Status(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\comprobar.png"));

                    }
                    
                    listaMotivo_cita.Add(motivo_CitaModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaMotivo_cita;
        }

        public bool eliminarMotivo_cita(string id_motivo, string id_paciente,string id_clinica, string alias)
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
                    query = "DELETE FROM motivo_cita WHERE id_motivo = '" + id_motivo + "' AND id_paciente ='" + id_paciente + "' and id_clinica='"+id_clinica+"'";

                    //query = "DELETE FROM motivo_cita where id_motivo='" + id_motivo + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se eliminó correctamente el Motivo: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public bool insertarMotivo_cita(string descripcion, string costo, string id_paciente,string id_clinica, string alias)
        {
            string id_motivo = "";
            Seguridad seguridad = new Seguridad();
            id_motivo = seguridad.SHA1(descripcion + costo + id_paciente +id_clinica+ DateTime.Now);
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
                    query = "INSERT INTO motivo_cita (id_motivo,descripcion,costo,id_paciente,id_clinica) VALUES('" + id_motivo + "','" + descripcion + "'," + costo + ",'" + id_paciente + "','" + id_clinica + "')";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente el Motivo: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public bool actualizarMotivo_cita(string id_motivo, string descripcion, string costo, string id_paciente, string id_clinica, string alias)
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
                    query = "UPDATE motivo_cita set descripcion = '" + descripcion + "',costo = " + costo + " where id_motivo = '" + id_motivo + "' and id_paciente='"+id_paciente+"' and id_clinica='"+id_clinica+"'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente el Motivo: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private BitmapImage LoadImage_Status(string filename)
        {
            BitmapImage bi;


            //MessageBox.Show("se encontro la foto en " + filename);
            var bitmap = new BitmapImage();
            //MessageBox.Show("A");
            var stream = File.OpenRead(filename);
            //MessageBox.Show("B");
            bitmap.BeginInit();
            //MessageBox.Show("C");
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //MessageBox.Show("D");
            bitmap.StreamSource = stream;
            //MessageBox.Show("E");
            bitmap.EndInit();
            //MessageBox.Show("F");
            stream.Close();
            //MessageBox.Show("G");
            stream.Dispose();
            //MessageBox.Show("H");

            return bitmap;

        }
    }
}
