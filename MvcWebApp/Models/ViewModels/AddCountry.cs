using Microsoft.Build.Framework;
using System.Drawing;

namespace MvcWebApp.Models.ViewModels
{
    public class AddCountry
    {
        [Required]
        public string? Photo { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
