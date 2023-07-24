using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WCFSeguro.Data;

namespace WCFSeguro.Services
{
    [ServiceContract]
    public interface IAfiliacionService
    {
        [OperationContract]
        TRAfiliacion Get(int id);

        [OperationContract]
        List<TRAfiliacion> GetAll();

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Save(TRAfiliacion input);

        [OperationContract]
        void Update(TRAfiliacion input);
    }
}