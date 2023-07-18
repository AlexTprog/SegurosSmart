using System;
using System.Collections.Generic;

namespace SegurosSmart.Models.Consts
{
    public class AppConsts
    {
        public static List<string> TipoSeguro = new List<string>
        {
            "Automóvil",
            "Hogar",
            "Vida",
            "Salud",
            "Viaje",
            "Responsabilidad Civil",
            "Negocios",
            "Incapacidad",
            "Responsabilidad Profesional",
            "Mascotas"
        };

        public static Dictionary<int, string> Genero = new Dictionary<int, string>()
        {
            {0,"Hombre" },
            {1,"Mujer" }
        };
    }
}
