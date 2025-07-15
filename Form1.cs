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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            picAsistencia.Enabled = false;
            picSesion.Enabled = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 MostrarOpc1 = new Form2();
            Form3 MostrarOpc2 = new Form3();
            MostrarOpc1.Show();
            MostrarOpc2.Show();
        }

        private void btnRegistrarAsitencia_Click(object sender, EventArgs e)
        {
            Form2 Asistencia = new Form2();
            if (Application.OpenForms["Form2"] == null)
            {
                Asistencia.Show();
            }
            else
            {
                Asistencia = (Form2)Application.OpenForms["Form2"];
                Asistencia.BringToFront();
                Asistencia.Activate();
            }
        }
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Form3 inicioSesion = new Form3();
            if (Application.OpenForms["Form3"] == null)
            {
                inicioSesion.Show();
            }
            else
            {
                inicioSesion = (Form3)Application.OpenForms["Form3"];
                inicioSesion.BringToFront();
                inicioSesion.Activate();
            }
        }
    }
}
