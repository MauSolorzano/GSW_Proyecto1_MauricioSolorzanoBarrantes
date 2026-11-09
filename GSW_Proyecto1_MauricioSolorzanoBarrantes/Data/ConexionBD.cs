using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Data
{
    public class ConexionBD
    {
        public static bool ProbarConexion()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionAsociacion"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                conexion.Open();
                return conexion.State == System.Data.ConnectionState.Open;
            }
        }
        // Método para obtener la conexión a la base de datos
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionAsociacion"]
                .ConnectionString;

            return new SqlConnection(cadena);
        }
    }
}