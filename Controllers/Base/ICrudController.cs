using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegurosSmart.Controllers.Base
{
    public interface ICrudController<T>
    {
        ActionResult Index();

        JsonResult Get(int id);

        JsonResult GetAll();

        int Delete(int id);

        int SaveOrUpdate(T input);
    }
}