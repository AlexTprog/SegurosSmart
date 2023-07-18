using SegurosSmart.Models.Consts;
using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public class Cliente : ModelBase
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string DocumentoIdentidad { get; set; }
        public Genero Genero { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public EstadoCliente Estado { get; set; }
    }
}