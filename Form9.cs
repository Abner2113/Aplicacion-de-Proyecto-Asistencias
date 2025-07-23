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
    public partial class Form9 : Form
    {
        private ClsConexion conexion;

        public Form9()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            picJustificado.Enabled = false;
            picNoJustificado.Enabled = false;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvIncidencia.ReadOnly = true;
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            cargarIncidencias();
            dgvIncidencia.DefaultCellStyle.ForeColor = Color.Black;
            dgvIncidencia.DefaultCellStyle.BackColor = Color.Red;
        }

        private void cargarIncidencias()
        {
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (con != null)
            {
                string consulta = "SELECT incidencia.`Id_Trabajador`, tipo_incidencia.`Incidencia`, incidencia.`Fecha`, incidencia.Hora FROM incidencia INNER JOIN tipo_incidencia ON incidencia.`id_tipoIncidencia` = tipo_incidencia.`id_tipoIncidencia`;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvIncidencia.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Error al conectar");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnJustificado_Click(object sender, EventArgs e)
        {
            dgvIncidencia.SelectedRows[0].DefaultCellStyle.BackColor = Color.Lime;
        }

        private void dgvIncidencia_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dgvIncidencia_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvIncidencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvIncidencia.Rows[e.RowIndex];
                txtNumTrabajador.Text = fila.Cells[0].Value?.ToString();
            }

        }
    }
}
