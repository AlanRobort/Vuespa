using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  public  interface IClientInfoService
    {
       bool saveClientAddressAsync(string IpClientAddresss);
        int GetClinetAccesstimes();
    }
}
