using SegurosSmart.AfiliacionService;
using SegurosSmart.ClienteService;
using SegurosSmart.CompaniaService;
using SegurosSmart.Controllers.Base;
using SegurosSmart.Models;
using SegurosSmart.Models.Enums;
using SegurosSmart.PagoService;
using SegurosSmart.SeguroService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SegurosSmart.Controllers
{
    public class AfiliacionController : BaseController, ICrudController<Afiliacion>
    {
        private AfiliacionServiceClient serviceAfiliacion = new AfiliacionServiceClient();
        private SeguroServiceClient serviceSeguro = new SeguroServiceClient();
        private ClienteServiceClient serviceCliente = new ClienteServiceClient();
        private PagoServiceClient servicePago = new PagoServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var dbAfilicacion = serviceAfiliacion.Get(id);
                dbAfilicacion.Estado = (int)EstadoAfiliacion.INACTIVO;

                serviceAfiliacion.Update(dbAfilicacion);
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
            var dbAfiliacion = serviceAfiliacion.Get(id);
            var afiliacionFormat = new
            {
                dbAfiliacion.Id,
                dbAfiliacion.Cliente,
                dbAfiliacion.Seguro,
                FechaAfiliacion = dbAfiliacion.FechaAfiliacion.ToShortDateString(),
            };

            return Json(afiliacionFormat, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var dbAfiliaciones = serviceAfiliacion.GetAll()
                .Where(p => p.Estado == (int)EstadoAfiliacion.ACTIVO)
                .Select(p => new
                {
                    p.Id,
                    Cliente = GetNombreCompletCliente(p.Cliente),
                    Seguro = GetSeguroDescripcion(p.Seguro),
                    FechaAfiliacion = p.FechaAfiliacion.ToShortDateString(),
                });

            return Json(dbAfiliaciones, JsonRequestBehavior.AllowGet);
        }

        private string GetNombreCompletCliente(int id)
        {
            var cliente = serviceCliente.Get(id);
            return $"{cliente.Nombres} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}";
        }

        private string GetSeguroDescripcion(int id)
        {
            var seguro = serviceSeguro.Get(id);
            return seguro.Descripcion;
        }

        public int SaveOrUpdate(Afiliacion input)
        {
            var dbAfiliacion = serviceAfiliacion.Get(input.Id);
            int responseCode = 0;
            try
            {
                if (dbAfiliacion.Id == 0)
                {
                    //Podria validar que tengan el estado activo
                    //No es necesario porque solo listo los activos
                    var seguro = serviceSeguro.Get(input.Seguro);
                    var cliente = serviceCliente.Get(input.Cliente);

                    var edad = DateTime.Now.Year - cliente.FechaNacimiento.Year;

                    if (edad >= seguro.EdadMaxima)
                    {
                        return responseCode = 2;
                    }

                    var newAfiliacion = new AfiliacionService.TRAfiliacion
                    {
                        Cliente = input.Cliente,
                        Seguro = input.Seguro,
                        FechaAfiliacion = input.FechaAfiliacion,

                        Estado = (int)EstadoAfiliacion.ACTIVO,
                        FechaCreacion = DateTime.Now,
                    };

                    serviceAfiliacion.Save(newAfiliacion);
                    responseCode = 1;
                }
                else
                {
                    var cliente = serviceCliente.Get(dbAfiliacion.Cliente);
                    var edad = DateTime.Now.Year - cliente.FechaNacimiento.Year;

                    if (edad >= dbAfiliacion.TMSeguro.EdadMaxima)
                    {
                        return responseCode = 0;
                    }
                    dbAfiliacion.Cliente = input.Cliente;
                    dbAfiliacion.Seguro = input.Seguro;
                    dbAfiliacion.FechaAfiliacion = input.FechaAfiliacion;

                    dbAfiliacion.FechaModificacion = DateTime.Now;

                    serviceAfiliacion.Update(dbAfiliacion);

                    responseCode = 1;
                }
            }
            catch (Exception e)
            {
                responseCode = 0;
            }

            return responseCode;
        }

        //Generar Pagos
        public int GenerarPagos(Afiliacion idAfiliacion)
        {
            int response = 0;

            try
            {
                servicePago.GenerarPagos(idAfiliacion.Id);
                response = 1;
            }
            catch (Exception e)
            {
                response = 0;
            }

            return response;
        }

        public JsonResult GetPagos(int idAfiliacion)
        {
            //Formato
            var pagos = servicePago.GetPagos(idAfiliacion).ToList()
                .Select(
                p => new
                {
                    p.Id,
                    p.Anio,
                    p.Mes,
                    Fecha = p.Fecha.ToShortDateString(),
                    p.Estado,
                    Cliente = GetNombreCompletCliente(p.Cliente),
                    Seguro = GetSeguroDescripcion(p.Seguro),
                    p.Cuota,
                }
                );

            return Json(pagos, JsonRequestBehavior.AllowGet);
        }

    }
}