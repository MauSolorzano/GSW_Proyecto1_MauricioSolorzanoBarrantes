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
    [Authorization]
    public class ProductoController : Controller
    {
        private ProductoData productoData = new ProductoData();

        // Consultar todos los productos
        public ActionResult Index()
        {
            var productos = productoData.ObtenerTodos();

            return View(productos);
        }

        // Mostrar formulario de registro
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        // Registrar un producto en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                bool resultado = productoData.Registrar(producto);

                if (resultado)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(producto);
        }

        // Mostrar formulario de edición de producto
        [HttpGet]
        public ActionResult Editar(int id)
        {
            Producto producto = productoData.ObtenerPorId(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        // Actualizar UN producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                bool resultado = productoData.Actualizar(producto);

                if (resultado)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(producto);
        }
    }
}