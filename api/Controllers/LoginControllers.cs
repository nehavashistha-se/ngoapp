using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Business_Logic;
using api.Data;

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

        ResultReturn resultReturn = new ResultReturn();
        LoginBL LoginBL = new LoginBL();


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Appusers _ObjappUser)
        {
            resultReturn = LoginBL.GetLogindetails(_ObjappUser);
            return new JsonResult(resultReturn);
        }
        [HttpPost]
        [Route("SaveUser")]
        public IActionResult SaveUser(Appusers _ObjappUser)
        {
            resultReturn = LoginBL.SaveUser(_ObjappUser);
            return new JsonResult(resultReturn);
        }

        [HttpPost]
        [Route("SaveUserDetail")]
        public IActionResult SaveUserDetail(AppUserDetail _ObjappUserDetail)
        {
            resultReturn = LoginBL.SaveUserDetail(_ObjappUserDetail);
            return new JsonResult(resultReturn);
        }
    }
}
