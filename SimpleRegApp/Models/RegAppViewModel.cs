using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleRegApp.Models
{
    public class RegAppViewModel
    {
        public string? SearchString { get; set; }
        public string? type { get; set; }
        public List<Events> ?Events { get; set; }
        public SelectList? Types { get; set; }

    }
}
