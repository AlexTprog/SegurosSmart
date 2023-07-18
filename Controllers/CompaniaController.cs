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
            var companiaDb = cn.TMCompaniaAseguradora.Where(p => p.Id == id && p.Estado == ((int)EstadoCompania.ACTIVO))
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
                });

            return Json(companiaDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var companiasDb = cn.TMCompaniaAseguradora.Where(p => p.Estado == ((int)EstadoCompania.ACTIVO))
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
                var companiaDb = cn.TMCompaniaAseguradora.Where(p => p.Id == id).First();
                companiaDb.Estado = ((int)EstadoCompania.INACTIVO);
                cn.SubmitChanges();
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
            int nregistrosAfectados = 0;

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

                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    cn.TMCompaniaAseguradora.InsertOnSubmit(newCompania);
                    cn.SubmitChanges();
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

                    cn.SubmitChanges();
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