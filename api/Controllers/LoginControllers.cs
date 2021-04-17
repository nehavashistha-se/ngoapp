using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Data;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
         private readonly DataContext _context;  
  
        public LoginController(DataContext context)  
        {  
            _context = context;  
        }  

        

        [HttpPost]
        //public async Task<IActionResult> SaveUser(Appusers appuser)
        public  IActionResult SaveUser(Appusers appuser)
        {
 
    _context.Users.Add(appuser); 
    _context.SaveChanges();
 
           return Ok(appuser.UserId); 
        } 
        [HttpGet]
 public  IActionResult GetUsers()
        {
  return Ok(_context.Users.ToList()); 
        } 
       
    }
}
