using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Business_Logic;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController][Route("api/[controller]")] 
    // [Authorize] 
  public class LoginController: ControllerBase {

    public LoginController(DataContext context) {

      LoginBL._context = context;
    }

    LoginBL LoginBL = new LoginBL();

    [HttpPost][Route("Login")]
    public IActionResult Login(AppUser _ObjappUser) {
      ResultReturn < AppUser > resultReturn = new ResultReturn < AppUser > ();
      try {
       
        resultReturn = LoginBL.GetLogindetails(_ObjappUser);
        // if(resultReturn.Data==null)
        // return BadRequest();
        // else
        {
            var secretkey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@365"));
            var signingCredentials=new SigningCredentials(secretkey,SecurityAlgorithms.HmacSha256);
            var tokenOption=new JwtSecurityToken(
            issuer:"https://localhost:5001",
            audience:"https://localhost:5001",
            claims:new List<Claim>(),
            expires:DateTime.Now.AddMinutes(5),
            signingCredentials:signingCredentials

            );
            var tokenString=new JwtSecurityTokenHandler().WriteToken(tokenOption);
            //return Ok(new {Token=tokenString});
            return new JsonResult(resultReturn);
        }
        
      } catch(Exception ex) {
        resultReturn.Exception = ex.Message;
        resultReturn.Status_Code = Enums.ResultStatus.InvalidLogin;
      }
      return Unauthorized()      ;
    }

    [HttpPost][Route("GetUser")]
    public IActionResult GetUser(AppUserDetail _ObjappUser) {

      var resultReturn = LoginBL.GetUser(_ObjappUser);
      return new JsonResult(resultReturn);
    }

    [HttpPost][Route("DeleteUser")]
    public IActionResult DeleteUser(AppUserDetail _ObjappUser) {

      var resultReturn = LoginBL.DeleteUser(_ObjappUser);
      return new JsonResult(resultReturn);
    } [HttpPost][Route("SaveUserDetail")]
    public IActionResult SaveUserDetail(AppUserDetail _ObjappUserDetail) {
      var resultReturn = LoginBL.SaveUserDetail(_ObjappUserDetail);
      return new JsonResult(resultReturn);
    }

    [HttpPost, DisableRequestSizeLimit][Route("Upload")]
    public IActionResult Upload() {
      try {

        var file = Request.Form.Files[0];

        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
        if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);
        if (file.Length > 0) {
          var fileName = Path.GetFileName(file.FileName);
          var fullPath = Path.Combine(pathToSave, fileName);
          var dbPath = Path.Combine("Upload", fileName);

          using(var stream = new FileStream(fullPath, FileMode.Create)) {
            file.CopyTo(stream);
          }

          return Ok(new {
            dbPath
          });
        }
        else {
          return BadRequest();
        }
      }
      catch(Exception ex) {
        return StatusCode(500, $"Internal server error: {ex}");
      }
    }
  }
}