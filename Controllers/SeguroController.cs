using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using SegurosSmart.Models.Consts;
using SegurosSmart.Controllers.Base;
using System.Collections.Generic;
using SegurosSmart.SeguroService;
using SegurosSmart.CompaniaService;

namespace SegurosSmart.Controllers
{
    public class SeguroController : BaseController, ICrudController<Seguro>
    {
        private SeguroServiceClient serviceSeguro = new SeguroServiceClient();
        private CompaniaServiceClient serviceCompania = new CompaniaServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var dbSeguro = serviceSeguro.Get(id);
            var seguroFormat = new
            {
                dbSeguro.Id,
                dbSeguro.Compania,
                dbSeguro.Descripcion,
                dbSeguro.Tipo,
                dbSeguro.Numero,
                dbSeguro.EdadMaxima,
                dbSeguro.FactorImpuesto,
                dbSeguro.PorcentajeComision,
                dbSeguro.Prima,
                dbSeguro.Moneda,
                dbSeguro.ImporteMensual,
                dbSeguro.Cobertura,
                FechaVigencia = dbSeguro.FechaVigencia.ToShortDateString(),
            };

            return Json(seguroFormat, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var dbSeguros = serviceSeguro.GetAll().Where(p => p.Estado == ((int)EstadoSeguro.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    Compania = serviceCompania.Get(p.Compania).RazonSocial,
                    p.Descripcion,
                    Tipo = Enum.GetName(typeof(TipoSeguro), p.Tipo),
                    p.Numero,
                    p.EdadMaxima,
                    p.FactorImpuesto,
                    p.PorcentajeComision,
                    p.Prima,
                    Moneda = Enum.GetName(typeof(Moneda), p.Moneda),
                    p.ImporteMensual,
                    p.Cobertura,
                    FechaVigencia = p.FechaVigencia.ToShortDateString(),
                }).ToList();


            return Json(dbSeguros, JsonRequestBehavior.AllowGet);
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var seguroDb = serviceSeguro.Get(id);
                seguroDb.Estado = (int)EstadoSeguro.INACTIVO;
                serviceSeguro.Update(seguroDb);
                nregistrosAfectados = 1;
            }
            catch (Exception ex)
            {
                nregistrosAfectados = 0;
            }
            return nregistrosAfectados;
        }

        public int SaveOrUpdate(Seguro input)
        {
            var seguroDb = serviceSeguro.Get(input.Id);
            int nregistrosAfectados = 0;
            try
            {
                if (seguroDb.Id == 0)
                {
                    var newSeguro = new SeguroService.TMSeguro
                    {
                        Numero = input.Numero,
                        Cobertura = input.Cobertura,
                        Compania = input.Compania,
                        Descripcion = input.Descripcion,
                        EdadMaxima = input.EdadMaxima,
                        Estado = (int)input.Estado,
                        FactorImpuesto = input.FactorImpuesto,
                        FechaVigencia = input.FechaVigencia,
                        ImporteMensual = input.ImporteMensual,
                        Moneda = (int)input.Moneda,
                        PorcentajeComision = input.PorcentajeComision,
                        Prima = input.Prima,
                        Tipo = (int)input.Tipo,
                        //ADUIT
                        FechaCreacion = DateTime.Now,
                    };
                    serviceSeguro.Save(newSeguro);

                    nregistrosAfectados = 1;
                }
                else
                {
                    seguroDb.Numero = input.Numero;
                    seguroDb.Cobertura = input.Cobertura;
                    seguroDb.Compania = input.Compania;
                    seguroDb.Descripcion = input.Descripcion;
                    seguroDb.EdadMaxima = input.EdadMaxima;
                    seguroDb.Estado = (int)input.Estado;
                    seguroDb.FactorImpuesto = input.FactorImpuesto;
                    seguroDb.FechaVigencia = input.FechaVigencia;
                    seguroDb.ImporteMensual = input.ImporteMensual;
                    seguroDb.Moneda = (int)input.Moneda;
                    seguroDb.PorcentajeComision = input.PorcentajeComision;
                    seguroDb.Prima = input.Prima;
                    seguroDb.Tipo = (int)input.Tipo;
                    //AUDIT
                    seguroDb.FechaModificacion = DateTime.Now;
                    
                    serviceSeguro.Update(seguroDb);
                    nregistrosAfectados = 1;
                }
            }
            catch (Exception ex)
            {
                nregistrosAfectados = 0;
            }
            return nregistrosAfectados;
        }

        public JsonResult GetTipoSeguro()
        {
            var tiposSeguro = new List<(int, string)>();

            foreach (var seguro in Enum.GetValues(typeof(TipoSeguro)))
            {
                tiposSeguro.Add((Convert.ToInt32(seguro), Convert.ToString(seguro)));
            }

            return Json(tiposSeguro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMonedas()
        {
            var monedas = new List<(int, string)>();

            foreach (var moneda in Enum.GetValues(typeof(Moneda)))
            {
                monedas.Add((Convert.ToInt32(moneda), Convert.ToString(moneda)));
            }

            return Json(monedas, JsonRequestBehavior.AllowGet);
        }
    }
}