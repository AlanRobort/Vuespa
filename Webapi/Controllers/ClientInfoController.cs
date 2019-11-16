using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ClientInfoController : Controller
    {
        private readonly IClientInfoService _clientInfoService;

        // private readonly IHttpContextAccessor _httpContextAccessor;
        //private AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();

        public ClientInfoController(IClientInfoService clientInfoService)
        {
            _clientInfoService = clientInfoService;
            //  _httpContextAccessor = httpContextAccessor;

        }

        // GET: ClientInfo
        [HttpGet]
        public IActionResult GetClientAddress()
        {
            //_accessor.HttpContext.Connection.RemoteIpAddress.ToString()
            //var result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //var result = _httpContextAccessor.
            //var result = _httpContextCurrent.Value.ToString();
            var result = "error";
            // nginx 设置proxy_set_header Client $remote_addr;
            if (Request.Headers.ContainsKey("Client"))
            {
                 result = Request.Headers["Client"].ToString();
                //if (string.IsNullOrEmpty(result)) 
                //{
                    _clientInfoService.saveClientAddress(result);
                return Ok(result);
               // }
            }

            return Ok("未获取到用户IP信息");

        }

    }
    
}