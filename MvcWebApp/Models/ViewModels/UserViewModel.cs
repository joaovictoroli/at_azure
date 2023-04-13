using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.Models.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Sobrenome")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Telefone")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Data de Nascimento")]
        [Required]
        public DateTime BirthDate { get; set; }
        [Display(Name = "CountryId")]
        [Required]
        public int? CountryId { get; set; }

        [Display(Name = "País")]
        public string CountryImageUrl { get; set; }
    }
}
