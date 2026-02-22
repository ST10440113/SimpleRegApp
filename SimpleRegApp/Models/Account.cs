using System.ComponentModel.DataAnnotations;

namespace SimpleRegApp.Models
{
    public class Account
    {
        [Key] public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Username { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        
        [Required,DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}
