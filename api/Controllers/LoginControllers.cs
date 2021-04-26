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
using System.IO; 
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

        [HttpPost, DisableRequestSizeLimit]
         [Route("Upload")]
public IActionResult Upload()
{
    try
    {
        
        var file = Request.Form.Files[0];
         
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
if(!Directory.Exists(pathToSave))
Directory.CreateDirectory(pathToSave);
        if (file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine("Upload", fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { dbPath });
        }
        else
        {
            return BadRequest();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex}");
    }
}
    }
}
