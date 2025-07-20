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
    public partial class Form10 : Form
    {
        private ClsConexion conexion;
        public Form10()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvUsuarios.ReadOnly = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumTrabajador.Text))
            {
                MessageBox.Show("Por favor ingrese una clave valida");
                txtNumTrabajador.Focus();
                return;
            }

            Form11 Edirar = new Form11(txtNumTrabajador.Text);
            string Id_Trabajador = txtNumTrabajador.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand verif = new MySqlCommand(Verificar, con);
            verif.Parameters.AddWithValue("@Id_Trabajador", Id_Trabajador);
            int count = Convert.ToInt32(verif.ExecuteScalar());

            if (count > 0)
            {
                Edirar.Show();
            }
            else
            {
                MessageBox.Show("El ID del trabajador no existe");
                txtNumTrabajador.Focus();
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (con != null)
            {
                string consulta = "SELECT Empleado.`Id_Trabajador`, Empleado.`Nombre`, Empleado.`Apellido_Paterno`, Empleado.`Apellido_Materno`, puestos.`Puesto`, Empleado.`Contrasenia`, carreras.`Carrera` FROM Empleado INNER JOIN puestos ON Empleado.`Id_puesto` = puestos.`Id_Puesto` INNER JOIN Carreras ON Empleado.`Id_Carrera` = Carreras.`Id_Carrera`;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvUsuarios.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Error al conectar");
            }
        }

        private void dgvUsuarios_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvUsuarios.Enabled = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtNumTrabajador.Text))
            {
                string query = "SELECT d.dia, h.H_Entrada, h.H_Salida, h.Periodo FROM Horario h INNER JOIN Dias d ON h.Id_Dia = d.Id_dia WHERE h.Id_Trabajador = @Id_Trabajador;";
                MySqlCommand cmd = new MySqlCommand(query, conexion.getConnection());
                cmd.Parameters.AddWithValue("@Id_Trabajador", txtNumTrabajador.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
            else
            {
                MessageBox.Show("ingrese el ID del trabajador");
                txtNumTrabajador.Focus();
            }

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void txtNumTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNumTrabajador_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtNumTrabajador.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string consulta = "SELECT Empleado.`Id_Trabajador`, Empleado.`Nombre`, Empleado.`Apellido_Paterno`, Empleado.`Apellido_Materno`, puestos.`Puesto`, carreras.`Carrera` FROM Empleado INNER JOIN Puestos ON Empleado.`Id_puesto` = Puestos.`Id_Puesto` INNER JOIN Carreras ON Empleado.`Id_Carrera` = Carreras.`Id_Carrera` WHERE Empleado.`Id_Trabajador` LIKE @Busqueda;";

            MySqlCommand command = new MySqlCommand(consulta, con);
            command.Parameters.AddWithValue("@Busqueda", "%" + filtro + "%");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgvUsuarios.DataSource = dataTable;
            con.Close();
        }
    }
}
