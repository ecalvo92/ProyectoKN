using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PersonasController : Controller
    {
        [HttpGet]
        public ActionResult RegistrarPersona()
        {
            var personasModelo = new PersonasModel();
            personasModelo.Nombre = "EDUARDO";

            return View(personasModelo);
        }

        [HttpPost]
        public ActionResult RegistrarPersona(PersonasModel modelo)
        {
            return View();
        }

    }
}