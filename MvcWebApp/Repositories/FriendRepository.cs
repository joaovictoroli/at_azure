using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;

namespace MvcWebApp.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FriendRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<FriendFriends>> GetAllByUserIdAsync(int id)
        {
            var list = await _dbContext.FriendFriends.Where(x => x.UserId == id).ToListAsync();
            return list;
        }

        public async Task<FriendFriends> AddAsync(FriendFriends userContact)
        {
            await _dbContext.FriendFriends.AddAsync(userContact);
            await _dbContext.SaveChangesAsync();
            return userContact;
        }

        public async Task<FriendFriends> DeleteAsync(FriendFriends userContact)
        {
            int[] ids = { userContact.UserId, userContact.FriendId };
            var contact = _dbContext.FriendFriends.Where(x => x.UserId == userContact.UserId).Where(x => x.FriendId == userContact.FriendId).FirstOrDefault();

            if (contact == null)
            {
                return null;
            }

            _dbContext.FriendFriends.Remove(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<IEnumerable<FriendFriends>> GetAllLinkedUsers(int id)
        {
            var list = await _dbContext.FriendFriends.Where(x => x.FriendId == id || x.UserId == id).ToListAsync();

            _dbContext.FriendFriends.RemoveRange(list);

            await _dbContext.SaveChangesAsync();

            return list;
        }
    }
}

