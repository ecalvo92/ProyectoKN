using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        RegistroErrores error = new RegistroErrores();

        #region RegistrarCuenta

        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RegistrarCuenta");
                return View("Error");                
            }           
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioModel model)
        {
            try
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
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RegistrarCuenta");
                return View("Error");
            }
        }

        #endregion

        #region IniciarSesion

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get IniciarSesion");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            try
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
                        Session["IdUsuario"] = info.Id;
                        Session["NombreUsuario"] = info.NombreUsuario;
                        Session["NombrePerfilUsuario"] = info.NombrePerfil;
                        Session["IdPerfilUsuario"] = info.IdPerfil;
                        return RedirectToAction("Inicio", "Principal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Su información no se ha podido validar correctamente";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post IniciarSesion");
                return View("Error");
            }
        }

        #endregion

        [HttpGet]
        public ActionResult Inicio()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get Inicio");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Inicio", "Principal");
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get CerrarSesion");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RecuperarContrasenna");
                return View("Error");
            }
        }

        //PENDIENTE EL POST DE RECUPERAR CONTRASEÑA

    }
}