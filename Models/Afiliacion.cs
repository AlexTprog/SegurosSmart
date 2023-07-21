using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public class Afiliacion : ModelBase
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Seguro { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public EstadoAfiliacion Estado { get; set; }
    }
}