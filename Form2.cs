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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);
            time.Start();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtEstado.ReadOnly = true;
            txtEstado.Text = "si";
        }

        private void time_Tick(object sender, EventArgs e)
        {
            lblTiempo.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1 salir = (Form1)Application.OpenForms["Form1"];
            salir.BringToFront();
            salir.Activate();
        }
    }
}
