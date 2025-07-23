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
        private ClsConexion conexion;
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
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (con != null)
            {
                string consulta = "SELECT Empleado.`Id_Trabajador`, Empleado.`Nombre`, Empleado.`Apellido_Paterno`, Empleado.`Apellido_Materno`, Empleado.`Id_Puesto`, puestos.`Puesto`, Empleado.`Contrasenia`, Empleado.`Id_Carrera`, carreras.`Carrera` FROM Empleado INNER JOIN puestos ON Empleado.`Id_puesto` = puestos.`Id_Puesto` INNER JOIN Carreras ON Empleado.`Id_Carrera` = Carreras.`Id_Carrera`;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(consulta, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvHistorialUsuarios.DataSource = dataTable;
                dgvHistorialUsuarios.Columns[4].Visible = false;
                dgvHistorialUsuarios.Columns[7].Visible = false;

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
            else
            {
                conexion = new ClsConexion();
                MySqlConnection con = conexion.getConnection();

                string incidencia = "DELETE FROM Incidencia WHERE Id_Trabajador = @Id;";
                MySqlCommand incidenciacmd = new MySqlCommand(incidencia, con);
                incidenciacmd.Parameters.AddWithValue("id", id);
                incidenciacmd.ExecuteNonQuery();

                string Asistencia = "DELETE FROM Asistencia WHERE Id_Trabajador = @Id;";
                MySqlCommand asistenciacmd = new MySqlCommand(Asistencia, con);
                asistenciacmd.Parameters.AddWithValue("id", id);
                asistenciacmd.ExecuteNonQuery();

                string eliminar = "DELETE FROM Horario WHERE Id_Trabajador = @Id;";
                MySqlCommand eliminarCommand = new MySqlCommand(eliminar, con);
                eliminarCommand.Parameters.AddWithValue("@Id", id);
                eliminarCommand.ExecuteNonQuery();


                string consulta = "DELETE FROM Empleado WHERE Id_Trabajador = @Id;";
                MySqlCommand command = new MySqlCommand(consulta, con);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();

                txtNumTrabajador.Clear();
                txtNumTrabajador.Focus();
                con.Close();
                CargarDatos();
                
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumTrabajador.Text))
            {
                MessageBox.Show("Por favor seleccione un usuario");
                return;
            }
            string Id_Trabajador = txtNumTrabajador.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand veri = new MySqlCommand(Verificar, con);
            veri.Parameters.AddWithValue("@Id_Trabajador", Id_Trabajador);
            int count = Convert.ToInt32(veri.ExecuteScalar());

            if (count > 0)
            {
                string clave = txtNumTrabajador.Text;
                for (int i = 0; i < dgvHistorialUsuarios.Rows.Count; i++)
                {
                    if (dgvHistorialUsuarios.Rows[i].Cells[0].Value.ToString() == clave)
                    {
                        string Nombre = dgvHistorialUsuarios.Rows[i].Cells[1].Value.ToString();
                        string A_paterno = dgvHistorialUsuarios.Rows[i].Cells[2].Value.ToString();
                        string A_Materno = dgvHistorialUsuarios.Rows[i].Cells[3].Value.ToString();
                        int puesto = Convert.ToInt32(dgvHistorialUsuarios.Rows[i].Cells[4].Value.ToString());
                        int carrera = Convert.ToInt32(dgvHistorialUsuarios.Rows[i].Cells[7].Value.ToString());

                        Form12 Actualizar = new Form12(Nombre, A_paterno, A_Materno, clave, puesto, carrera);
                        Actualizar.Show();
                        this.Hide();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("El trabjador no esta registrado");
                txtNumTrabajador.Focus();
                return;
            }
        }
    }
}
