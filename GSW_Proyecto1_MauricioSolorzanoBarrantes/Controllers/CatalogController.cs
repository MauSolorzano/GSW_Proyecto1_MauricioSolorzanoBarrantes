using GSW_Proyecto1_MauricioSolorzanoBarrantes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Controllers
{
    public class CatalogController : Controller
    {
        private ProductoData productoData = new ProductoData();

        public ActionResult Index()
        {
            var productos = productoData.ObtenerDisponibles();

            return View(productos);
        }
    }
}