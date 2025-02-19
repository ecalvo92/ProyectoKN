using KN_ProyectoClase.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class OfertaController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarOfertas()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().ToList();
                return View(info);
            }
        }

        [HttpGet]
        public ActionResult ConsultarOfertasDisponibles()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().ToList();
                return View(info);
            }
        }

        [HttpGet]
        public ActionResult AgregarOferta()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarPuestos().ToList();

                ViewBag.PuestoCombo = info.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
            }

            return View();
        }        

    }
}