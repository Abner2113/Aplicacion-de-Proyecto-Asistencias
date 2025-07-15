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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cmbPuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPuesto.Items.Add("Empleado");
            cmbPuesto.Items.Add("Administrador");
            cmbPuesto.SelectedIndex = 0;
            
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics linea = e.Graphics;
            linea.DrawLine(Pens.Black, 190, 190, 410, 190);
            linea.DrawLine(Pens.Black, 190, 470, 410, 470);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1 salir = (Form1)Application.OpenForms["Form1"];
            salir.BringToFront();
            salir.Activate();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Form5 Empleado = new Form5();
            Form6 Administrador = new Form6();
            if(cmbPuesto.SelectedIndex == 0 )
            {
                Empleado.Show();
            }
            else if (cmbPuesto.SelectedIndex == 1 )
            {
                Administrador.Show();
            }
            else
            {
                MessageBox.Show("ERROR 404");
            }
        }
    }
}
