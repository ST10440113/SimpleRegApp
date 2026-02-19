using System.ComponentModel.DataAnnotations;

namespace SimpleRegApp.Models
{
    public class Login
    {
        [Key] public string Username { get; set; }
        public string Password { get; set; }
       
    }
}
