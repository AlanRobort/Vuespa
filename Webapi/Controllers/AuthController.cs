using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult TokenResult()
        {
            var getheader = Request.Headers["Authorization"];
            if (getheader.ToString().StartsWith("Basic")) 
            {
                var headerString = getheader.ToString().Substring("Basic".Length).Trim();//admin:password
                var UsernameandPasswordenc =Encoding.UTF8.GetString(Convert.FromBase64String(headerString));
                var UsernameandPassword = UsernameandPasswordenc.Split(":");
                var username = UsernameandPassword[0];
                var password = UsernameandPassword[1];
                if (username == "Admin" && password == "123456") 
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdhjashdjaksdhajksdhajksdasd"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken
                     (
                        issuer: "mysite.com",
                        audience: "mysite.com",
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: signInCred
                      );


                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }

                return BadRequest("用户名或密码错误");
            }
            return BadRequest("数据获取失败");
          
        }

    }
}