using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Business_Logic;
using api.Data;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {


        public LoginController(DataContext context)
        {

            LoginBL._context = context;
        }

       
        LoginBL LoginBL = new LoginBL();


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AppUser _ObjappUser)
        {

         var   resultReturn = LoginBL.GetLogindetails(_ObjappUser);          
        HttpContext.Session.SetString("UserId", resultReturn.Data.UserId.ToString());
         HttpContext.Session.SetString("Role", resultReturn.Data.Role);
         
            return new JsonResult(resultReturn);
        }

          [HttpPost]
        [Route("GetUser")]
        public IActionResult GetUser(AppUserDetail _ObjappUser)
        {

//          if(HttpContext.Session.GetString("Role").ToUpper()=="USER")
//          {
// _ObjappUser.UserId=Convert.ToInt32(HttpContext.Session.GetString("UserId"));

//          }
         
          var  resultReturn = LoginBL.GetUser(_ObjappUser);
            return new JsonResult(resultReturn);
        }
       

        [HttpPost]
        [Route("SaveUserDetail")]
        public IActionResult SaveUserDetail(AppUserDetail _ObjappUserDetail)
        {
        var    resultReturn = LoginBL.SaveUserDetail(_ObjappUserDetail);
            return new JsonResult(resultReturn);
        }
    }
}
