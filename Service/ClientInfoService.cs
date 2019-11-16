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
                    ClientAccessTimes clientAccessTimes = new ClientAccessTimes();
                    clientAccessTimes.Count = Count;
                    clientAccessTimes.dateTime = DateTime.Now;
                    _studentDbContext.clientAccessTimes.Add(clientAccessTimes);
                    _studentDbContext.SaveChanges();
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
