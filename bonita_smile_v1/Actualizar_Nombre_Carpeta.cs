using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    public partial class Actualizar_Nombre_Carpeta : Form
    {
        string id_paciente = "";
        string id_carpeta = "";
        string id_motivo = "";
        bool bandera_online_offline = false;
        string alias;
        public Actualizar_Nombre_Carpeta(string id_paciente, string id_carpeta,string id_motivo,string alias)
        {
            MessageBox.Show("el id_motivo "+id_motivo);
            this.id_paciente = id_paciente;
            this.id_motivo = id_motivo;
            this.id_carpeta = id_carpeta;
            this.alias = alias;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           if(!txtAbono.Text.Equals(""))
            {
                Carpeta_archivos ca = new Carpeta_archivos(bandera_online_offline);
                bool insertarCarpeta = ca.actualizarCarpeta_archivos(id_carpeta, txtAbono.Text, id_paciente, id_motivo ,alias);
                if (insertarCarpeta)
                {

                    System.Windows.Forms.MessageBox.Show("Se actualizò Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ca = new Carpeta_archivos(!bandera_online_offline);
                    //ca.actualizarCarpeta_archivos(id_carpeta, txtAbono.Text, id_paciente, id_motivo);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.DialogResult = DialogResult.OK;
            }
           else
            {
                System.Windows.Forms.MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAbono_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}