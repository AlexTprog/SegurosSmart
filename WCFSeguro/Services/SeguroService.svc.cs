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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SeguroService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SeguroService.svc o SeguroService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SeguroService : ServiceBase, IEntityServiceBase<TMSeguro>
    {
        public void Delete(int id)
        {
            var seguro = Get(id);
            if (seguro != null)
                cn.TMSeguroes.Remove(seguro);
        }

        public TMSeguro Get(int id)
        {
            var seguro = cn.TMSeguroes.FirstOrDefault(p => p.Id == id);
            return seguro;
        }

        public List<TMSeguro> GetAll()
        {
            var seguros = cn.TMSeguroes.ToList();
            return seguros;
        }

        public void Save(TMSeguro input)
        {
            input.FechaCreacion = DateTime.Now;
            cn.TMSeguroes.Add(input);
            cn.SaveChanges();
        }

        public void Update(TMSeguro input)
        {
            var seguro = Get(input.Id);
            if (seguro != null)
            {         
                seguro.Numero = input.Numero;
                seguro.Cobertura = input.Cobertura;
                seguro.Compania = input.Compania;
                seguro.Descripcion = input.Descripcion;
                seguro.EdadMaxima = input.EdadMaxima;
                seguro.Estado = input.Estado;
                seguro.FactorImpuesto = input.FactorImpuesto;
                seguro.FechaVigencia = input.FechaVigencia;
                seguro.ImporteMensual = input.ImporteMensual;
                seguro.Moneda = input.Moneda;
                seguro.PorcentajeComision = input.PorcentajeComision;
                seguro.Prima = input.Prima;
                seguro.Tipo = input.Tipo;

                seguro.FechaModificacion = DateTime.Now;                
            }
            cn.SaveChanges();
        }
    }
}
