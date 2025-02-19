using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_ProyectoClase.Models
{
    public class OfertaModel
    {
        public long IdPuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Salario { get; set; }
        public string Horario { get; set; }
    }
}