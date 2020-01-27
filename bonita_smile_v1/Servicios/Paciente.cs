using bonita_smile_v1.Modelos;
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
         string ruta = @"C:\bs\";
         string ruta2= @"C:\bs_auxiliar\";
        Test_Internet ti = new Test_Internet();
        private bool online;

        public Paciente(bool online)
        {
            this.conexionBD = obj.conexion(online);
            this.online = online;
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
                    pacienteModel.imagen = LoadImage( reader[5].ToString());
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
                    pacienteModel.imagen = LoadImage(reader[5].ToString());
                    pacienteModel.email = reader[6].ToString();
                    if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                    //pacienteModel.marketing = reader[6].ToString();

                    pacienteModel.antecedente = reader[9].ToString();
                    clinicaModel.id_clinica = reader[10].ToString();
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
            bool internet = ti.Test();
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    query = "DELETE FROM paciente where id_paciente='" + id_paciente + "'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarPaciente(string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica)
        {
            string auxiliar_identificador="";
            Seguridad seguridad = new Seguridad();
            bool internet = ti.Test();
            auxiliar_identificador = seguridad.SHA1(nombre + apellidos + direccion + telefono + foto + antecedente + email + marketing + id_clinica+DateTime.Now);

            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    query = "INSERT INTO paciente (id_paciente,nombre,apellidos,direccion,telefono,foto,antecedente,email,marketing,id_clinica,auxiliar_identificador) VALUES('" + auxiliar_identificador + "','" + nombre + "','" + apellidos + "','" + direccion + "','" + telefono + "','" + foto + "','" + antecedente + "','" + email + "'," + marketing + ",'" + id_clinica + "','<!--" + auxiliar_identificador + "-->')";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarPaciente(string id_paciente, string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica)
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
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE paciente set nombre = '" + nombre + "',apellidos = '" + apellidos + "',direccion = '" + direccion + "',telefono = '" + telefono + "',foto = '" + foto + "',email = '" + email + "',marketing = " + marketing + ",id_clinica =' " + id_clinica + "',antecedente='" + antecedente + "',auxiliar_identificador = '"+ id_paciente +"' where id_paciente ='"+id_paciente +"'";
                    Console.WriteLine(query);

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir(query + ";");
                }
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
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
            string rutisima = "";
            BitmapImage bi;
            bool carpeta_bs = File.Exists(ruta + filename);
            bool carptea_bs_auxilar= File.Exists(ruta2 + filename); 
            if (carpeta_bs && carptea_bs_auxilar )
            {
                try
                {
                    File.Delete(ruta + filename);
                    string destFile = System.IO.Path.Combine(@"C:\bs\", filename);
                    //MessageBox.Show("el valor de result es " + result);
                    System.IO.File.Copy(ruta2 + filename, destFile, true);
                    File.Delete(ruta2 + filename);
                    rutisima = ruta + filename;
                }
                catch (Exception ex)
                {
                    //File.Delete(ruta + filename);
                    //string destFile = System.IO.Path.Combine(@"C:\bs\", filename);
                    //MessageBox.Show("el valor de result es " + result);
                    //System.IO.File.Copy(ruta2 + filename, destFile, true);
                    //File.Delete(ruta2 + filename);
                    rutisima = ruta2 + filename;
                }
               
                
                bi = new BitmapImage(new Uri(rutisima));

            }
            else
                if(File.Exists(ruta+filename))
                {
               // MessageBox.Show("ESTA EN BS"+ruta +filename);
                bi = new BitmapImage(new Uri(ruta + filename));
                }
            
            else
            
                if (File.Exists(ruta2 + filename))
                {
                    bi = new BitmapImage(new Uri(ruta2 + filename));
                }
            else
            {
                bi = new BitmapImage(new Uri(@"C:\bs\img1.jpg"));
            }
                
                
            
            return bi;
        }
    }
}