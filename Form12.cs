using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Aplicacion_de_Proyecto_Asistencias
{
    public partial class Form12 : Form
    {
        private int puestos;
        private int carreras;
        private ClsConexion conexion;
        public Form12(string Nombre, string A_Paterno, string A_Materno, string Clave, int Puesto, int Carrera)
        {
            InitializeComponent();
            puestos = Puesto;
            carreras = Carrera;
            txtNombres.Text = Nombre;
            txtApellidoP.Text = A_Paterno;
            txtApellidoM.Text = A_Materno;
            txtIdTrabajador.Text = Clave;
            txtIdTrabajador.Enabled = false;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.Text = "";
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

            cmbPuesto.SelectedIndex = puestos - 1;
            cmbCarrera.SelectedIndex = carreras - 1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidoP.Text) || string.IsNullOrEmpty(txtApellidoM.Text) || string.IsNullOrEmpty(txtIdTrabajador.Text) || cmbCarrera.SelectedIndex == -1 || cmbPuesto.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios");
                return;
            }
            string nombre = txtNombres.Text;
            string ApellidoP = txtApellidoP.Text;
            string ApellidoM = txtApellidoM.Text;
            string idTrabajador = txtIdTrabajador.Text;
            int carrera = Convert.ToInt32(cmbCarrera.SelectedIndex + 1);
            int puesto = Convert.ToInt32(cmbPuesto.SelectedIndex + 1);

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string consulta = "UPDATE empleado SET Nombre = @Nombre, Apellido_Paterno = @A_Paterno, Apellido_Materno = @A_Materno, Id_puesto = @Id_Puesto, Id_Carrera = @Id_Carrera WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand adapter = new MySqlCommand(consulta, con);
            adapter.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            adapter.Parameters.AddWithValue("@Nombre", nombre);
            adapter.Parameters.AddWithValue("@A_Paterno", ApellidoP);
            adapter.Parameters.AddWithValue("@A_Materno", ApellidoM);
            adapter.Parameters.AddWithValue("@Id_Puesto", puesto);
            adapter.Parameters.AddWithValue("@Id_Carrera", carrera);
            adapter.ExecuteNonQuery();

            MessageBox.Show("Se actualizo correctamente");

        }
    }
}
