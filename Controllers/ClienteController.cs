using SegurosSmart.Models.Consts;
using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SegurosSmart.Controllers.Base;
using SegurosSmart.ClienteService;


namespace SegurosSmart.Controllers
{
    public class ClienteController : BaseController, ICrudController<Cliente>
    {

        private ClienteServiceClient serviceCliente = new ClienteServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var dbCliente = serviceCliente.Get(id);

            //Con Dtos Quedaba Mejor D:
            //Da formato, la idea es que en caso se pueda agregar mas tipos de documento desde codigo
            var dbFormat = new
            {
                dbCliente.Id,
                dbCliente.Nombres,
                dbCliente.ApellidoPaterno,
                dbCliente.ApellidoMaterno,
                FechaNacimiento = dbCliente.FechaNacimiento.ToShortDateString(),
                Genero = (Genero)dbCliente.Genero,
                dbCliente.Telefono,
                dbCliente.Direccion,
                TipoDocumento = (TipoDocumento)dbCliente.TipoDocumento,
                dbCliente.DocumentoIdentidad,
                dbCliente.Email
            };

            return Json(dbFormat, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var dbClientes = serviceCliente.GetAll()
                .Where(p => p.Estado == (int)EstadoSeguro.ACTIVO).Select(p => new
                {
                    p.Id,
                    p.Nombres,
                    p.ApellidoPaterno,
                    p.ApellidoMaterno,
                    FechaNacimiento = p.FechaNacimiento.ToShortDateString(),
                    Genero = Enum.GetName(typeof(Genero), p.Genero),
                    p.Telefono,
                    p.Direccion,
                    p.Email,
                    TipoDocumento = Enum.GetName(typeof(TipoDocumento), p.TipoDocumento),
                    p.DocumentoIdentidad
                }).ToList();

            return Json(dbClientes, JsonRequestBehavior.AllowGet);
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var clienteDb = serviceCliente.Get(id);
                clienteDb.Estado = ((int)EstadoCliente.INACTIVO);
                serviceCliente.Update(clienteDb);
                nregistrosAfectados = 1;
            }
            catch (Exception ex)
            {
                nregistrosAfectados = 0;
            }

            return nregistrosAfectados;
        }

        public int SaveOrUpdate(Cliente input)
        {
            var clienteDb = serviceCliente.Get(input.Id);

            int nregistrosAfectados = 0;

            try
            {
                if (clienteDb.Id == 0)
                {

                    var newCliente = new TMCliente
                    {
                        Nombres = input.Nombres,
                        ApellidoMaterno = input.ApellidoMaterno,
                        ApellidoPaterno = input.ApellidoPaterno,
                        TipoDocumento = (int)input.TipoDocumento,
                        Direccion = input.Direccion,
                        DocumentoIdentidad = input.DocumentoIdentidad,
                        Email = input.Email,
                        Estado = (int)input.Estado,
                        Genero = (int)input.Genero,
                        FechaNacimiento = input.FechaNacimiento,
                        Telefono = input.Telefono,

                        FechaCreacion = DateTime.Now,
                    };

                    serviceCliente.Save(newCliente);

                    nregistrosAfectados = 1;
                }
                else
                {
                    clienteDb.Nombres = input.Nombres;
                    clienteDb.ApellidoMaterno = input.ApellidoMaterno;
                    clienteDb.ApellidoPaterno = input.ApellidoPaterno;
                    clienteDb.TipoDocumento = (int)input.TipoDocumento;
                    clienteDb.Direccion = input.Direccion;
                    clienteDb.DocumentoIdentidad = input.DocumentoIdentidad;
                    clienteDb.Email = input.Email;
                    clienteDb.Estado = (int)input.Estado;
                    clienteDb.Genero = (int)input.Genero;
                    clienteDb.FechaNacimiento = input.FechaNacimiento;
                    clienteDb.Telefono = input.Telefono;

                    clienteDb.FechaModificacion = DateTime.Now;

                    serviceCliente.Update(clienteDb);
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

        public JsonResult GetGeneros()
        {
            var generos = new List<(int, string)>();

            foreach (var genero in Enum.GetValues(typeof(Genero)))
            {
                generos.Add((Convert.ToInt32(genero), Convert.ToString(genero)));
            }

            return Json(generos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTipoDocumento()
        {
            var tiposDocumento = new List<(int, string)>();


            foreach (var documento in Enum.GetValues(typeof(TipoDocumento)))
            {
                tiposDocumento.Add((Convert.ToInt32(documento), Convert.ToString(documento)));
            }

            return Json(tiposDocumento, JsonRequestBehavior.AllowGet);
        }
    }
}