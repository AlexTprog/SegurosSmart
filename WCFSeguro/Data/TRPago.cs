//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFSeguro.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRPago
    {
        public int Id { get; set; }
        public int Afiliacion { get; set; }
        public int Cliente { get; set; }
        public int Seguro { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Cuota { get; set; }
        public Nullable<int> Monto { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual TRAfiliacion TRAfiliacion { get; set; }
    }
}
