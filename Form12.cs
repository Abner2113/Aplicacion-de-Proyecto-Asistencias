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
    public partial class Form12 : Form
    {
        private ClsConexion conexion;
        public Form12(string Nombre, string A_Paterno, string A_Materno, string Clave, int Puesto, int Carrera)
        {
            InitializeComponent();
            txtNombres.Text = Nombre;
            txtApellidoP.Text = A_Paterno;
            txtApellidoM.Text = A_Materno;
            txtIdTrabajador.Text = Clave;
            txtIdTrabajador.Enabled = false;
            cmbPuesto.SelectedValue = Puesto;
            cmbCarrera.SelectedValue = Carrera;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            cmbCarrera.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCarrera.Items.Add("TIC");
            cmbCarrera.Items.Add("Gastronomia");
            cmbCarrera.Items.Add("Mecanica");
            cmbCarrera.Items.Add("Contaduria");

            cmbPuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPuesto.Items.Add("Docente");
            cmbPuesto.Items.Add("Secretario");
            cmbPuesto.Items.Add("Administrador");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
