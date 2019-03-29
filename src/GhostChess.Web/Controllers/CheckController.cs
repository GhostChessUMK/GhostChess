using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GhostChess.Web.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostChess.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        [HttpGet]
        public HttpStatusCode Check(string password)
        {
            if (password != Game.Password)
            {
                return HttpStatusCode.Unauthorized;
            }

            return HttpStatusCode.OK;
        }
    }
}