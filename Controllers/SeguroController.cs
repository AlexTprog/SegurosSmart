using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var seguroDb = cn.TMSeguro.FirstOrDefault(p => p.Id == id && p.Estado == ((int)EstadoSeguro.ACTIVO));

            return Json(seguroDb);
        }

        public JsonResult GetAll()
        {
            var seguroDb = cn.TMSeguro.Where(p => p.Estado == ((int)EstadoSeguro.ACTIVO));

            return Json(seguroDb);
        }

        public JsonResult Delete(int id)
        {
            var seguroDb = cn.TMSeguro.FirstOrDefault(p => p.Id == id);

            seguroDb.Estado = (int)EstadoSeguro.INACTIVO;

            return Json(seguroDb);
        }

        public JsonResult SaveOrDelete(Seguro input)
        {
            var seguroDb = cn.TMSeguro.FirstOrDefault(p => p.Id == input.Id);
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

                        FechaCreacion = input.FechaCreacion,
                        FechaModificacion = input.FechaModificacion,
                    };
                    cn.TMSeguro.InsertOnSubmit(newSeguro);
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


                    seguroDb.FechaCreacion = input.FechaCreacion;
                    seguroDb.FechaModificacion = input.FechaModificacion;
                    cn.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //
            }
            return Json(seguroDb);
        }
    }
}