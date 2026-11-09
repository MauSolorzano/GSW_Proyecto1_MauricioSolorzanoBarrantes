using GSW_Proyecto1_MauricioSolorzanoBarrantes.Data;
using GSW_Proyecto1_MauricioSolorzanoBarrantes.Filters;
using GSW_Proyecto1_MauricioSolorzanoBarrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private PurchaseRequestData solicitudData = new PurchaseRequestData();
        private ProductoData productoData = new ProductoData();

        private Producto ObtenerProductoDisponible(int id)
        {
            Producto producto = productoData.ObtenerPorId(id);

            if (producto == null || !producto.Disponible)
                return null;

            return producto;
        }

        private void PrepararVista(Producto producto)
        {
            ViewBag.NombreProducto = producto.Nombre;
            ViewBag.Precio = producto.Precio;
        }

        // Mostrar formulario de solicitud
        [HttpGet]
        public ActionResult Crear(int id)
        {
            Producto producto = ObtenerProductoDisponible(id);

            if (producto == null)
                return RedirectToAction("Index", "Catalog");

            PrepararVista(producto);

            PurchaseRequest solicitud = new PurchaseRequest();
            solicitud.IdProducto = id;

            return View(solicitud);
        }

        // Registrar solicitud
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(PurchaseRequest solicitud)
        {
            Producto producto = ObtenerProductoDisponible(solicitud.IdProducto);

            if (producto == null)
                return RedirectToAction("Index", "Catalog");

            PrepararVista(producto);

            if (ModelState.IsValid)
            {
                bool resultado = solicitudData.Registrar(solicitud);

                if (resultado)
                    return RedirectToAction("Confirmacion");

                ModelState.AddModelError(
                    "",
                    "No fue posible registrar la solicitud. El producto podría ya no estar disponible."
                );
            }

            return View(solicitud);
        }

        // Mostrar confirmación
        [HttpGet]
        public ActionResult Confirmacion()
        {
            return View();
        }

        // Consultar solicitudes recibidas
        [Authorization]
        public ActionResult Index()
        {
            var solicitudes = solicitudData.ObtenerTodas();

            return View(solicitudes);
        }

        // Actualizar estado de una solicitud
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorization]
        public ActionResult ActualizarEstado(int idSolicitud, string estado)
        {
            bool resultado = solicitudData.ActualizarEstado(idSolicitud, estado);

            if (resultado)
                TempData["Mensaje"] = "El estado se actualizó correctamente.";
            else
                TempData["Mensaje"] = "No fue posible actualizar el estado.";

            return RedirectToAction("Index");
        }
    }
}