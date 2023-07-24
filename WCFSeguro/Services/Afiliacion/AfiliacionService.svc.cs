using System;
using System.Collections.Generic;
using System.Linq;
using WCFSeguro.Data;
using WCFSeguro.Services.Base;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AfiliacionService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AfiliacionService.svc o AfiliacionService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AfiliacionService : ServiceBase, IAfiliacionService
    {
        public void Delete(int id)
        {
            var afiliacion = Get(id);
            if (afiliacion != null)
                cn.TRAfiliacions.Remove(afiliacion);

            cn.SaveChanges();
        }

        public TRAfiliacion Get(int id)
        {
            var afiliacion = cn.TRAfiliacions.FirstOrDefault(x => x.Id == id);

            var dtoAfiliacion = new TRAfiliacion();

            if (afiliacion != null)
            {
                dtoAfiliacion = new TRAfiliacion
                {
                    Id = afiliacion.Id,
                    Estado = afiliacion.Estado,
                    Cliente = afiliacion.Cliente,
                    FechaAfiliacion = afiliacion.FechaAfiliacion,
                    FechaCreacion = afiliacion.FechaCreacion,
                    FechaModificacion = afiliacion.FechaModificacion,
                    Seguro = afiliacion.Seguro,
                };
            }
            return dtoAfiliacion;
        }

        public List<TRAfiliacion> GetAll()
        {
            var afiliaciones = cn.TRAfiliacions.ToList().Select(p => new TRAfiliacion
            {
                Id = p.Id,
                Estado = p.Estado,
                Cliente = p.Cliente,
                FechaAfiliacion = p.FechaAfiliacion,
                FechaCreacion = p.FechaCreacion,
                FechaModificacion = p.FechaModificacion,
                Seguro = p.Seguro,
            }).ToList();

            return afiliaciones;
        }

        public void Save(TRAfiliacion input)
        {
            input.FechaCreacion = DateTime.Now;
            cn.TRAfiliacions.Add(input);
            cn.SaveChanges();
        }

        public void Update(TRAfiliacion input)
        {
            var afiliacion = Get(input.Id);

            if (afiliacion != null)
            {
                afiliacion.Cliente = input.Cliente;
                afiliacion.Seguro = input.Seguro;
                afiliacion.FechaAfiliacion = input.FechaAfiliacion;
                //AUDIT
                afiliacion.FechaModificacion = DateTime.Now;
            }
            cn.SaveChanges();
        }
    }
}
