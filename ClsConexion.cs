using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_de_Proyecto_Asistencias
{
     internal class ClsConexion
    {
        private string CadenaConexion;

        public ClsConexion()
        {
            CadenaConexion = "Server=127.0.0.1; Database=proyecto; Uid=root; Pwd=; Port=3306;";
        }
        public MySqlConnection getConnection()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(CadenaConexion);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}