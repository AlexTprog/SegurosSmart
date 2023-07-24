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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AfiliacionService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AfiliacionService.svc o AfiliacionService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AfiliacionService : ServiceBase, IEntityServiceBase<TRAfiliacion>
    {
        public void Delete(int id)
        {
            var afiliacion = Get(id);
            if (afiliacion != null)
            {
                cn.TRAfiliacions.Remove(afiliacion);
            }
        }

        public TRAfiliacion Get(int id)
        {
            var afiliacion = cn.TRAfiliacions.FirstOrDefault(x => x.Id == id);
            return afiliacion;
        }

        public List<TRAfiliacion> GetAll()
        {
            var afiliaciones = cn.TRAfiliacions.ToList();
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
