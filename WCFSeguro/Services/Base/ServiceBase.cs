using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCFSeguro.Data;

namespace WCFSeguro.Services.Base
{
    public abstract class ServiceBase
    {
        protected BDSegurosEntities cn = new BDSegurosEntities();
    }
}