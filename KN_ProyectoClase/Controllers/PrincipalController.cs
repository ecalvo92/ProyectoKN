using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

    }
}