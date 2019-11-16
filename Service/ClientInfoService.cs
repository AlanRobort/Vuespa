using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Service
{
    public class ClientInfoService : IClientInfoService
    {
        private readonly StudentDbContext _studentDbContext;

        public ClientInfoService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public bool saveClientAddressAsync(string ClientIpAddress)
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
                    return true;
                }
                else 
                {
                    return false;
                }   
            }
            return false;
        }

        public int GetClinetAccesstimes() 
        {
            //qqq
            //var first = _studentDbContext.clientAddressInfos.First();
            //_studentDbContext.clientAddressInfos.Select(t=>t.Accessingtime<=DateTime.Now&&t.Accessingtime> first.Accessingtime);
            //_studentDbContext.clientAddressInfos.Count()
        }
    }
}
