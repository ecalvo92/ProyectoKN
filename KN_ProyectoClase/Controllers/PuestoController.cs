using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PuestoController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarPuestos()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarPuestos().ToList();
                return View(info);
            }
        }

        [HttpGet]
        public ActionResult AgregarPuesto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarPuesto(PuestoModel model)
        {
            using (var context = new KN_DBEntities())
            {
                Puesto tabla = new Puesto();
                tabla.Nombre = model.Nombre;
                tabla.Descripcion = model.Descripcion;

                context.Puesto.Add(tabla);
                var result = context.SaveChanges();

                if (result > 0)
                    return RedirectToAction("ConsultarPuestos", "Puesto");
                else
                {
                    ViewBag.Mensaje = "La información no se ha podido registrar correctamente";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult ActualizarPuesto(long q)
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.Puesto.Where(x => x.Id == q).FirstOrDefault();
                return View(info);
            }
        }

        [HttpPost]
        public ActionResult ActualizarPuesto(Puesto model)
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.Puesto.Where(x => x.Id == model.Id).FirstOrDefault();

                info.Nombre = model.Nombre;
                info.Descripcion = model.Descripcion;
                var result = context.SaveChanges();

                if (result > 0)
                    return RedirectToAction("ConsultarPuestos", "Puesto");
                else
                {
                    ViewBag.Mensaje = "La información no se ha podido actualizar correctamente";
                    return View();
                }

            }
        }

    }
}