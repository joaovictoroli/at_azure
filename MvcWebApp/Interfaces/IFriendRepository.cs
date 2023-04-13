using MvcWebApp.Entities;

namespace MvcWebApp.Interfaces
{
    public interface IFriendRepository
    {
        Task<IEnumerable<FriendFriends>> GetAllByUserIdAsync(int id);

        Task<IEnumerable<FriendFriends>> GetAllLinkedUsers(int id);

        Task<FriendFriends> AddAsync(FriendFriends userContact);

        Task<FriendFriends> DeleteAsync(FriendFriends userContact);

    }
}
