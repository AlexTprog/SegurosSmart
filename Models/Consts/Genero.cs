using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SegurosSmart.Models
{
    public enum Genero
    {
        HOMBRE,
        MUJER
    }
    public class GeneroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}