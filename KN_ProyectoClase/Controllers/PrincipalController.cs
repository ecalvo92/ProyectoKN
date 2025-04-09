using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

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
                        Session["CorreoUsuario"] = info.Correo;
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

        #region RecuperarContrasenna

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

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioModel model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.Usuario.Where(x => x.Correo == model.Correo
                                                       && x.Estado == true).FirstOrDefault();

                    if (info != null)
                    {
                        var codigoTemporal = CrearCodigo();

                        info.Contrasenna = codigoTemporal;
                        context.SaveChanges();

                        string mensaje = $"Hola {info.Nombre}, por favor utilice el siguiente código para ingresar al sistema: {codigoTemporal}";
                        var notificacion = util.EnviarCorreo(info.Correo, mensaje, "Acceso al sistema KN");
                    
                        if(notificacion)
                            return RedirectToAction("IniciarSesion", "Principal");
                    }

                    ViewBag.Mensaje = "Su acceso no se ha podido reestablecer correctamente";
                    return View();
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RecuperarContrasenna");
                return View("Error");
            }
        }

        #endregion

        [HttpGet]
        public ActionResult Inicio()
        {
            try
            {
                var ofertasTop = ConsultarOfertasTop();
                return View(ofertasTop);
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get Inicio");
                return View("Error");
            }
        }

        public List<ConsultarOfertas_Result> ConsultarOfertasTop()
        {
            using (var context = new KN_DBEntities())
            {
                return context.ConsultarOfertas().Where(x => x.Disponible == true && x.Cantidad > 0)
                    .OrderByDescending(x => x.Salario)
                    .Take(8)
                    .ToList();
            }
        }

        [HttpPost]
        public ActionResult AplicarOferta(ConsultarOfertas_Result model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var IdUsuario = long.Parse(Session["IdUsuario"].ToString());

                    var info = context.UsuariosOferta.Where(x => x.IdUsuario == IdUsuario
                                                              && x.IdOferta == model.Id).FirstOrDefault();

                    var ofertasTop = ConsultarOfertasTop();

                    if (info != null)
                    {
                        ViewBag.Mensaje = "Ya se encuentra participando en esta oferta";
                        return View("Inicio", ofertasTop);
                    }

                    UsuariosOferta tabla = new UsuariosOferta();
                    tabla.Id = 0;
                    tabla.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
                    tabla.IdOferta = model.Id;
                    tabla.Fecha = DateTime.Now;
                    tabla.Estado = 1;

                    context.UsuariosOferta.Add(tabla);
                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        string mensaje = $"Hola { Session["NombreUsuario"].ToString() }, la postulación en la oferta { model.Nombre} ha sido registrada";
                        var notificacion = util.EnviarCorreo(Session["CorreoUsuario"].ToString(), mensaje, "Postulación de Ofertas");

                        return RedirectToAction("ConsultarOfertasAplicadas", "Oferta");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No fue posible aplicar en esta oferta";
                        return View("Inicio", ofertasTop);
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post AplicarOferta");
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

        private string CrearCodigo()
        {
            int length = 5;
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}