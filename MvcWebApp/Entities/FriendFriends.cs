using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.Entities
{
    public class FriendFriends
    {
        public int UserId { get; set; }
        public Friend User { get; set; }

        [Display(Name = "Contato")]
        public int FriendId { get; set; }
        public Friend Friend { get; set; }

        public FriendFriends(int userId, int friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }
    }
}
