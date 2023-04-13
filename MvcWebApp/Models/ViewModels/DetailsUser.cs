using MvcWebApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.Models.ViewModels
{
    public class DetailsUser
    {
        public UserViewModel User { get; set; }

        public List<ContactViewModel> userContacts { get; set; }
    }
}
