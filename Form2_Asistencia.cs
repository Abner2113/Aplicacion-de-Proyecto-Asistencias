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
            txtClave.UseSystemPasswordChar = true;
        }

        private void time_Tick(object sender, EventArgs e)
        {
            lblTiempo.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1_Inicio salir = (Form1_Inicio)Application.OpenForms["Form1_Inicio"];
            txtClave.Clear();
            salir.BringToFront();
            salir.Activate();
        }

        private void btnRegistrarAsistencia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Por favor, ingrese su clave.");
                txtClave.Focus();
                return;
            }
            string clave = txtClave.Text;
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (!ExisteTrabajador(con, clave))
            {
                MessageBox.Show("El ususario no esta registrado");
                return;
            }

            int DiaActual = (int)DateTime.Now.DayOfWeek;
            if (DiaActual == 0 || DiaActual == 6)
            {
                MessageBox.Show("No se puede registrar asistencia en fin de semana.");
                txtClave.Focus();
                txtClave.Clear();
                return;
            }
            if (YaSeRegistroAsistencia(con, clave))
            {
                MessageBox.Show("Ya se ha registrado su asistencia para hoy.");
            }
            else
            {
                string querty = "SELECT H_Entrada FROM Horario WHERE Id_Trabajador = @Id_Trabajador AND Id_dia = @Dia;";
                MySqlCommand cmd2 = new MySqlCommand(querty, con);
                cmd2.Parameters.AddWithValue("@Id_Trabajador", clave);
                cmd2.Parameters.AddWithValue("@Dia", DiaActual);
                TimeSpan horaEntrada = (TimeSpan)cmd2.ExecuteScalar();
                TimeSpan horaActual = DateTime.Now.TimeOfDay;

                if (horaActual < horaEntrada.Subtract(new TimeSpan(0, 30, 0)))
                {
                    MessageBox.Show("No se puede registrar asistencia antes de media hora de la entrada programada.");
                    txtClave.Focus();
                    txtClave.Clear();
                    return;
                }

                if (horaActual <= horaEntrada.Add(new TimeSpan(0, 10, 0)) && horaActual >= horaEntrada.Subtract(new TimeSpan(0, 30, 0)))
                {
                    string InsertarA = "INSERT INTO Asistencia (Id_Trabajador, H_Entrada, Fecha) VALUES (@Id_Trabajador, @H_Entrada, @Fecha);";
                    MySqlCommand cmdAsistencia = new MySqlCommand(InsertarA, con);
                    cmdAsistencia.Parameters.AddWithValue("@Id_Trabajador", clave);
                    cmdAsistencia.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmdAsistencia.Parameters.AddWithValue("@H_Entrada", DateTime.Now.ToString("HH:mm:ss"));
                    cmdAsistencia.ExecuteNonQuery();

                    MessageBox.Show("se registro su asistencia");
                    return;
                }
                else if (horaActual >= horaEntrada.Add(new TimeSpan(0, 10, 0)) && horaActual < horaEntrada.Add(new TimeSpan(0, 20, 0)))
                {
                    string InsertarIn = "INSERT INTO Incidencia (id_tipoIncidencia, Id_Trabajador, Fecha, Hora) VALUES (@id_tipoIncidencia, @Id_Trabajador, @Fecha, @Hora);";
                    MySqlCommand cmdIncidencia = new MySqlCommand(InsertarIn, con);
                    cmdIncidencia.Parameters.AddWithValue("@Id_Trabajador", clave);
                    cmdIncidencia.Parameters.AddWithValue("@id_tipoIncidencia", 1);
                    cmdIncidencia.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmdIncidencia.Parameters.AddWithValue("@Hora", DateTime.Now.ToString("HH:mm:ss"));
                    cmdIncidencia.ExecuteNonQuery();

                    MessageBox.Show("Se ha registrado una incidencia por RETRASO en su asistencia.");
                    return;
                }
                else
                {
                    string InsertarIn = "INSERT INTO Incidencia (id_tipoIncidencia, Id_Trabajador, Fecha, Hora) VALUES (@id_tipoIncidencia, @Id_Trabajador, @Fecha, @Hora);";
                    MySqlCommand cmdIncidencia = new MySqlCommand(InsertarIn, con);
                    cmdIncidencia.Parameters.AddWithValue("@Id_Trabajador", clave);
                    cmdIncidencia.Parameters.AddWithValue("@id_tipoIncidencia", 2);
                    cmdIncidencia.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmdIncidencia.Parameters.AddWithValue("@Hora", DateTime.Now.ToString("HH:mm:ss"));
                    cmdIncidencia.ExecuteNonQuery();
                    MessageBox.Show("Se ha registrado una incidencia de FALTA en su asistencia.");
                    return;
                }
                txtClave.Focus();
                txtClave.Clear();
            }
            con.Close();
        }
        private bool ExisteTrabajador(MySqlConnection con, string clave)
        {
            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand veri = new MySqlCommand(Verificar, con);
            veri.Parameters.AddWithValue("@Id_Trabajador", clave);
            int count = Convert.ToInt32(veri.ExecuteScalar());
            return count > 0;
        }

        private bool YaSeRegistroAsistencia(MySqlConnection con, string clave)
        {
            string consult = "SELECT COUNT(*) FROM Asistencia WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha;";
            MySqlCommand cmd1 = new MySqlCommand(consult, con);
            cmd1.Parameters.AddWithValue("@Id_Trabajador", clave);
            cmd1.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
            int asistenciaCount = Convert.ToInt32(cmd1.ExecuteScalar());
            return asistenciaCount > 0;
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e) { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Por favor, ingrese su clave.");
                txtClave.Focus();
                return;
            }
            string clave = txtClave.Text;
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();

            if (!ExisteTrabajador(con, clave))
            {
                MessageBox.Show("El ususario no esta registrado");
                return;
            }

            if (YaSeRegistroSalida(con, clave))
            {
                MessageBox.Show("Ya se ha registrado su salida para hoy.");
                txtClave.Focus();
                txtClave.Clear();
                return;
            }

            int DiaActual = (int)DateTime.Now.DayOfWeek;
            string querty = "SELECT H_Salida FROM Horario WHERE Id_Trabajador = @Id_Trabajador AND Id_dia = @Dia;";
            MySqlCommand cmd = new MySqlCommand(querty, con);
            cmd.Parameters.AddWithValue("@Id_Trabajador", clave);
            cmd.Parameters.AddWithValue("@Dia", DiaActual);
            TimeSpan horaSalida = (TimeSpan)cmd.ExecuteScalar();
            TimeSpan horaActual = DateTime.Now.TimeOfDay;

            if (horaActual < horaSalida)
            {
                string InsertarIn = "INSERT INTO Incidencia (id_tipoIncidencia, Id_Trabajador, Fecha, Hora) VALUES (@id_tipoIncidencia, @Id_Trabajador, @Fecha, @Hora);";
                MySqlCommand cmdIncidencia = new MySqlCommand(InsertarIn, con);
                cmdIncidencia.Parameters.AddWithValue("@Id_Trabajador", clave);
                cmdIncidencia.Parameters.AddWithValue("@id_tipoIncidencia", 3);
                cmdIncidencia.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                cmdIncidencia.Parameters.AddWithValue("@Hora", DateTime.Now.ToString("HH:mm:ss"));
                cmdIncidencia.ExecuteNonQuery();

                string InsertarA = "UPDATE Asistencia SET H_Salida = @H_Salida WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha;";
                MySqlCommand cmd2 = new MySqlCommand(InsertarA, con);
                cmd2.Parameters.AddWithValue("@Id_Trabajador", clave);
                cmd2.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd2.Parameters.AddWithValue("@H_Salida", DateTime.Now.ToString("HH:mm:ss"));
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Se ha registrado un PERMISO en su asistencia.");
                return;
            }
            RegistrarSalida(con, clave);
        }
        private void RegistrarSalida(MySqlConnection con, string clave)
        {
            string consulta = "SELECT H_Salida FROM Asistencia WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha;";
            MySqlCommand cmd = new MySqlCommand(consulta, con);
            cmd.Parameters.AddWithValue("@Id_Trabajador", clave);
            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
            object horaSalidaRegistrada = cmd.ExecuteScalar();

            if (horaSalidaRegistrada == null || horaSalidaRegistrada.ToString() == "00:00:00")
            {
                string InsertarA = "UPDATE Asistencia SET H_Salida = @H_Salida WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha;";
                MySqlCommand cmd2 = new MySqlCommand(InsertarA, con);
                cmd2.Parameters.AddWithValue("@Id_Trabajador", clave);
                cmd2.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd2.Parameters.AddWithValue("@H_Salida", DateTime.Now.ToString("HH:mm:ss"));
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Se registró su salida");
            }
            else
            {
                MessageBox.Show("Ya se ha registrado su salida para hoy.");
            }
            txtClave.Focus();
            txtClave.Clear();
        }
        private bool YaSeRegistroSalida(MySqlConnection con, string clave)
        {
            string consult = "SELECT H_salida FROM Asistencia WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha AND H_Salida IS NOT NULL;";
            MySqlCommand cmd1 = new MySqlCommand(consult, con);
            cmd1.Parameters.AddWithValue("@Id_Trabajador", clave);
            cmd1.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
            string asistenciaCount = cmd1.ExecuteNonQuery().ToString();
            if (string.IsNullOrEmpty(asistenciaCount) || asistenciaCount == "00:00:00")
            {
                return false;
            }
            return true;
        }
    }
}