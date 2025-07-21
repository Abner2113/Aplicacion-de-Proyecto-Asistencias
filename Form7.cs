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
    public partial class Form7 : Form
    {
        private ClsConexion conexion;

        public Form7()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            cargarAsistencias();
        }

        private void cargarAsistencias()
        {
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (con != null)
            {
                string consulta = "SELECT asistencia.`Id_Trabajador`, asistencia.`H_Entrada`, asistencia.`H_Salida`, asistencia.`fecha` FROM asistencia;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvAsistencias.DataSource = dataTable;
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
    }
}
