using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KN_ProyectoClase.Controllers
{
    public class OfertaController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

        //Consulta las ofertas para darles mantenimiento
        [HttpGet]
        public ActionResult ConsultarOfertas()
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.ConsultarOfertas().ToList();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ConsultarOfertas");
                return View("Error");
            }
        }

        //Consulta las ofertas para poder aplicar
        [HttpGet]
        public ActionResult ConsultarOfertasDisponibles()
        {
            try
            {
                var info = ConsultarOfertasDisp();
                return View(info);
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ConsultarOfertasDisponibles");
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult AgregarOferta()
        {
            try
            {
                CargarComboPuestos();
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get AgregarOferta");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarOferta(OfertaModel model, HttpPostedFileBase ImagenOferta)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    Oferta tabla = new Oferta();
                    tabla.Id = 0;
                    tabla.IdPuesto = model.IdPuesto;
                    tabla.Cantidad = model.Cantidad;
                    tabla.Salario = model.Salario;
                    tabla.Horario = model.Horario;
                    tabla.Disponible = true;

                    context.Oferta.Add(tabla);
                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        //Guardar la imagen
                        string extension = Path.GetExtension(ImagenOferta.FileName);
                        string ruta = AppDomain.CurrentDomain.BaseDirectory + "ImagenesOfertas\\" + tabla.Id + extension;

                        ImagenOferta.SaveAs(ruta);

                        tabla.Imagen = "/ImagenesOfertas/" + tabla.Id + extension;
                        context.SaveChanges();

                        return RedirectToAction("ConsultarOfertas", "Oferta");
                    }
                    else
                    {
                        ViewBag.Mensaje = "La información no se ha podido registrar correctamente";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post AgregarOferta");
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult ActualizarOferta(long q)
        {
            try
            {
                CargarComboPuestos();
                using (var context = new KN_DBEntities())
                {
                    var info = context.Oferta.Where(x => x.Id == q).FirstOrDefault();
                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarOferta");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarOferta(Oferta model, HttpPostedFileBase ImagenOferta)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.Oferta.Where(x => x.Id == model.Id).FirstOrDefault();

                    info.IdPuesto = model.IdPuesto;
                    info.Cantidad = model.Cantidad;
                    info.Salario = model.Salario;
                    info.Horario = model.Horario;
                    info.Disponible = model.Disponible;

                    if (ImagenOferta != null)
                    {
                        //Guardar la imagen
                        string rutaBase = AppDomain.CurrentDomain.BaseDirectory;

                        string extension = Path.GetExtension(ImagenOferta.FileName);
                        string ruta = rutaBase + "ImagenesOfertas\\" + model.Id + extension;

                        if (model.Imagen != null)
                            System.IO.File.Delete(rutaBase + model.Imagen);

                        ImagenOferta.SaveAs(ruta);

                        info.Imagen = "/ImagenesOfertas/" + model.Id + extension;
                    }

                    var result = context.SaveChanges();

                    if (result > 0)
                        return RedirectToAction("ConsultarOfertas", "Oferta");
                    else
                    {
                        CargarComboPuestos();
                        ViewBag.Mensaje = "La información no se ha podido actualizar correctamente";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post ActualizarOferta");
                return View("Error");
            }
        }

        //Consulta las ofertas aplicadas por un usuario
        [HttpGet]
        public ActionResult ConsultarOfertasAplicadas()
        {
            try
            {
                var IdUsuario = long.Parse(Session["IdUsuario"].ToString());

                using (var context = new KN_DBEntities())
                {
                    var info = context.UsuariosOferta
                            .Include(a => a.EstadoAplicacion)
                            .Include(o => o.Oferta.Puesto)
                            .Include(a => a.Oferta)
                            .Where(x => x.IdUsuario == IdUsuario).ToList();

                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ConsultarOfertasDisponibles");
                return View("Error");
            }
        }

        //Aplica en una oferta desde la vista de Ofertas Disponibles
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

                    var ofertasTop = ConsultarOfertasDisp();

                    if (info != null)
                    {
                        ViewBag.Mensaje = "Ya se encuentra participando en esta oferta";
                        return View("ConsultarOfertasDisponibles", ofertasTop);
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
                        string mensaje = $"Hola {Session["NombreUsuario"].ToString()}, la postulación en la oferta {model.Nombre} ha sido registrada";
                        var notificacion = util.EnviarCorreo(Session["CorreoUsuario"].ToString(), mensaje, "Postulación de Ofertas");

                        return RedirectToAction("ConsultarOfertasAplicadas", "Oferta");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No fue posible aplicar en esta oferta";
                        return View("ConsultarOfertasDisponibles", ofertasTop);
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post AplicarOferta");
                return View("Error");
            }
        }

        //Consulta las ofertas para poder cambiar su etapa 
        [HttpGet]
        public ActionResult SeguimientoOfertas()
        {
            try
            {
                CargarComboEtapas();
                var datos = ConsultarSeguimientoOfertas();

                return View(datos);
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get SeguimientoOfertas");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult SeguimientoOfertas(UsuariosOferta model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.UsuariosOferta.Where(x => x.Id == model.Id).FirstOrDefault();
                    info.Estado = model.Estado;

                    if (model.Estado == 4)
                    { 
                        var oferta = context.Oferta.Where(x => x.Id == model.IdOferta).FirstOrDefault();
                        oferta.Cantidad = oferta.Cantidad - 1;
                    }
                    
                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        string mensaje = $"Hola {Session["NombreUsuario"].ToString()}, le informamos que su postulación ha cambiado de estado";
                        var notificacion = util.EnviarCorreo(Session["CorreoUsuario"].ToString(), mensaje, "Postulación de Ofertas");

                        return RedirectToAction("SeguimientoOfertas", "Oferta");
                    }
                    else
                    {
                        CargarComboEtapas();
                        var datos = ConsultarSeguimientoOfertas();

                        ViewBag.Mensaje = "La estapa no se ha podido actualizar correctamente";
                        return View(datos);
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post SeguimientoOfertas");
                return View("Error");
            }
        }

        private List<ConsultarOfertas_Result> ConsultarOfertasDisp()
        {
            using (var context = new KN_DBEntities())
            {
                return context.ConsultarOfertas().Where(x => x.Disponible == true && x.Cantidad > 0).ToList();
            }
        }

        private List<UsuariosOferta> ConsultarSeguimientoOfertas()
        {
            using (var context = new KN_DBEntities())
            {
                return context.UsuariosOferta
                        .Include(a => a.EstadoAplicacion)
                        .Include(a => a.Usuario)
                        .Include(o => o.Oferta.Puesto)
                        .Include(a => a.Oferta).ToList();
            }
        }

        private void CargarComboPuestos()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarPuestos().ToList();

                var puestoCombo = new List<SelectListItem>();

                puestoCombo.Add(new SelectListItem { Value = "", Text = "Seleccione" });
                foreach (var item in info)
                {
                    puestoCombo.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Nombre });
                }

                ViewBag.PuestoCombo = puestoCombo;
            }
        }

        private void CargarComboEtapas()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.EstadoAplicacion.ToList();

                var etapaCombo = new List<SelectListItem>();

                etapaCombo.Add(new SelectListItem { Value = "0", Text = "..." });
                foreach (var item in info)
                {
                    etapaCombo.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.NombreEstado });
                }

                ViewBag.EtapaCombo = etapaCombo.OrderBy(x => x.Value);
            }
        }

    }
}