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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CompaniaService : ServiceBase, IEntityServiceBase<TMCompaniaAseguradora>
    {
        public void Delete(int id)
        {
            var compania = Get(id);

            if (compania != null)
                cn.TMCompaniaAseguradoras.Remove(compania);
        }

        public TMCompaniaAseguradora Get(int id)
        {
            var compania = cn.TMCompaniaAseguradoras.FirstOrDefault(p => p.Id == id);

            return compania;
        }

        public List<TMCompaniaAseguradora> GetAll()
        {
            var companias = cn.TMCompaniaAseguradoras.ToList();
            return companias;
        }

        public void Save(TMCompaniaAseguradora input)
        {
            input.FechaCreacion = DateTime.Now;
            cn.TMCompaniaAseguradoras.Add(input);
            cn.SaveChanges();
        }

        public void Update(TMCompaniaAseguradora input)
        {
            var compania = Get(input.Id);

            if (compania != null)
            {
                compania.RazonSocial = input.RazonSocial;
                compania.Ruc = input.Ruc;
                compania.Celular = input.Celular;
                compania.Contacto = input.Contacto;
                compania.Contrato = input.Contrato;
                compania.Descripcion = input.Descripcion;
                compania.Estado = input.Estado;
                compania.FechaRenovacion = input.FechaRenovacion;                
                //Audit
                compania.FechaModificacion = DateTime.Now;
            }
            cn.SaveChanges();
        }
    }
}
