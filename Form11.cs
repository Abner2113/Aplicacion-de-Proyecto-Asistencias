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
        private string ID_Trabajador;

        public Form11(string Id_Trabajador)
        {
            InitializeComponent();
            ID_Trabajador = Id_Trabajador;
            conexion = new ClsConexion();
            CargarHorario(Id_Trabajador);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.Text = "";
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
            cmbPeriodo.Items.Add("2025-2");
            cmbPeriodo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEntrada.Text) || string.IsNullOrEmpty(txtSalida.Text) || cmbDia.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor complete todos los campos antes de guardar.");
                return;
            }

            int dia = cmbDia.SelectedIndex + 1;
            int periodo = cmbPeriodo.SelectedIndex;
            string entrada = txtEntrada.Text;
            string salida = txtSalida.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string actualizar = "UPDATE Horario SET H_Entrada = @Entrada, H_Salida = @Salida WHERE Id_Trabajador = @Id_Trabajador AND id_dia = @id_dia;";
            MySqlCommand cmd = new MySqlCommand(actualizar, con);
            cmd.Parameters.AddWithValue("@Entrada", entrada);
            cmd.Parameters.AddWithValue("@Salida", salida);
            cmd.Parameters.AddWithValue("@Id_Trabajador", ID_Trabajador);
            cmd.Parameters.AddWithValue("@id_dia", dia);
            try
            {
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Horario actualizado correctamente.");
                    CargarHorario(ID_Trabajador);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el horario. Verifique los datos.");
                    cmbDia.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el horario: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
