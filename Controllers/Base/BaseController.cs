using SegurosSmart.Controllers.Base;
using SegurosSmart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegurosSmart.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base
        public readonly ConnectionDataContext cn = new ConnectionDataContext();
       
    }
}