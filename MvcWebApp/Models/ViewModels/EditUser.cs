using MvcWebApp.Entities;
using MvcWebApp.Entities.Common;

namespace MvcWebApp.Models.ViewModels
{
    public class EditUser : BaseEntity
    {
        public UserViewModel User { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
