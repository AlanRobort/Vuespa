using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  public  class UserService : IUserService
    {
        private readonly StudentDbContext _studentDbContext;
        //private readonly IConfiguration _configuration;

        public UserService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
           
        }

        public async Task<bool> IsUser(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // var result =  _studentDbContext.users.FirstOrDefault(x => x.UserName == username && x.PassWord == password);
                var result = await _studentDbContext.users.FirstOrDefaultAsync(x => x.UserName == username && x.PassWord == password);
                if (result != null)
                {
                    return true;
                }
                //throw new Exception("用户名密码不正确");
                return false;
            } 
            else 
            {
                //throw new Exception("用户名或密码不能为空");
                return false;
            }

        }
    }
}
