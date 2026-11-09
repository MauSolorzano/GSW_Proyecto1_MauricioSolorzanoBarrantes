using GSW_Proyecto1_MauricioSolorzanoBarrantes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Data
{
    public class PurchaseRequestData
    {
        public bool Registrar(PurchaseRequest solicitud)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    INSERT INTO SolicitudesCompra
                    (IdProducto, NombreSolicitante, Correo, Telefono)
                    SELECT @IdProducto, @NombreSolicitante, @Correo, @Telefono
                    FROM Productos
                    WHERE IdProducto = @IdProducto
                    AND Disponible = 1";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", solicitud.IdProducto);
                    comando.Parameters.AddWithValue("@NombreSolicitante", solicitud.NombreSolicitante);
                    comando.Parameters.AddWithValue("@Correo", solicitud.Correo);
                    comando.Parameters.AddWithValue("@Telefono", solicitud.Telefono);

                    conexion.Open();

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Consultar todas las solicitudes recibidas
        public List<PurchaseRequest> ObtenerTodas()
        {
            List<PurchaseRequest> solicitudes = new List<PurchaseRequest>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    SELECT S.IdSolicitud, S.IdProducto,
                           S.NombreSolicitante, S.Correo,
                           S.Telefono, S.FechaSolicitud,
                           S.Estado, P.Nombre AS NombreProducto
                    FROM SolicitudesCompra S
                    INNER JOIN Productos P
                    ON S.IdProducto = P.IdProducto
                    ORDER BY S.FechaSolicitud DESC";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            PurchaseRequest solicitud = new PurchaseRequest();

                            solicitud.IdSolicitud = (int)lector["IdSolicitud"];
                            solicitud.IdProducto = (int)lector["IdProducto"];
                            solicitud.NombreSolicitante = lector["NombreSolicitante"].ToString();
                            solicitud.Correo = lector["Correo"].ToString();
                            solicitud.Telefono = lector["Telefono"].ToString();
                            solicitud.FechaSolicitud = (System.DateTime)lector["FechaSolicitud"];
                            solicitud.Estado = lector["Estado"].ToString();
                            solicitud.NombreProducto = lector["NombreProducto"].ToString();

                            solicitudes.Add(solicitud);
                        }
                    }
                }
            }

            return solicitudes;
        }

        // Actualizar el estado de una solicitud
        public bool ActualizarEstado(int idSolicitud, string estado)
        {
            if (estado != "Pendiente" &&
                estado != "En proceso" &&
                estado != "Completada" &&
                estado != "Cancelada")
            {
                return false;
            }

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"
                    UPDATE SolicitudesCompra
                    SET Estado = @Estado
                    WHERE IdSolicitud = @IdSolicitud";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = estado;
                    comando.Parameters.Add("@IdSolicitud", SqlDbType.Int).Value = idSolicitud;

                    conexion.Open();

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}