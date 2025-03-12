using KN_ProyectoClase.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_ProyectoClase.Models
{
    public class RegistroErrores
    {
        public void RegistrarError(string Mensaje, string Origen)
        {
            using (var context = new KN_DBEntities())
            {
                var IdUsuario = HttpContext.Current.Session["IdUsuario"].ToString() ?? "0";

                context.RegistrarError(long.Parse(IdUsuario), Mensaje, Origen);
            }
        }

    }
}