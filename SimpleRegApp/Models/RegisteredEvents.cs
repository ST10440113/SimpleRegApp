using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRegApp.Models
{
    public class RegisteredEvents
    {
        [Key]
       
        public int Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [ForeignKey("Events")]
        [ValidateNever]

        public int EventId { get; set; }
        public Events? Event { get; set; }


    }
}
