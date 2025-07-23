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
    public partial class Form1_Inicio : Form
    {
        public Form1_Inicio()
        {
            InitializeComponent();
            //la ventana inicia en el centro de la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;
            picAsistencia.Enabled = false;
            picSesion.Enabled = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2_Asistencia MostrarOpc1 = new Form2_Asistencia();
            Form3 MostrarOpc2 = new Form3();
            MostrarOpc1.Show();
            MostrarOpc2.Show();
        }

        private void btnRegistrarAsitencia_Click(object sender, EventArgs e)
        {
            Form2_Asistencia Asistencia = new Form2_Asistencia();
            if (Application.OpenForms["Form2_Asistencia"] == null)
            {
                Asistencia.Show();
            }
            else
            {
                Asistencia = (Form2_Asistencia)Application.OpenForms["Form2_Asistencia"];
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
