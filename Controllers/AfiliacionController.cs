using SegurosSmart.Controllers.Base;
using SegurosSmart.Models;
using SegurosSmart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegurosSmart.Controllers
{
    public class AfiliacionController : BaseController, ICrudController<Afiliacion>
    {
        public ActionResult Index()
        {
            return View();
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var dbAfilicacion = cn.TRAfiliacion.FirstOrDefault(p => p.Id == id);
                dbAfilicacion.Estado = (int)EstadoAfiliacion.INACTIVO;

                cn.SubmitChanges();
                nregistrosAfectados = 1;
            }
            catch (Exception e)
            {
                nregistrosAfectados = 0;
            }
            return nregistrosAfectados;
        }

        public JsonResult Get(int id)
        {
            var dbAfiliacion = cn.TRAfiliacion.Where(p => p.Id == id && p.Estado == (int)EstadoAfiliacion.ACTIVO)
                .Select(p => new
                {
                    p.Id,
                    p.Cliente,
                    p.Seguro,
                    FechaAfiliacion = p.FechaAfiliacion.ToShortDateString(),
                }); ;

            return Json(dbAfiliacion, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var dbAfiliaciones = cn.TRAfiliacion
                .Where(p => p.Estado == (int)EstadoAfiliacion.ACTIVO)
                .Select(p => new
                {
                    p.Id,
                    Cliente = $"{p.TMCliente.Nombres} {p.TMCliente.ApellidoPaterno} {p.TMCliente.ApellidoMaterno}",
                    Seguro = p.TMSeguro.Descripcion,
                    FechaAfiliacion = p.FechaAfiliacion.ToShortDateString(),
                });

            return Json(dbAfiliaciones, JsonRequestBehavior.AllowGet);
        }

        public int SaveOrUpdate(Afiliacion input)
        {
            var dbAfiliacion = cn.TRAfiliacion.FirstOrDefault(p => p.Id == input.Id);
            int responseCode = 0;
            try
            {
                if (dbAfiliacion is null)
                {
                    //Podria validar que tengan el estado activo
                    //No es necesario porque solo listo los activos
                    var seguro = cn.TMSeguro.FirstOrDefault(p => p.Id == input.Seguro);
                    var cliente = cn.TMCliente.FirstOrDefault(p => p.Id == input.Cliente);

                    var edad = DateTime.Now.Year - cliente.FechaNacimiento.Year;

                    if (edad >= seguro.EdadMaxima)
                    {
                        return responseCode = 2;
                        //Agregar respuesta
                    }

                    var newAfiliacion = new Data.TRAfiliacion
                    {
                        Cliente = input.Cliente,
                        Seguro = input.Seguro,
                        FechaAfiliacion = input.FechaAfiliacion,

                        Estado = (int)EstadoAfiliacion.ACTIVO,
                        FechaCreacion = DateTime.Now,
                    };

                    cn.TRAfiliacion.InsertOnSubmit(newAfiliacion);
                    cn.SubmitChanges();
                    responseCode = 1;
                }
                else
                {
                    var edad = DateTime.Now.Year - dbAfiliacion.TMCliente.FechaNacimiento.Year;

                    if (edad >= dbAfiliacion.TMSeguro.EdadMaxima)
                    {
                        return responseCode = 0;
                    }
                    dbAfiliacion.Cliente = input.Cliente;
                    dbAfiliacion.Seguro = input.Seguro;
                    dbAfiliacion.FechaAfiliacion = input.FechaAfiliacion;

                    dbAfiliacion.FechaModificacion = DateTime.Now;

                    responseCode = 1;
                }
            }
            catch (Exception e)
            {
                responseCode = 0;
            }

            return responseCode;
        }
    }
}