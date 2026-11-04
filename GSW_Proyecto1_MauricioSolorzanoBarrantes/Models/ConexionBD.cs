using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Models
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
    }
}