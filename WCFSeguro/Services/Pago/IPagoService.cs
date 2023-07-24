using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFSeguro.Data;

namespace WCFSeguro.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPagoService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPagoService
    {
        [OperationContract]
        void GenerarPagos(int id);

        [OperationContract]
        List<TRPago> GetPagos(int idAfiliacion);
    }
}
