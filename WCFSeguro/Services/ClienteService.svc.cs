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
    public class ClienteService : ServiceBase, IEntityServiceBase<TMCliente>
    {
        public void Delete(int id)
        {
            var cliente = Get(id);

            if (cliente != null)
                cn.TMClientes.Remove(cliente);
        }

        public TMCliente Get(int id)
        {
            var cliente = cn.TMClientes.FirstOrDefault(x => x.Id == id);
            return cliente;
        }

        public List<TMCliente> GetAll()
        {
            var clientes = cn.TMClientes.ToList();
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
