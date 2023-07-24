using System;
using System.Collections.Generic;
using System.Linq;
using WCFSeguro.Data;
using WCFSeguro.Services.Base;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CompaniaService : ServiceBase, ICompaniaService
    {
        public void Delete(int id)
        {
            var compania = Get(id);

            if (compania != null)
                cn.TMCompaniaAseguradoras.Remove(compania);
            cn.SaveChanges();
        }

        public TMCompaniaAseguradora Get(int id)
        {
            var compania = cn.TMCompaniaAseguradoras.FirstOrDefault(p => p.Id == id);

            //Generar Dtos e implementar automapper
            var dtoCompania = new TMCompaniaAseguradora();
            if (dtoCompania != null)
            {
                dtoCompania = new TMCompaniaAseguradora
                {
                    Id = compania.Id,
                    Celular = compania.Celular,
                    Contacto = compania.Contacto,
                    Contrato = compania.Contrato,
                    Descripcion = compania.Descripcion,
                    Estado = compania.Estado,
                    FechaCreacion = compania.FechaCreacion,
                    FechaModificacion = compania.FechaModificacion,
                    FechaRenovacion = compania.FechaRenovacion,
                    RazonSocial = compania.RazonSocial,
                    Ruc = compania.Ruc,
                };
            }

            return dtoCompania;
        }

        public List<TMCompaniaAseguradora> GetAll()
        {
            var companias = cn.TMCompaniaAseguradoras.ToList().Select(p => new TMCompaniaAseguradora
            {
                Id = p.Id,
                Celular = p.Celular,
                Contacto = p.Contacto,
                Contrato = p.Contrato,
                Descripcion = p.Descripcion,
                Estado = p.Estado,
                FechaCreacion = p.FechaCreacion,
                FechaModificacion = p.FechaModificacion,
                FechaRenovacion = p.FechaRenovacion,
                RazonSocial = p.RazonSocial,
                Ruc = p.Ruc,
            }).ToList();

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
