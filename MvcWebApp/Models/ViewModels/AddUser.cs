using Microsoft.Build.Framework;
using MvcWebApp.Entities;

namespace MvcWebApp.Models.ViewModels
{
    public class AddUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int CountryId { get; set; }
        public IEnumerable<Country>? Countries { get; set; }
    }
}
