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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Form8 Usuarios = new Form8();
            Usuarios.Show();
        }
    }
}
