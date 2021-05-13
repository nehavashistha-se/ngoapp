using System;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Business_Logic;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
   [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        LoginBL LoginBL = new LoginBL();
        public LoginController(DataContext context, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            LoginBL._context = context;
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AppUser _ObjappUser)
        {

            try
            {

                var resultReturn = LoginBL.GetLogindetails(_ObjappUser);
                if (resultReturn.Data == null)
                {
                    return Unauthorized();
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_jwtAuthenticationManager.Key);
                var tokenDescritor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                                new Claim(ClaimTypes.Name,_ObjappUser.Username)
                            }),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescritor);
                //new{token=tokenHandler.WriteToken(token)}
                return new JsonResult( new{token=tokenHandler.WriteToken(token),resultdata=resultReturn});
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }

        }

        [HttpPost]
        [Route("GetUser")]
        public IActionResult GetUser(AppUserDetailGet _ObjappUser)
        {
           var currentUser = HttpContext.User;  

            var resultReturn = LoginBL.GetUser(_ObjappUser._ObjAppUser, _ObjappUser.numberOfObjectsPerPage
            , _ObjappUser.pageNumber);
            return new JsonResult(resultReturn);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(AppUserDetail _ObjappUser)
        {

            var resultReturn = LoginBL.DeleteUser(_ObjappUser);
            return new JsonResult(resultReturn);
        }
        [HttpPost]
        [Route("SaveUserDetail")]
        public IActionResult SaveUserDetail(AppUserDetail _ObjappUserDetail)
        {
            var resultReturn = LoginBL.SaveUserDetail(_ObjappUserDetail);
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
                if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine("Upload", fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new
                    {
                        dbPath
                    });
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


        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/" + filename);

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
    }
}