
using MvcWebApp.Entities.Common;

namespace MvcWebApp.Entities
{
    public class Friend : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }          
        public int? CountryId {  get; set; }
        public Country Country { get; set; }
        //public FriendFriends UserContacts { get; set; }
        public ICollection<FriendFriends> ContactTo { get; set; }
        public ICollection<FriendFriends> ContactFrom { get; set; }
    }
}
