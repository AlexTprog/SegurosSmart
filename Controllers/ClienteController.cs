﻿using SegurosSmart.Models.Consts;
using SegurosSmart.Models.Enums;
using SegurosSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlTypes;
using SegurosSmart.Controllers.Base;

namespace SegurosSmart.Controllers
{
    public class ClienteController : BaseController, ICrudController<Cliente>
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            var clienteDb = cn.TMCliente.Where(p => p.Id == id && p.Estado == ((int)EstadoSeguro.ACTIVO))
                .Select(p => new
                {
                    p.Id,
                    p.Nombres,
                    p.ApellidoPaterno,
                    p.ApellidoMaterno,
                    FechaNacimiento = p.FechaNacimiento.ToShortDateString(),
                    Genero = (Genero)p.Genero,
                    p.Telefono,
                    p.Direccion,
                    TipoDocumento = (TipoDocumento)p.TipoDocumento,
                    p.DocumentoIdentidad,
                    p.Email
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
                    FechaNacimiento = p.FechaNacimiento.ToShortDateString(),
                    Genero = Enum.GetName(typeof(Genero), p.Genero),
                    p.Telefono,
                    p.Direccion,
                    p.Email,
                    TipoDocumento = Enum.GetName(typeof(TipoDocumento), p.TipoDocumento),
                    p.DocumentoIdentidad
                })
                .ToList();

            return Json(clientesDb, JsonRequestBehavior.AllowGet);
        }

        public int Delete(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                var clienteDb = cn.TMCliente.Where(p => p.Id == id).First();

                clienteDb.Estado = ((int)EstadoCliente.INACTIVO);
                cn.SubmitChanges();
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
            var clienteDb = cn.TMCliente.FirstOrDefault(p => p.Id == input.Id);
            int nregistrosAfectados = 0;
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

                        FechaCreacion = DateTime.Now,
                    };

                    cn.TMCliente.InsertOnSubmit(newCliente);
                    cn.SubmitChanges();
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