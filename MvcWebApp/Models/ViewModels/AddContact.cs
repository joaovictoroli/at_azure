using Microsoft.AspNetCore.SignalR;
using MvcWebApp.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace MvcWebApp.Models.ViewModels
{
    public class AddContact
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int FriendId { get; set; }
        public UserViewModel User { get; set; }
        public List<ContactViewModel> userContacts { get; set; }
        public FriendFriends userContact { get; set; }
    }
}
