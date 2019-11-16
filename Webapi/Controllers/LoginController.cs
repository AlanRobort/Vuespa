using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IStudentservice _studentservice;

        public LoginController(IStudentservice studentservice)
        {
            _studentservice = studentservice;
        }

        //Post:Login/UserLogin
        [HttpPost]
        public async Task<IActionResult>  Login([FromBody]User user) 
        {
           
            if (user != null) 
            {
              var result = await  _studentservice.Login(user);
                return Ok("登录成功");
            }
            return BadRequest();
        }
    }
}