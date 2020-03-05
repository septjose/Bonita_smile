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
    public partial class Actualizar_Fecha_Membresia : Form
    {
        string id_membresia;
        string id_paciente;
        string id_clinica;
        string alias;
        public Actualizar_Fecha_Membresia(string id_membresia, string id_paciente, string id_clinica, string alias)
        {
            InitializeComponent();
            this.id_membresia = id_membresia;
            this.id_paciente = id_paciente;
            this.id_clinica = id_clinica;
            this.alias = alias;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Membresia abo = new Membresia(false);

            bool elimino = abo.ActualizarMembresia(id_membresia, id_paciente, datetimepicker1.Value.ToString("yyyy/MM/dd"), id_clinica, alias);
            if (elimino)
            {
                this.DialogResult = DialogResult.OK;
                //abo = new Abonos(!bandera_online_offline);
                //abo.eliminarAbono(abono.id_abono);

                // System.Windows.Forms.MessageBox.Show("Se elimino la membresia correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("No se pudo eliminar el abono", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
