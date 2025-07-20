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
    public partial class Form2_Asistencia : Form
    {
        private ClsConexion conexion;
        public Form2_Asistencia()
        {
            InitializeComponent();
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);
            time.Start();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void time_Tick(object sender, EventArgs e)
        {
            lblTiempo.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1_Inicio salir = (Form1_Inicio)Application.OpenForms["Form1_Inicio"];
            salir.BringToFront();
            salir.Activate();
        }

        private void btnRegistrarAsistencia_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("por favor, Ingrese una clave");
                        textBox.Focus();
                        return;
                    }
                }
            }
        
            string clave = txtClave.Text;

            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand veri = new MySqlCommand(Verificar, con);
            veri.Parameters.AddWithValue("@Id_Trabajador", clave);
            int count = Convert.ToInt32(veri.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("se registro su asistencia");
                txtClave.Focus();
                txtClave.Clear();
                return;
            }
            else
            {
                MessageBox.Show("El usuario no está registrado");
            }

        }
    }
}
