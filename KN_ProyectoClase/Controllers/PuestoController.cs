using KN_ProyectoClase.BaseDatos;
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
    }
}