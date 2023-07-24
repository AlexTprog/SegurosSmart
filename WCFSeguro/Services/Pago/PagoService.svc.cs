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

        public void GenerarPagos(int id)
        {
            var input = cn.TRAfiliacions.FirstOrDefault(p => p.Id == id);

            for (int i = 1; i <= 12; i++)
            {
                var fecha = DateTime.Now.AddMonths(i - 1);
                cn.TRPagoes.Add(new TRPago
                {
                    Afiliacion = input.Id,
                    Cliente = input.Cliente,
                    Seguro = input.Seguro,
                    Mes = i,
                    Anio = fecha.Year,
                    Fecha = fecha,
                    Cuota = input.TMSeguro.ImporteMensual,

                    FechaCreacion = DateTime.Now,
                    Estado = "PEN",
                });
            }

            cn.SaveChanges();
        }

        public List<TRPago> GetPagos(int idAfiliacion)
        {
            var pagos = cn.TRPagoes.Where(p => p.Afiliacion == idAfiliacion).ToList();

            var dtoPagos = pagos.Select(p => new TRPago
            {
                Id = p.Id,
                Afiliacion = p.Afiliacion,
                Seguro = p.Seguro,
                Anio = p.Anio,
                Cliente = p.Cliente,
                Cuota = p.Cuota,
                Fecha = p.Fecha,
                Estado = p.Estado,
                FechaCreacion = p.FechaCreacion,
                FechaModificacion = p.FechaModificacion,
                Mes = p.Mes,
                Monto = p.Monto,
                Numero = p.Numero,
            }).ToList();

            return dtoPagos;
        }
    }
}
