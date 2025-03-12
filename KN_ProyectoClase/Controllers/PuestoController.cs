using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PuestoController : Controller
    {
        RegistroErrores error = new RegistroErrores();

        [HttpGet]
        public ActionResult ConsultarPuestos()
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.ConsultarPuestos().ToList();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ConsultarPuestos");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarPuesto()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AgregarPuesto");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarPuesto(PuestoModel model)
        {
            try
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
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AgregarPuesto");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ActualizarPuesto(long q)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.Puesto.Where(x => x.Id == q).FirstOrDefault();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarPuesto");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarPuesto(Puesto model)
        {
            try
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
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post ActualizarPuesto");
                return View("Error");
            }
        }

    }
}