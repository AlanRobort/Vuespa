using Model;
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
        
        Task<IEnumerable<ClientAddressInfo>> GetAll();

        Task<IEnumerable<ClientAccessTimes>> GetClientAccessTimes();
        Task<PaginatedList<ClientAddressInfo>> GetAllAddressInfo(PaginationParamer paginationParamer);
    }
}
