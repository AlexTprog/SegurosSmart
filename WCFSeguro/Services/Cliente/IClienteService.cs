using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFSeguro.Data;

namespace WCFSeguro.Services
{
    [ServiceContract]
    public interface IClienteService
    {
        [OperationContract]
        TMCliente Get(int id);

        [OperationContract]
        List<TMCliente> GetAll();

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Save(TMCliente input);

        [OperationContract]
        void Update(TMCliente input);
    }
}
