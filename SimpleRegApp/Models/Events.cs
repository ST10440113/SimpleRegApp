using System.ComponentModel.DataAnnotations;

namespace SimpleRegApp.Models
{
    public class Events
    {
        [Key]public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
         
        public string ImageUrl { get; set; }
        
    }
}
