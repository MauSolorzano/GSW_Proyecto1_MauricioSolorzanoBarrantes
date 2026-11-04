using GSW_Proyecto1_MauricioSolorzanoBarrantes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Data
{
    public class ProductoData
    {
        // Obtener todos los productos de la base de datos
        public List<Producto> ObtenerTodos()
        {
            // Crear una lista para almacenar los productos
            List<Producto> productos = new List<Producto>();
            // Establecer la conexión a la base de datos y ejecutar la consulta
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT IdProducto, Nombre, Descripcion,
                                           Precio, Disponible, FechaRegistro
                                    FROM Productos
                                    ORDER BY IdProducto DESC";

                SqlCommand comando = new SqlCommand(consulta, conexion);

                conexion.Open();
                // Ejecutar la consulta y leer los resultados
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        // Crear un objeto Producto y asignar los valores de cada columna
                        Producto producto = new Producto();

                        producto.IdProducto = Convert.ToInt32(lector["IdProducto"]);
                        producto.Nombre = lector["Nombre"].ToString();
                        producto.Descripcion = lector["Descripcion"].ToString();
                        producto.Precio = Convert.ToDecimal(lector["Precio"]);
                        producto.Disponible = Convert.ToBoolean(lector["Disponible"]);
                        producto.FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"]);
                        // Agregar el producto a la lista
                        productos.Add(producto);
                    }
                }
            }

            return productos;
        }
        // Obtener un producto por su ID
        public Producto ObtenerPorId(int id)
        {
            // Crear un objeto Producto para almacenar el resultado
            Producto producto = null;
            // Establecer la conexión a la base de datos y ejecutar la consulta
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT IdProducto, Nombre, Descripcion,
                                           Precio, Disponible, FechaRegistro
                                    FROM Productos
                                    WHERE IdProducto = @IdProducto";
                // Crear un comando SQL con la consulta y el parámetro
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdProducto", id);

                conexion.Open();
                // Ejecutar la consulta y leer el resultado
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        // Crear un objeto Producto y asignar los valores de cada columna
                        producto = new Producto();
                        // Asignar los valores de cada columna a las propiedades del objeto Producto
                        producto.IdProducto = Convert.ToInt32(lector["IdProducto"]);
                        producto.Nombre = lector["Nombre"].ToString();
                        producto.Descripcion = lector["Descripcion"].ToString();
                        producto.Precio = Convert.ToDecimal(lector["Precio"]);
                        producto.Disponible = Convert.ToBoolean(lector["Disponible"]);
                        producto.FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"]);
                    }
                }
            }
            // Devolver el objeto Producto obtenido de la base de datos
            return producto;
        }
        // Registrar un nuevo producto en la base de datos
        public bool Registrar(Producto producto)
        {
            // Establecer la conexión a la base de datos y ejecutar la consulta de inserción
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"INSERT INTO Productos
                            (Nombre, Descripcion, Precio, Disponible)
                            VALUES
                            (@Nombre, @Descripcion, @Precio, @Disponible)";
                // Crear un comando SQL con la consulta y los parámetros
                SqlCommand comando = new SqlCommand(consulta, conexion);
                // Agregar los valores de las propiedades del objeto Producto como parámetros del comando
                comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Disponible", producto.Disponible);

                conexion.Open();
                //Devolver true si la inserción fue exitosa, false en caso contrario
                return comando.ExecuteNonQuery() > 0;
            }
        }
        // Actualizar un producto existente en la base de datos
        public bool Actualizar(Producto producto)
        {
            // Establecer la conexión a la base de datos y ejecutar la consulta de actualización
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"UPDATE Productos
                            SET Nombre = @Nombre,
                                Descripcion = @Descripcion,
                                Precio = @Precio,
                                Disponible = @Disponible
                            WHERE IdProducto = @IdProducto";
                // Crear un comando SQL con la consulta y los parámetros
                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Disponible", producto.Disponible);

                conexion.Open();
                // Devolver true si la actualización fue exitosa, false en caso contrario
                return comando.ExecuteNonQuery() > 0;
            }
        }
    }
}