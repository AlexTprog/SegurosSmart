using SegurosSmart.Models.Consts;
using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public class Seguro: ModelBase
    {
        public int Id { get; set; }
        public Compania Compania { get; set; }
        public string Numero { get; set; }
        public TipoSeguro Tipo { get; set; }
        public string Descripcion { get; set;}
        public int FactorImpuesto { get; set; }
        public int PorcentajeComision { get; set; }
        public int Prima { get; set; }
        public Moneda Moneda { get; set; }
        public int EdadMaxima { get; set; }
        public DateTime FechaVigencia { get; set; }
        public int ImporteMensual { get; set; }
        public int Cobertura { get; set; }
        public EstadoSeguro Estado { get; set; }        
    }
}