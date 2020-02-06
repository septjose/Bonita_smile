using bonita_smile_v1.Modelos;
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
    public partial class EliminarMembresia : Form
    {
        PacienteModel paciente = new PacienteModel();
        bool bandera_online_offline = false;
        public EliminarMembresia(PacienteModel paciente)
        {
            this.paciente = paciente;
            InitializeComponent();

            label1.Text = "El paciente: " + paciente.nombre + " " + paciente.apellidos + " cuanta con una suscipción activa, ¿Desea cancelar la membresia?";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Está seguro que desea eliminar la membresia del paciente? " + paciente.nombre + " " + paciente.apellidos, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    bool elimino = new Paciente(bandera_online_offline).eliminarMembresia(paciente);
                    if (elimino)
                    {
                        System.Windows.Forms.MessageBox.Show("Se Eliminó la membresia", "Se Eliminó", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // -----------------------------------------------/
                        elimino = new Paciente(!bandera_online_offline).eliminarMembresia(paciente);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo  Eliminar la membresia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex + "");
                    this.DialogResult = DialogResult.OK;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}