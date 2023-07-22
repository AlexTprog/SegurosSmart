using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using SegurosSmart.Models.Consts;
using SegurosSmart.Controllers.Base;
using System.Collections.Generic;

namespace SegurosSmart.Controllers
{
    public class SeguroController : BaseController, ICrudController<Seguro>
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var seguroDb = cn.TMSeguro.Where(p => p.Id == id && p.Estado == ((int)EstadoSeguro.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    p.Compania,
                    p.Descripcion,
                    p.Tipo,
                    p.Numero,
                    p.EdadMaxima,
                    p.FactorImpuesto,
                    p.PorcentajeComision,
                    p.Prima,
                    p.Moneda,
                    p.ImporteMensual,
                    p.Cobertura,
                    FechaVigencia = p.FechaVigencia.ToShortDateString(),
                });

            return Json(seguroDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var segurosDb = cn.TMSeguro.Where(p => p.Estado == ((int)EstadoSeguro.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    Compania = p.TMCompaniaAseguradora.RazonSocial,
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

            return Json(segurosDb, JsonRequestBehavior.AllowGet);
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var seguroDb = cn.TMSeguro.Where(p => p.Id == id).First();

                seguroDb.Estado = (int)EstadoSeguro.INACTIVO;
                cn.SubmitChanges();
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
            var seguroDb = cn.TMSeguro.FirstOrDefault(p => p.Id == input.Id);
            int nregistrosAfectados = 0;
            try
            {
                if (seguroDb is null)
                {
                    var newSeguro = new Data.TMSeguro
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
                    cn.TMSeguro.InsertOnSubmit(newSeguro);
                    cn.SubmitChanges();
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
                    cn.SubmitChanges();
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