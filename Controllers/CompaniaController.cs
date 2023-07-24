using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SegurosSmart.Controllers.Base;
using SegurosSmart.ClienteService;
using SegurosSmart.CompaniaService;

namespace SegurosSmart.Controllers
{
    public class CompaniaController : BaseController, ICrudController<Compania>
    {
        private CompaniaServiceClient serviceCompania = new CompaniaServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var dbCompania = serviceCompania.Get(id);
            var formatCompania = new
            {
                dbCompania.Id,
                dbCompania.Descripcion,
                dbCompania.Ruc,
                dbCompania.RazonSocial,
                dbCompania.Contacto,
                dbCompania.Celular,
                dbCompania.Contrato,
                FechaRenovacion = dbCompania.FechaRenovacion.ToShortDateString(),
            };

            return Json(formatCompania, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var companiasDb = serviceCompania.GetAll().Where(p => p.Estado == ((int)EstadoCompania.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    p.Descripcion,
                    p.Ruc,
                    p.RazonSocial,
                    p.Contacto,
                    p.Celular,
                    p.Contrato,
                    FechaRenovacion = p.FechaRenovacion.ToShortDateString(),
                }).ToList();


            return Json(companiasDb, JsonRequestBehavior.AllowGet);
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var companiaDb = serviceCompania.Get(id);
                companiaDb.Estado = ((int)EstadoCompania.INACTIVO);
                serviceCompania.Update(companiaDb);
                nregistrosAfectados = 1;
            }
            catch (Exception ex)
            {
                nregistrosAfectados = 0;
            }
            return nregistrosAfectados;
        }

        public int SaveOrUpdate(Compania input)
        {

            var companiaDb = serviceCompania.Get(input.Id);
            int nregistrosAfectados = 0;
            try
            {
                if (companiaDb.Id == 0)
                {
                    var newCompania = new CompaniaService.TMCompaniaAseguradora
                    {
                        RazonSocial = input.RazonSocial,
                        Ruc = input.Ruc,
                        Celular = input.Celular,
                        Contacto = input.Contacto,
                        Contrato = input.Contrato,
                        Descripcion = input.Descripcion,
                        Estado = (int)input.Estado,
                        FechaRenovacion = input.FechaRenovacion,

                        FechaCreacion = DateTime.Now,
                    };
                    serviceCompania.Save(newCompania);

                    nregistrosAfectados = 1;
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
                    companiaDb.FechaModificacion = DateTime.Now;

                    serviceCompania.Update(companiaDb);
                    nregistrosAfectados = 1;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                nregistrosAfectados = 0;
            }

            return nregistrosAfectados;
        }
    }
}