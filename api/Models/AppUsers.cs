
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
public class AppUser{
    public int UserId { get; set; }
       
        public string Username { get; set; }
       
        public string Password { get; set; }
       
        public string Role { get; set; }
}
    public class AppUserDetail
    {

   [Key]
        public int UserId { get; set; }
       
        public string Username { get; set; }
       
        public string Password { get; set; }
       
        public string Role { get; set; }
        
        public string Category { get; set; }


        public string Name { get; set; }

        public string Designation { get; set; }

        public string Address { get; set; }

        public string Contactno { get; set; }

        public string Email { get; set; }

        public string Dob { get; set; }

        public string Birthplace { get; set; }

        public string Publications { get; set; }

        public string Image { get; set; }

        public string Biodata { get; set; }

        public bool status { get; set; }=true;
        public DateTime createdate { get; set; } = DateTime.Now;

        public int createdby { get; set; }
    }
    public class FileUpload{
public string folder { get; set; }


    }
    public class AppUserDetailHistory
    {
        [Key]
        public DateTime logdate { get; set; } = DateTime.Now;
        public long UserId { get; set; }


        public string Category { get; set; }


        public string Name { get; set; }

        public string Designation { get; set; }

        public string Address { get; set; }

        public string Contactno { get; set; }

        public string Email { get; set; }

        public string Dob { get; set; }

        public string Birthplace { get; set; }

        public string Publications { get; set; }

        public string Image { get; set; }

        public string Biodata { get; set; }

        public bool status { get; set; }
        public DateTime createdate { get; set; }

        public int createdby { get; set; }
    }
}