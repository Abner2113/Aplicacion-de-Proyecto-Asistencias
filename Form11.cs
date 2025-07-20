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
    public partial class Form11 : Form
    {
        private ClsConexion conexion;
        private string Id_Trabajador;

        public Form11(string Id_Trabajador)
        {
            InitializeComponent();
            Id_Trabajador = Id_Trabajador;
            conexion = new ClsConexion();
            CargarHorario(Id_Trabajador);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void CargarHorario(string Id_Trabajador)
        {
            try
            {
                string query = "SELECT d.dia, h.H_Entrada, h.H_Salida, h.Periodo " +
                               "FROM Horario h " +
                               "INNER JOIN Dias d ON h.Id_Dia = d.Id_dia " +
                               "WHERE h.Id_Trabajador = @Id_Trabajador";
                MySqlCommand cmd = new MySqlCommand(query, conexion.getConnection());
                cmd.Parameters.AddWithValue("@Id_Trabajador", Id_Trabajador);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvHorarios.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el horario: " + ex.Message);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            cmbDia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDia.Items.Add("Lunes");
            cmbDia.Items.Add("Martes");
            cmbDia.Items.Add("Miércoles");
            cmbDia.Items.Add("Jueves");
            cmbDia.Items.Add("Viernes");
            cmbDia.SelectedIndex = -1;
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.Text = "2025-2";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
