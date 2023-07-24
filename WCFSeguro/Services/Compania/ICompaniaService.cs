using System.Collections.Generic;
using System.ServiceModel;
using WCFSeguro.Data;

namespace WCFSeguro.Services
{
    [ServiceContract]
    public interface ICompaniaService
    {
        [OperationContract]
        TMCompaniaAseguradora Get(int id);

        [OperationContract]
        List<TMCompaniaAseguradora> GetAll();

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Save(TMCompaniaAseguradora input);

        [OperationContract]
        void Update(TMCompaniaAseguradora input);
    }
}
