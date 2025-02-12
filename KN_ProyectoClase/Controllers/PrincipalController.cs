using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        #region RegistrarCuenta
        
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioModel model)
        {
            //EF utilizando LinQ
            using (var context = new KN_DBEntities())
            {
                //Usuario tabla = new Usuario();
                //tabla.Identificacion = model.Identificacion;
                //tabla.Contrasenna = model.Contrasenna;
                //tabla.Nombre = model.Nombre;
                //tabla.Correo = model.Correo;
                //tabla.Estado = true;
                //tabla.IdPerfil = 2;

                //context.Usuario.Add(tabla);
                //var result = context.SaveChanges();

                //EF utilizando Procedimientos Almacenados
                var result = context.RegistrarCuenta(model.Identificacion, model.Contrasenna, model.Nombre, model.Correo);

                if (result > 0)
                    return RedirectToAction("IniciarSesion", "Principal");
                else
                {
                    ViewBag.Mensaje = "Su información no se ha podido registrar correctamente";
                    return View();
                }
            }            
        }

        #endregion

        #region 

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            using (var context = new KN_DBEntities())
            {
                //EF utilizando LinQ
                //var info = context.Usuario.
                //Where(x => x.Identificacion == model.Identificacion
                //&& x.Contrasenna == model.Contrasenna
                //&& x.Estado == true).FirstOrDefault();

                //EF utilizando Procedimientos Almacenados
                var info = context.IniciarSesion(model.Identificacion, model.Contrasenna).FirstOrDefault();

                if (info != null)
                {
                    Session["NombreUsuario"] = info.Nombre;
                    return RedirectToAction("Inicio", "Principal");
                }
                else
                {
                    ViewBag.Mensaje = "Su información no se ha podido validar correctamente";
                    return View();
                }
            }
        }

        #endregion



        [HttpGet]
        public ActionResult Inicio()
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