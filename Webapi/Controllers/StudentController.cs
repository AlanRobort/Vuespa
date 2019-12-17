using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentservice _studentservice;

        public StudentController(IStudentservice studentservice)
        {
            _studentservice = studentservice;
        }


        //api/get
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var aud = identity.Claims.FirstOrDefault(x => x.Type.ToString() == "audience");

            var result = await _studentservice.GetAll();
            if (result==null) {
                Response.StatusCode = 500;
                return null;
            }
            return result;
        }

        //api/login
        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody]User user)
        //{
        //    if (user != null)
        //    {
        //        var result = await _studentservice.Login(user);
        //        if (result == null)
        //        {
        //            throw new Exception("用户输入数据错误");
        //        }
        //        return Ok();
        //    }
        //    return BadRequest();
        //}


        //api/get/id
        [HttpGet("{id}")]
        public async Task<Student> GetById(int id) 
        {
            var result = await _studentservice.GetById(id);
            if (result != null)
            {
                return result;
            }
            else {
                throw new Exception("未找到数据");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student studentmodel) 
        {
            if (studentmodel != null) {
                bool result = await _studentservice.Add(studentmodel);
                if (!result) 
                {
                    Response.StatusCode = 400;
                    throw new Exception("没有接收到数据");   
                }
                return Ok();
            }
            return BadRequest();
        
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id) 
        {
            var result = await _studentservice.GetById(id);
            if (result == null) 
            {
                throw new Exception("数据库中未找到删除对应的id");
            }
             
             _studentservice.Delete(result);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Student model) 
        {

            if (model == null)
            {
                return BadRequest("数据库跟新失败");

            }
            else 
            {
                bool isUpdate = await _studentservice.Update(model);
                if (!isUpdate)
                {
                    throw new Exception("跟新失败");
                }
                else {
                    return Ok();
                }
            }
           
           
        }
       

    }
}