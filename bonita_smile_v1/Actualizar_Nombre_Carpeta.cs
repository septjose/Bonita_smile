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
        public Actualizar_Nombre_Carpeta(string id_paciente, string id_carpeta)
        {
            this.id_paciente = id_paciente;
            this.id_carpeta = id_carpeta;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool insertarCarpeta = new Servicios.Carpeta_archivos().actualizarCarpeta_archivos(id_carpeta, txtAbono.Text, id_paciente);
            if (insertarCarpeta)
            {
                System.Windows.Forms.MessageBox.Show("Se actualizò Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;
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