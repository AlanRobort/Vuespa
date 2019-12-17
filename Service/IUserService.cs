using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  public interface IUserService
    {
        Task<bool> IsUser(string username,string password);
    }
}
