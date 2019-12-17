using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Extension
{
    public class CounterViewCompent
    {
        //private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public CounterViewCompent(
            IConnectionMultiplexer redis
           
            ) 
        {
           // _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<string> InvokeAsync(string ControllerName, string AcitonName) 
        {
            if (!string.IsNullOrWhiteSpace(ControllerName) && !string.IsNullOrWhiteSpace(AcitonName)) 
            {
                var pageId = $"{ControllerName}-{AcitonName}";
                await _database.StringIncrementAsync(pageId);
                var count = _database.StringGet(pageId);
                var result = $"{pageId}:{count}";
                return result;
            }

            throw new Exception("数据获取失败");
        }
    }
}
