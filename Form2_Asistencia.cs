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
        private System.Windows.Forms.Timer timeAsistencias;
        public Form2_Asistencia()
        {
            InitializeComponent();
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);
            time.Start();

            timerAsistencias = new System.Windows.Forms.Timer();
            timerAsistencias.Interval = 3600000;
            timerAsistencias.Tick += new EventHandler(VerificarAusenciasTick);
            timerAsistencias.Start();

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
        private void VerificarAusenciasTick(object sender, EventArgs e)
        {
            VerificarAusencias();
        }
        private void VerificarAusencias()
        {
            DateTime horaActual = DateTime.Now;
            string query = "SELECT Id_Trabajador, H_Salida FROM Horario WHERE Id_dia = @Dia AND H_Salida <= @HoraActual";
            conexion = new ClsConexion();
            MySqlConnection con = conexion.getConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Dia", (int)horaActual.DayOfWeek + 1);
            cmd.Parameters.AddWithValue("@HoraActual", horaActual.TimeOfDay);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string idTrabajador = reader["Id_Trabajador"].ToString();
                TimeSpan horaSalida = (TimeSpan)reader["H_Salida"];

                string queryAsistencia = "SELECT COUNT(*) FROM Asistencia WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha AND H_Salida IS NOT NULL";
                con.Close();
                con.Open();
                MySqlCommand cmdAsistencia = new MySqlCommand(queryAsistencia, conexion.getConnection());
                cmdAsistencia.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
                cmdAsistencia.Parameters.AddWithValue("@Fecha", horaActual.ToString("yyyy-MM-dd"));
                int asistenciaCount = Convert.ToInt32(cmdAsistencia.ExecuteScalar());

                if (asistenciaCount == 0)
                {
                    string InsertarIn = "INSERT INTO Incidencia (id_tipoIncidencia, Id_Trabajador, Fecha) VALUES (@id_tipoIncidencia, @Id_Trabajador, @Fecha)";
                    MySqlCommand cmdIncidencia = new MySqlCommand(InsertarIn, conexion.getConnection());
                    cmdIncidencia.Parameters.AddWithValue("@Id_Trabajador", idTrabajador);
                    cmdIncidencia.Parameters.AddWithValue("@id_tipoIncidencia", 2); // 2 para falta
                    cmdIncidencia.Parameters.AddWithValue("@Fecha", horaActual.ToString("yyyy-MM-dd"));
                    cmdIncidencia.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form1_Inicio salir = (Form1_Inicio)Application.OpenForms["Form1_Inicio"];
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

            string Verificar = "SELECT COUNT(*) FROM Empleado WHERE Id_Trabajador = @Id_Trabajador;";
            MySqlCommand veri = new MySqlCommand(Verificar, con);
            veri.Parameters.AddWithValue("@Id_Trabajador", clave);
            int count = Convert.ToInt32(veri.ExecuteScalar());

            if (count > 0)
            {
                int DiaActual = (int)DateTime.Now.DayOfWeek;
                if (DiaActual == 0 || DiaActual == 6)
                {
                    MessageBox.Show("No se puede registrar asistencia en fin de semana.");
                    txtClave.Focus();
                    txtClave.Clear();
                    return;
                }
                else
                {
                    string dia = DateTime.Now.ToString("dddd").ToLower();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM Asistencia WHERE Id_Trabajador = @Id_Trabajador AND Fecha = @Fecha;", con);
                    cmd1.Parameters.AddWithValue("@Id_Trabajador", clave);
                    cmd1.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                    int asistenciaCount = Convert.ToInt32(cmd1.ExecuteScalar());
                    if (asistenciaCount > 0)
                    {
                        if (asistenciaCount > 0)
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
                                txtClave.Focus();
                                txtClave.Clear();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Ya se ha registrado su salida para hoy.");
                                txtClave.Focus();
                                txtClave.Clear();
                                return;
                            }
                        }
                        MessageBox.Show("Ya se ha registrado su asistencia para hoy.");
                        txtClave.Focus();
                        txtClave.Clear();
                        return;
                    }
                    else
                    {
                        DiaActual++;

                        string querty = "SELECT H_Entrada FROM Horario WHERE Id_Trabajador = @Id_Trabajador AND Id_dia = @Dia;";
                        MySqlCommand cmd2 = new MySqlCommand(querty, con);
                        cmd2.Parameters.AddWithValue("@Id_Trabajador", clave);
                        cmd2.Parameters.AddWithValue("@Dia", DiaActual);
                        TimeSpan horaEntrada = (TimeSpan)cmd2.ExecuteScalar();

                        DateTime horaActual = DateTime.Now;
                        TimeSpan HoraActualTs = horaActual.TimeOfDay;

                        if (HoraActualTs > horaEntrada.Add(new TimeSpan(0, 10, 0)))
                        {
                            string InsertarIn = "INSERT INTO Incidencia (id_tipoIncidencia, Id_Trabajador, Fecha, Hora) VALUES (@id_tipoIncidencia, @Id_Trabajador, @Fecha, @Hora);";
                            MySqlCommand cmd = new MySqlCommand(InsertarIn, con);
                            cmd.Parameters.AddWithValue("@Id_Trabajador", clave);
                            cmd.Parameters.AddWithValue("@id_tipoIncidencia", 1);
                            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Hora", DateTime.Now.ToString("HH:mm:ss"));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Se ha registrado una incidencia por retraso en su asistencia.");
                            txtClave.Focus();
                            txtClave.Clear();
                            return;
                        }
                        else
                        {
                            string InsertarA = "INSERT INTO Asistencia (Id_Trabajador, H_Entrada, Fecha) VALUES (@Id_Trabajador, @H_Entrada, @Fecha);";
                            MySqlCommand cmd = new MySqlCommand(InsertarA, con);
                            cmd.Parameters.AddWithValue("@Id_Trabajador", clave);
                            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@H_Entrada", DateTime.Now.ToString("HH:mm:ss"));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("se registro su asistencia");
                            txtClave.Focus();
                            txtClave.Clear();
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El usuario no está registrado");
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
