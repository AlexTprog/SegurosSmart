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
    public interface ISeguroService
    {
        [OperationContract]
        TMSeguro Get(int id);

        [OperationContract]
        List<TMSeguro> GetAll();

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Save(TMSeguro input);

        [OperationContract]
        void Update(TMSeguro input);
    }
}
