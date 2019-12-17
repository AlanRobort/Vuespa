using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Webapi.Extension;

namespace Webapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisTestController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;
        //private readonly CounterViewCompent _counter;
        private readonly IDatabase _database;

        public RedisTestController(
            IConnectionMultiplexer redis
            
            )
        {
            _redis = redis;
           // _counter = counter;
            _database =_redis.GetDatabase();
        }


        [HttpGet]
        public IActionResult GetName() 
        {
          var issuccess = _database.StringSet("fullname","RobortAlan");
            if (issuccess) 
            {
                var result = _database.StringGet("fullname");
                return Ok(result);
            }
           
            return BadRequest("获取失败");
            
        }

        [HttpGet]
        public async Task<IActionResult> GetControllerandActionCount() 
        {
            CounterViewCompent count = new CounterViewCompent(_redis);
            
            var ControllerName = RouteData.Values["Controller"] as string;
            var ActionName = RouteData.Values["Action"] as string;
            var result =  await count.InvokeAsync(ControllerName, ActionName);
            return Ok(result);
        }
    }
}