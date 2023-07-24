using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFSeguro.Data;
using WCFSeguro.Services.Base;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "PagoService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione PagoService.svc o PagoService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class PagoService : ServiceBase, IPagoService
    {

        public void GenerarPagos(TRAfiliacion input)//o id afiliacion
        {

            for (int i = 1; i <= 12; i++)
            {
                cn.TRPagoes.Add(new TRPago
                {
                    Afiliacion = input.Id,
                    Cliente = input.Cliente,
                    Seguro = input.Seguro,
                    Mes = i,
                    Anio = DateTime.Now.Year,
                    Fecha = DateTime.Now.AddMonths(i - 1),
                    Cuota = input.TMSeguro.ImporteMensual,

                    FechaCreacion = DateTime.Now,
                    Estado = "PEN",
                });
            }

        }

      
    }
}
