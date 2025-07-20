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
    public partial class Form8 : Form
    {
        private ClsConexion coneccion;
        public Form8()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvHistorialUsuarios.ReadOnly = true;
        }
        private void CargarDatos()
        {
            coneccion = new ClsConexion();
            MySqlConnection con = coneccion.getConnection();

            if (con != null)
            {
                string consulta = "SELECT Empleado.`Id_Trabajador`, Empleado.`Nombre`, Empleado.`Apellido_Paterno`, Empleado.`Apellido_Materno`, puestos.`Puesto`, Empleado.`Contrasenia`, carreras.`Carrera` FROM Empleado INNER JOIN puestos ON Empleado.`Id_puesto` = puestos.`Id_Puesto` INNER JOIN Carreras ON Empleado.`Id_Carrera` = Carreras.`Id_Carrera`; \r\n";
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvHistorialUsuarios.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("Error al conectar");
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void dgvHistorialUsuarios_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvHistorialUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvHistorialUsuarios.Rows[e.RowIndex];
                txtNumTrabajador.Text = fila.Cells[0].Value?.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNumTrabajador.Text))
            {
                MessageBox.Show("Por favor seleccione un usuario");
                return;
            }
            int id = Convert.ToInt32(txtNumTrabajador.Text);
            DialogResult result = MessageBox.Show("Seguro que quieres eliminarlo?", "Advertencia", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }
            try
            {
                coneccion = new ClsConexion();
                MySqlConnection con = coneccion.getConnection();

                string consulta = "DELETE FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
                MySqlCommand command = new MySqlCommand(consulta, con);
                command.Parameters.AddWithValue("@Id_Trabajador", id);

                int filasAfectadas = command.ExecuteNonQuery();
                con.Close();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("El usuario se elimino correctamente");
                    txtNumTrabajador.Clear();

                    string eliminar = "DELETE FROM Horario WHERE Id_Trabajador = @Id_Trabajador;";
                    MySqlCommand eliminarCommand = new MySqlCommand(eliminar, con);
                    eliminarCommand.Parameters.AddWithValue("@Id_Trabajador", id);
                    eliminarCommand.ExecuteNonQuery();

                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("No se puedo eliminar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
