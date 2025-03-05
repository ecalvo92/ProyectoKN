using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KN_ProyectoClase.Controllers
{
    public class OfertaController : Controller
    {
        //Consulta las ofertas para darles mantenimiento
        [HttpGet]
        public ActionResult ConsultarOfertas()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().ToList();
                return View(info);
            }
        }

        //Consulta las ofertas para poder aplicar
        [HttpGet]
        public ActionResult ConsultarOfertasDisponibles()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().Where(x => x.Disponible == true).ToList();
                return View(info);
            }
        }

        [HttpGet]
        public ActionResult AgregarOferta()
        {
            CargarComboPuestos();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarOferta(OfertaModel model)
        {
            using (var context = new KN_DBEntities())
            {
                Oferta tabla = new Oferta();
                tabla.IdPuesto = model.IdPuesto;
                tabla.Cantidad = model.Cantidad;
                tabla.Salario = model.Salario;
                tabla.Horario = model.Horario;
                tabla.Disponible = true;

                context.Oferta.Add(tabla);
                var result = context.SaveChanges();

                if (result > 0)
                    return RedirectToAction("ConsultarOfertas", "Oferta");
                else
                {
                    ViewBag.Mensaje = "La información no se ha podido registrar correctamente";
                    return View();
                }
            }
        }


        [HttpGet]
        public ActionResult ActualizarOferta(long q)
        {
            CargarComboPuestos();
            using (var context = new KN_DBEntities())
            {
                var info = context.Oferta.Where(x => x.Id == q).FirstOrDefault();
                return View(info);
            }
        }

        [HttpPost]
        public ActionResult ActualizarOferta(Oferta model)
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.Oferta.Where(x => x.Id == model.Id).FirstOrDefault();

                info.IdPuesto = model.IdPuesto;
                info.Cantidad = model.Cantidad;
                info.Salario = model.Salario;
                info.Horario = model.Horario;
                info.Disponible = model.Disponible;
                var result = context.SaveChanges();

                if (result > 0)
                    return RedirectToAction("ConsultarOfertas", "Oferta");
                else
                {
                    ViewBag.Mensaje = "La información no se ha podido actualizar correctamente";
                    return View();
                }

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