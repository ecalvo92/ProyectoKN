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
                using (var context = new KN_DBEntities())
                {
                    var info = context.ConsultarOfertas().Where(x => x.Disponible == true).ToList();
                    return View(info);
                }
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

    }
}