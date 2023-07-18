using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public class Afiliacion : ModelBase
    {
        public Cliente Cliente { get; set; }
        public Seguro Seguro { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public EstadoAfiliacion Estado { get; set; }
    }
}