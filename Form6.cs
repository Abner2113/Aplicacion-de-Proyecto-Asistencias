using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacion_de_Proyecto_Asistencias
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            picEmpleado.Enabled = false;
            picAsistencias.Enabled = false;
            picIncidencias.Enabled = false;
            picHorarios.Enabled = false;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.Text = ""; 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            Form4 RegistroEmpleado = new Form4();
            RegistroEmpleado.Show();
        }

        private void btnAsistencias_Click(object sender, EventArgs e)
        {
            Form7 Asistencias = new Form7();
            Asistencias.Show();
        }

        private void btnInsidencias_Click(object sender, EventArgs e)
        {
            Form9 Incidendias = new Form9();
            Incidendias.Show();
        }

        private void btnHorarios_Click(object sender, EventArgs e)
        {
            Form10 Horarios = new Form10();
            Horarios.Show();
        }
    }
}
