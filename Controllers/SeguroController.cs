using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using SegurosSmart.Models.Consts;

namespace SegurosSmart.Controllers
{
    public class SeguroController : BaseController
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
                    Tipo = (TipoSeguro)p.Tipo,
                    p.Numero,
                    p.EdadMaxima,
                    p.FactorImpuesto,
                    p.PorcentajeComision,
                    p.Prima,
                    Moneda = (Moneda)p.Moneda,
                    p.ImporteMensual,
                    p.Cobertura,
                    FechaVigencia = p.FechaVigencia.ToShortDateString(),
                });

            return Json(seguroDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var seguroDb = cn.TMSeguro.Where(p => p.Estado == ((int)EstadoSeguro.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    p.Compania,
                    p.Descripcion,
                    Tipo = (TipoSeguro)p.Tipo,
                    p.Numero,
                    p.EdadMaxima,
                    p.FactorImpuesto,
                    p.PorcentajeComision,
                    p.Prima,
                    Moneda = (Moneda)p.Moneda,
                    p.ImporteMensual,
                    p.Cobertura,
                    FechaVigencia = p.FechaVigencia.ToShortDateString(),
                }).ToList();

            var seguroFormatted = seguroDb.Select(p => new
            {
                p.Id,
                p.Compania,
                p.Descripcion,
                Tipo = p.Tipo.ToString(),
                p.Numero,
                p.EdadMaxima,
                p.FactorImpuesto,
                p.PorcentajeComision,
                p.Prima,
                Moneda = p.Moneda.ToString(),
                p.ImporteMensual,
                p.Cobertura,
                p.FechaVigencia,
            });

            return Json(seguroFormatted, JsonRequestBehavior.AllowGet);
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

        public int SaveOrDelete(Seguro input)
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
                        Compania = input.Compania.Id,
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

                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    cn.TMSeguro.InsertOnSubmit(newSeguro);
                    cn.SubmitChanges();
                    nregistrosAfectados = 1;
                }
                else
                {
                    seguroDb.Numero = input.Numero;
                    seguroDb.Cobertura = input.Cobertura;
                    seguroDb.Compania = input.Compania.Id;
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
    }
}