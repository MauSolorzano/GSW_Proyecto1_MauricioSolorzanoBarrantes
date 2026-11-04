using GSW_Proyecto1_MauricioSolorzanoBarrantes.Data;
using GSW_Proyecto1_MauricioSolorzanoBarrantes.Data;
using GSW_Proyecto1_MauricioSolorzanoBarrantes.Filters;
using GSW_Proyecto1_MauricioSolorzanoBarrantes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Controllers
{
    public class AccountController : Controller
    {
        //Obtenemos la vista de login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Método para iniciar sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Recibimos el correo y la contraseña del usuario para verificar si son correctos
        public ActionResult Login(string correo, string contrasena)
        {
            // Validamos que el correo y la contraseña no estén vacíos
            if (string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                // Si alguno de los campos está vacío, mostramos un mensaje de error y retornamos la vista de login
                ViewBag.Mensaje = "Ingrese el correo y la contraseña.";
                return View();
            }
            // Verificamos si el correo y la contraseña son correctos
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT IdUsuario, Nombre, Correo, Contrasena
                                    FROM Usuarios
                                    WHERE Correo = @Correo";

                // Creamos un comando SQL para ejecutar la consulta
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Correo", correo);
                // Abrimos la conexión a la base de datos
                conexion.Open();
                // Ejecutamos la consulta y obtenemos el resultado
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    // Si encontramos un usuario con el correo proporcionado, verificamos la contraseña
                    if (lector.Read())
                    {
                        // Obtenemos el hash de la contraseña almacenada en la base de datos
                        string hash = lector["Contrasena"].ToString();
                        // Verificamos si la contraseña proporcionada coincide con el hash almacenado
                        if (Seguridad.VerificarContrasena(contrasena, hash))
                        {
                            // Si la contraseña es correcta, almacenamos la información del usuario en la sesión
                            Session["IdUsuario"] = lector["IdUsuario"];
                            Session["NombreUsuario"] = lector["Nombre"].ToString();
                            Session["Autenticado"] = true;
                            // Redirigimos al usuario a la página principal después de iniciar sesión correctamente
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            // Si el correo o la contraseña son incorrectos, mostramos un mensaje de error y retornamos la vista de login
            ViewBag.Mensaje = "Correo o contraseña incorrectos.";
            return View();
        }

        [Authorization]
        //Método para cerrar sesión y limpiar la sesión del usuario
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}