using MvcWebApp.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.Entities
{
    public class Country : BaseEntity
    {
        [Display(Name = "Imagem")]
        public string imageUrl { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public ICollection<Friend> Friend { get; set; }
    }
}
