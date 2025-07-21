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
    public partial class Form4 : Form
    {
        private ClsConexion conexion;
        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            cmbCarrera.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCarrera.SelectedIndex = -1;
            cmbCarrera.Items.Add("TIC");
            cmbCarrera.Items.Add("Gastronomia");
            cmbCarrera.Items.Add("Mecanica");
            cmbCarrera.Items.Add("Contaduria");

            cmbPuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPuesto.Items.Add("Docente");
            cmbPuesto.Items.Add("Secretario");
            cmbPuesto.Items.Add("Administrador");
            cmbPuesto.SelectedIndex = -1;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Form8 Usuarios = new Form8();
            if (!string.IsNullOrEmpty(txtNombres.Text) ||
                !string.IsNullOrEmpty(txtApellidoP.Text) ||
                !string.IsNullOrEmpty(txtApellidoM.Text) ||
                !string.IsNullOrEmpty(txtIdTrabajador.Text) ||
                !string.IsNullOrEmpty(txtContraseña.Text) ||
                cmbCarrera.SelectedIndex >= 0 ||
                cmbPuesto.SelectedIndex >= 0)    
            {
                DialogResult respuesta = MessageBox.Show("¿Deseas continuar?\nTienes algo escrito\nSi continuas se perdera el progreso", "Advertencia", MessageBoxButtons.OKCancel);
                if (respuesta == DialogResult.OK)
                {
                    Usuarios.Show();
                    this.Hide();
                }
            }
            else
            {
                Usuarios.Show();
                this.Hide();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidoP.Text) || string.IsNullOrEmpty(txtApellidoM.Text) || string.IsNullOrEmpty(txtIdTrabajador.Text) || cmbCarrera.SelectedIndex == -1 || cmbPuesto.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, llene todos los campos obligatorios");
                return;
            }
            if (cmbPuesto.SelectedIndex == 2 && string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Por favor, ingrese una contraseña");
                txtContraseña.Focus();
                return;
            }

            string nombre = txtNombres.Text;
            string ApellidoP = txtApellidoP.Text;
            string ApellidoM = txtApellidoM.Text;
            string idTrabajador = txtIdTrabajador.Text;
            string Contraseña = txtContraseña.Text;
            int carrera = Convert.ToInt32(cmbCarrera.SelectedIndex + 1);
            int puesto = Convert.ToInt32(cmbPuesto.SelectedIndex + 1);

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand veri = new MySqlCommand(Verificar, con);
            veri.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            int count = Convert.ToInt32(veri.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("El ID del trabajador ya existe");
                txtIdTrabajador.Focus();
                return;
            }

            string consulta = "INSERT INTO Empleado (Id_Trabajador, Nombre, Apellido_Paterno, Apellido_Materno, Id_puesto, Contrasenia, Id_Carrera) VALUES (@Id_Trabajador, @Nombre, @Apellido_Paterno, @Apellido_Materno, @Id_Puesto, @Contrasenia, @Carrera);";
            MySqlCommand adapter = new MySqlCommand(consulta, con);
            adapter.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            adapter.Parameters.AddWithValue("@Nombre", nombre);
            adapter.Parameters.AddWithValue("@Apellido_Paterno", ApellidoP);
            adapter.Parameters.AddWithValue("@Apellido_Materno", ApellidoM);
            adapter.Parameters.AddWithValue("@Id_Puesto", puesto);
            adapter.Parameters.AddWithValue("@Contrasenia", Contraseña);
            adapter.Parameters.AddWithValue("@Carrera", carrera);
            adapter.ExecuteNonQuery();

            MessageBox.Show("Registro extitoso...");

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    txtNombres.Clear();
                    txtApellidoP.Clear();
                    txtApellidoM.Clear();
                    txtIdTrabajador.Clear();
                    txtContraseña.Clear();
                    cmbCarrera.SelectedIndex = -1;
                    cmbPuesto.SelectedIndex = -1;
                }
            }
            string isertar1 = "INSERT INTO Horario (H_Entrada, H_Salida, Id_Trabajador, Id_dia, periodo) VALUE ('0:00 AM', '0:00 PM', @Id_Trabajador, 1, '2025-2');";
            MySqlCommand insertar1 = new MySqlCommand(isertar1, con);
            insertar1.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            insertar1.ExecuteNonQuery();
            string isertar2 = "INSERT INTO Horario (H_Entrada, H_Salida, Id_Trabajador, Id_dia, periodo) VALUE ('0:00 AM', '0:00 PM', @Id_Trabajador, 2, '2025-2');";
            MySqlCommand insertar2 = new MySqlCommand(isertar2, con);
            insertar2.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            insertar2.ExecuteNonQuery();
            string isertar3 = "INSERT INTO Horario (H_Entrada, H_Salida, Id_Trabajador, Id_dia, periodo) VALUE ('0:00 AM', '0:00 PM', @Id_Trabajador, 3, '2025-2');";
            MySqlCommand insertar3 = new MySqlCommand(isertar3, con);
            insertar3.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            insertar3.ExecuteNonQuery();
            string isertar4 = "INSERT INTO Horario (H_Entrada, H_Salida, Id_Trabajador, Id_dia, periodo) VALUE ('0:00 AM', '0:00 PM', @Id_Trabajador, 4, '2025-2');";
            MySqlCommand insertar4 = new MySqlCommand(isertar4, con);
            insertar4.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            insertar4.ExecuteNonQuery();
            string isertar5 = "INSERT INTO Horario (H_Entrada, H_Salida, Id_Trabajador, Id_dia, periodo) VALUE ('0:00 AM', '0:00 PM', @Id_Trabajador, 5, '2025-2');";
            MySqlCommand insertar5 = new MySqlCommand(isertar5, con);
            insertar5.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
            insertar5.ExecuteNonQuery();
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNombres.CharacterCasing = CharacterCasing.Upper;
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellidoP_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtApellidoP.CharacterCasing = CharacterCasing.Upper;
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellidoM_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtApellidoM.CharacterCasing = CharacterCasing.Upper;
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtIdTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}