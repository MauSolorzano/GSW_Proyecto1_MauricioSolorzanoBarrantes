using GSW_Proyecto1_MauricioSolorzanoBarrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProbarConexion()
        {
            try
            {
                bool conectado = ConexionBD.ProbarConexion();

                if (conectado)
                {
                    return Content("Conexión con SQL Server exitosa.");
                }

                return Content("No se pudo establecer la conexión.");
            }
            catch (Exception ex)
            {
                return Content("Error de conexión: " + ex.Message);
            }
        }
    }
}