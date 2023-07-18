using SegurosSmart.Models.Consts;
using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegurosSmart.Controllers
{
    public class ClienteController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var clienteDb = cn.TMCliente.Where(p => p.Id == id && p.Estado == ((int)EstadoSeguro.ACTIVO)).Select(p => new
            {
                p.Id,
                p.Nombres,
                p.ApellidoPaterno,
                p.ApellidoMaterno,
                p.FechaNacimiento,
                Genero = (Genero)p.Genero,
                p.Telefono,
                p.Direccion,
                TipoDocumento = (TipoDocumento)p.TipoDocumento,
                p.DocumentoIdentidad,
            });

            var clientesFormatted = clienteDb.Select(p => new
            {
                p.Id,
                p.Nombres,
                p.ApellidoPaterno,
                p.ApellidoMaterno,
                FechaNacimiento = p.FechaNacimiento.ToString("yyyy/MM/dd"),
                Genero = p.Genero.ToString(),
                p.Telefono,
                p.Direccion,
                TipoDocumento = p.TipoDocumento.ToString(),
                p.DocumentoIdentidad,
            });

            return Json(clienteDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var clientesDb = cn.TMCliente
                .Where(p => p.Estado == (int)EstadoSeguro.ACTIVO)
                .Select(p => new
                {
                    p.Id,
                    p.Nombres,
                    p.ApellidoPaterno,
                    p.ApellidoMaterno,
                    p.FechaNacimiento,
                    Genero = (Genero)p.Genero,
                    p.Telefono,
                    p.Direccion,
                    TipoDocumento = (TipoDocumento)p.TipoDocumento,
                    p.DocumentoIdentidad,
                })
                .ToList();

            var clientesFormatted = clientesDb.Select(p => new
            {
                p.Id,
                p.Nombres,
                p.ApellidoPaterno,
                p.ApellidoMaterno,
                FechaNacimiento = p.FechaNacimiento.ToString("yyyy/MM/dd"),
                Genero = p.Genero.ToString(),
                p.Telefono,
                p.Direccion,
                TipoDocumento = p.TipoDocumento.ToString(),
                p.DocumentoIdentidad,
            });

            return Json(clientesFormatted, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var clienteDb = cn.TMCliente.Where(p => p.Id == id).First();

            clienteDb.Estado = ((int)EstadoCliente.INACTIVO);
            cn.SubmitChanges();

            return Json(clienteDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveOrUpdate(Cliente input)
        {
            var clienteDb = cn.TMCliente.FirstOrDefault(p => p.Id == input.Id);

            try
            {
                if (clienteDb is null)
                {
                    var newCliente = new Data.TMCliente
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
                        FechaCreacion = input.FechaCreacion,
                        FechaModificacion = input.FechaModificacion,
                    };

                    cn.TMCliente.InsertOnSubmit(newCliente);
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
                    clienteDb.FechaCreacion = input.FechaCreacion;
                    clienteDb.FechaModificacion = input.FechaModificacion;

                    cn.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
            }

            return Json(clienteDb, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGeneros()
        {
            List<GeneroDTO> generos = new List<GeneroDTO>();
            foreach (Genero genero in Enum.GetValues(typeof(Genero)))
            {
                generos.Add(new GeneroDTO
                {
                    Id = (int)genero,
                    Nombre = genero.ToString()
                });
            }

            return Json(generos, JsonRequestBehavior.AllowGet);
        }
    }
}