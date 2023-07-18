using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public class Compania : ModelBase
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string Contacto { get; set; }
        public string Celular { get; set; }
        public string Contrato { get; set; }
        public DateTime FechaRenovacion { get; set; }
        public EstadoCompania Estado { get; set; }
    }
}