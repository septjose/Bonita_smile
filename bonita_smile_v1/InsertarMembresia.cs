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
    public partial class InsertarMembresia : Form
    {
        PacienteModel paciente = new PacienteModel();
        bool bandera_online_offline = false;
        public InsertarMembresia(PacienteModel paciente)
        {
            this.paciente = paciente;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(paciente.nombre);
                bool inserto = new Paciente(bandera_online_offline).actualizarMembresia(paciente);
                if (inserto)
                {
                    System.Windows.Forms.MessageBox.Show("El paciente " + paciente.nombre + " " + paciente.apellidos + " es ahora miembro", "Se ingreso correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // -----------------------------------------------/
                        inserto = new Paciente(!bandera_online_offline).actualizarMembresia(paciente);
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
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}