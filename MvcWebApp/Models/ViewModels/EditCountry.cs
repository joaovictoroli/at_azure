using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.Models.ViewModels
{
    public class EditCountry
    {
        [Required]
        public int Id { get; set; }
      
        public string? Photo { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
