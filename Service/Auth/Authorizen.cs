using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Service.Auth
{
  public class Authorizen
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly Jwtmodel _jwtdata;

        public Authorizen(IUserService userService, IConfiguration configuration, IOptions<Jwtmodel> jwtsettings)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtdata = jwtsettings.Value;
        }
        //username password 

        //生成JWT密钥
        public async Task<string> Getjwtstring(string username,string password) 
        {
            //异步
            var isuser = await _userService.IsUser(username,password);
            if (!isuser) 
            {
                throw new Exception("用户名或密码错误");
            }
           // var jwtstring = _configuration["Jwt: JwtSecuritykey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtdata.JwtSecuritykey));

            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken
             (
                issuer: "mysite.com",
                audience: "mysite.com",
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signInCred
              );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        //验证JWT密钥


    }


}

