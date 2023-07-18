using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegurosSmart.Controllers
{
    public class CompaniaController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var companiaDb = cn.TMCompaniaAseguradora.FirstOrDefault(p => p.Id == id && p.Estado == ((int)EstadoCompania.ACTIVO));

            return Json(companiaDb);
        }

        public JsonResult GetAll()
        {
            var companiasDb = cn.TMCompaniaAseguradora.Where(p => p.Estado == ((int)EstadoCompania.ACTIVO)).ToList();

            return Json(companiasDb);
        }

        public JsonResult Delete(int id)
        {
            var companiaDb = cn.TMCompaniaAseguradora.FirstOrDefault(p => p.Id == id);
            companiaDb.Estado = ((int)EstadoCompania.INACTIVO);

            cn.SubmitChanges();

            return Json(companiaDb);
        }

        public JsonResult SaveOrUpdate(Compania input)
        {
            var companiaDb = cn.TMCompaniaAseguradora.FirstOrDefault(p => p.Id == input.Id);

            try
            {
                if (companiaDb != null)
                {
                    var newCompania = new Data.TMCompaniaAseguradora
                    {
                        RazonSocial = input.RazonSocial,
                        Ruc = input.Ruc,
                        Celular = input.Celular,
                        Contacto = input.Contacto,
                        Contrato = input.Contrato,
                        Descripcion = input.Descripcion,
                        Estado = (int)input.Estado,
                        FechaRenovacion = input.FechaRenovacion,

                        FechaCreacion = input.FechaCreacion,
                        FechaModificacion = input.FechaModificacion,
                    };
                    cn.TMCompaniaAseguradora.InsertOnSubmit(newCompania);
                }
                else
                {
                    companiaDb.RazonSocial = input.RazonSocial;
                    companiaDb.Ruc = input.Ruc;
                    companiaDb.Celular = input.Celular;
                    companiaDb.Contacto = input.Contacto;
                    companiaDb.Contrato = input.Contrato;
                    companiaDb.Descripcion = input.Descripcion;
                    companiaDb.Estado = (int)input.Estado;
                    companiaDb.FechaRenovacion = input.FechaRenovacion;
                    //AUDIT
                    companiaDb.FechaCreacion = input.FechaCreacion;
                    companiaDb.FechaModificacion = input.FechaModificacion;

                    cn.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //
            }

            return Json(companiaDb);
        }
    }
}