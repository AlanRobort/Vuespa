﻿using Model;
using Service.extend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  public  interface IClientInfoService
    {
       bool saveClientAddress(string IpClientAddresss);
        //int SaveClinetAccesstimes();
        Task<IEnumerable<ClientAccessTimes>> GetClientAccessTimes();
        Task<IEnumerable<ClientAddressInfo>> GetAllAddressInfo(PaginationParamer paginationParamer);
    }
}
