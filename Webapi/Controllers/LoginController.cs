using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Service;
using Service.Auth;

namespace Webapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        //private readonly IStudentservice _studentservice;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IOptions<Jwtmodel> _jwtsettings;
        //private readonly Jwtmodel _jwtdata;

        public LoginController(IStudentservice studentservice,IUserService userService,IConfiguration configuration,IOptions<Jwtmodel> jwtsettings)
        {
            //_studentservice = studentservice;
            _userService = userService;
            _configuration = configuration;
            _jwtsettings = jwtsettings;
            //_jwtdata = jwtsettings.Value;
        }

        //Post:Login/UserLogin
        [HttpPost]
        public async Task<IActionResult>  Login([FromBody]User user) 
        {

            Authorizen auth = new Authorizen(_userService,_configuration, _jwtsettings);
            var jwt = await auth.Getjwtstring(user.UserName,user.PassWord);
            if (string.IsNullOrWhiteSpace(jwt)) 
            {
                throw new Exception("登录失败");
            }

            return Ok(jwt);


            //if (user != null) 
            //{
            //  var result = await  _studentservice.Login(user);
            //    Authorizen auth = new Authorizen();

            //    //if (result != null) 
            //    //{
            //    //    //var username = user.UserName;
            //    //    //var userpassword = user.PassWord;
            //    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asjdklasdjklasjdklasdj"));
            //    //    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            //    //    var token = new JwtSecurityToken
            //    //        (
            //    //             issuer: "mysite.com",
            //    //            audience: "mysite.com",
            //    //            expires: DateTime.Now.AddMinutes(1),
            //    //            signingCredentials: signInCred
            //    //        );
            //    //    var tokenString =new JwtSecurityTokenHandler().WriteToken(token);
            //    //    return Ok("登录成功"+tokenString);
            //    //}
            //    //return BadRequest("登录失败");
            //    return Ok("登录成功");
            //}
            //return BadRequest();
        }
    }
}