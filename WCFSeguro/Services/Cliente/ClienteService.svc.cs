using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFSeguro.Data;
using WCFSeguro.Services.Base;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClienteService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el ClienteService de prueba WCF para probar este servicio, seleccione ClienteService.svc o ClienteService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClienteService : ServiceBase, IClienteService
    {
        public void Delete(int id)
        {
            var cliente = Get(id);

            if (cliente != null)
                cn.TMClientes.Remove(cliente);

            cn.SaveChanges();
        }

        public TMCliente Get(int id)
        {
            var cliente = cn.TMClientes.FirstOrDefault(x => x.Id == id);

            var dtoCliente = new TMCliente();
            if (cliente != null)
            {
                //Generar Dtos e implementar automapper
                dtoCliente = new TMCliente
                {
                    Id = cliente.Id,
                    Nombres = cliente.Nombres,
                    ApellidoPaterno = cliente.ApellidoPaterno,
                    ApellidoMaterno = cliente.ApellidoMaterno,
                    Direccion = cliente.Direccion,
                    DocumentoIdentidad = cliente.DocumentoIdentidad,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    FechaCreacion = cliente.FechaCreacion,
                    FechaModificacion = cliente.FechaModificacion,
                    FechaNacimiento = cliente.FechaNacimiento,
                    Genero = cliente.Genero,
                    Telefono = cliente.Telefono,
                    TipoDocumento = cliente.TipoDocumento,
                };
            }
            

            return dtoCliente;
        }

        public List<TMCliente> GetAll()
        {
            //Generar Dtos e implementar automapper
            var clientes = cn.TMClientes.ToList().Select(p => new TMCliente
            {
                Id = p.Id,
                Nombres = p.Nombres,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                Direccion = p.Direccion,
                DocumentoIdentidad = p.DocumentoIdentidad,
                Email = p.Email,
                Estado = p.Estado,
                FechaCreacion = p.FechaCreacion,
                FechaModificacion = p.FechaModificacion,
                FechaNacimiento = p.FechaNacimiento,
                Genero = p.Genero,
                Telefono = p.Telefono,
                TipoDocumento = p.TipoDocumento,
            }).ToList();

            return clientes;
        }

        public void Save(TMCliente input)
        {
            input.FechaCreacion = DateTime.Now;
            cn.TMClientes.Add(input);
            cn.SaveChanges();
        }

        public void Update(TMCliente input)
        {
            var cliente = Get(input.Id);
            if (cliente != null)
            {
                cliente.Nombres = input.Nombres;
                cliente.ApellidoPaterno = input.ApellidoPaterno;
                cliente.ApellidoMaterno = input.ApellidoMaterno;
                cliente.FechaNacimiento = input.FechaNacimiento;
                cliente.Genero = input.Genero;
                cliente.Telefono = input.Telefono;
                cliente.TipoDocumento = input.TipoDocumento;
                cliente.DocumentoIdentidad = input.DocumentoIdentidad;
                cliente.Email = input.Email;
                cliente.Direccion = input.Direccion;
                cliente.Estado = input.Estado;
                //Audit                
                cliente.FechaModificacion = DateTime.Now;
            }
            cn.SaveChanges();
        }
    }
}
