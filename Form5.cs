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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
            Graphics linea = e.Graphics;
            linea.DrawLine(Pens.Black, 100, 240, 400, 240);
            linea.DrawLine(Pens.Black, 100, 286, 400, 286);
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
