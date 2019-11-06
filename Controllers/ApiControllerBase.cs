using SarayTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SarayTap.Controllers
{
    public class ApiControllerBase : ApiController
    {
       protected SarayTapEntities db = new SarayTapEntities();
    }
}
