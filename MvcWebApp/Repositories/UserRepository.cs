using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;
using System.Diagnostics.Metrics;

namespace MvcWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Friend> AddAsync(Friend friend)
        {
            await _dbContext.Friends.AddAsync(friend);
            await _dbContext.SaveChangesAsync();
            return friend;
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            var list = await _dbContext.Friends.ToListAsync();

            return list;
        }

        public async Task<Friend> GetByIdAsync(int id)
        {
            var friend = await _dbContext.Friends.FindAsync(id);
            return friend;
        }

        public async Task<Friend> UpdateAsync(Friend friend)
        {
            var existingUser = await _dbContext.Friends.FirstOrDefaultAsync(x => x.Id == friend.Id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.BirthDate = friend.BirthDate;
            existingUser.PhoneNumber = friend.PhoneNumber;
            existingUser.FirstName = friend.FirstName;
            existingUser.LastName = friend.LastName;   
            existingUser.Email = friend.Email;
            existingUser.CountryId = friend.CountryId;


            await _dbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<Friend> DeleteAsync(int id)
        {
            var friend = await _dbContext.Friends.FindAsync(id);

            if (friend != null)
            {
                _dbContext.Friends.Remove(friend);
                await _dbContext.SaveChangesAsync();
                return friend;
            }

            return null;
        }

        public async Task<IEnumerable<Friend>> GetByCountryIdAsync(int CountryId)
        {
            var friends = _dbContext.Friends.Where(x => x.CountryId == CountryId);
            if (friends != null)
            {
                foreach (var friend in friends)
                {
                    friend.CountryId = null;
                }
            }

            return friends;
        }
    }
}
