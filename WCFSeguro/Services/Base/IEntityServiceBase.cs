using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFSeguro.Data;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IEntityServiceBase<T>
    {        

        [OperationContract]
        T Get(int id);

        [OperationContract]
        List<T> GetAll();

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Save(T input);

        [OperationContract]
        void Update(T input);

    }
}
