
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Appusers
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username{get;set;}
        [Required]
        public string Password { get; set; }
    }
}