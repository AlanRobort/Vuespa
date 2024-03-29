﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using Service.extend;

namespace Service
{
    public class ClientInfoService : IClientInfoService
    {
        private readonly StudentDbContext _studentDbContext;

        public ClientInfoService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<IEnumerable<ClientAddressInfo>> GetAll()
        {
           var query =  from c in _studentDbContext.clientAddressInfos
            select c;
            return await query.ToListAsync();
        }

        public async Task<PaginatedList<ClientAddressInfo>> GetAllAddressInfo(PaginationParamer paginationParamer)
        {
            //var result = new List<ClientAddressInfo>();
            //var linqresult = from c in _studentDbContext.clientAddressInfos
            //              select c;
            // var result = await linqresult.ToListAsync();
            // return result;
            var query = _studentDbContext.clientAddressInfos.OrderBy(x => x.Id);
            //74
            var count = await query.CountAsync();
            var items = await query
                 .Skip(paginationParamer.PageSize * paginationParamer.PageIndex)
                 .Take(paginationParamer.PageSize)
                 .ToListAsync();

            return new PaginatedList<ClientAddressInfo>(paginationParamer.PageIndex, count, paginationParamer.PageSize, items);
        }

        public async Task<IEnumerable<ClientAccessTimes>> GetClientAccessTimes()
        {
           //var query = from a in _studentDbContext.clientAccessTimes select a;
           
            var result = await _studentDbContext.clientAccessTimes.ToListAsync();
            

            
          return result;
        }

        //查询ID=x以后的y条数据
        //iD=?,y=?
        //select top 10 * from A where id not in (select top 30 id from A)
        public async Task<IEnumerable<ClientAddressInfo>> GetClientAddressInfos(int id,int num) 
        {
            var sqldata = _studentDbContext.clientAddressInfos;
            var result = from i in sqldata
                         where i.Id > id && i.Id < id + num
                         select i;

            var resultlist = await result.ToListAsync();
            return resultlist;
        }


        public bool saveClientAddress(string ClientIpAddress)
        {
            if (ClientIpAddress!=null) 
            {
                ClientAddressInfo clientdata = new ClientAddressInfo();
                clientdata.Accessingtime = DateTime.Now;
                clientdata.ClientAddress = ClientIpAddress;

                _studentDbContext.clientAddressInfos.Add(clientdata);

                var result =   _studentDbContext.SaveChanges();
                if (result > 0)
                {
                   
                    //每次保存都存
                    int Count =  SaveClinetAccesstimes();
                    //如果clientinfo的时间是当天时间则是更新

                    ClientAccessTimes clientAccessTimes = new ClientAccessTimes();
                    var Original = _studentDbContext.clientAccessTimes.FirstOrDefault(x => x.dateTime.Date == clientdata.Accessingtime.Date);
                    if (Original!=null)
                    {
                       
                        Original.Count += 1;
                        _studentDbContext.SaveChanges();
                    }
                    else
                    { 
                        //如果clientinfo的时候不是当天则添加
                        
                        clientAccessTimes.dateTime = DateTime.Now;
                        clientAccessTimes.Count += 1;
                        _studentDbContext.clientAccessTimes.Add(clientAccessTimes);
                        //int resultCount = _studentDbContext.SaveChanges();
                        
                    }
                   
                   
                   // _studentDbContext.SaveChanges();
                    return true;
                }
                else 
                {
                    return false;
                }   
            }
            return false;
        }

        private  int SaveClinetAccesstimes()
        {
            var ClientCount =  _studentDbContext.clientAddressInfos.Count();
            return ClientCount;
        }



       
    }
}
