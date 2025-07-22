using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplicacion_de_Proyecto_Asistencias;
using MySql.Data.MySqlClient;

namespace Aplicacion_de_Proyecto_Asistencias
{
    public partial class Form3 : Form
    {
        private ClsConexion conexion;
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics linea = e.Graphics;
            linea.DrawLine(Pens.Black, 190, 190, 410, 190);
            linea.DrawLine(Pens.Black, 190, 470, 410, 470);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1_Inicio salir = (Form1_Inicio)Application.OpenForms["Form1_Inicio"];
            salir.BringToFront();
            salir.Activate();
            txtClave.Clear();
            txtContraseña.Clear();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClave.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Por favor ingrese su clave y contraseña");
                txtClave.Focus();
                return;
            }

            Form6 Admin = new Form6();

            string clave = txtClave.Text;
            string contraseña = txtContraseña.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador AND Contrasenia = @Contrasenia AND Id_Puesto = 3;";
            MySqlCommand verif = new MySqlCommand(Verificar, con);
            verif.Parameters.AddWithValue("@Id_Trabajador", clave);
            verif.Parameters.AddWithValue("@Contrasenia", contraseña);
            int count = Convert.ToInt32(verif.ExecuteScalar());
            con.Close();

            if (count == 0)
            {
                string VerificarClave = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
                MySqlCommand verifClave = new MySqlCommand(VerificarClave, con);
                verifClave.Parameters.AddWithValue("@Id_Trabajador", clave);

                con.Open();
                int countClave = Convert.ToInt32(verifClave.ExecuteScalar());
                con.Close();

                if (countClave == 0)
                {
                    MessageBox.Show("La clave es incorrecta");
                    txtClave.Focus();
                }
                else
                {
                    string VerificarPuesto = "SELECT Id_Puesto FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
                    MySqlCommand verifPuesto = new MySqlCommand(VerificarPuesto, con);
                    verifPuesto.Parameters.AddWithValue("@Id_Trabajador", clave);

                    con.Open();
                    int puesto = Convert.ToInt32(verifPuesto.ExecuteScalar());
                    con.Close();

                    if (puesto != 3)
                    {
                        MessageBox.Show("No tiene permisos de administrador");
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta");
                    }
                    txtContraseña.Focus();
                }
            }
            else
            {
                Admin.Show();
                txtClave.Clear();
                txtContraseña.Clear();
            }
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
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
