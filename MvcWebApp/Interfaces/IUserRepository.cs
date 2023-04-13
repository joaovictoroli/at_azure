using MvcWebApp.Entities;

namespace MvcWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Friend>> GetAllAsync();
        Task<Friend> AddAsync(Friend friend);
        Task<Friend> GetByIdAsync(int id);
        Task<Friend> UpdateAsync(Friend friend);
        Task<Friend> DeleteAsync(int id);
        Task<IEnumerable<Friend>> GetByCountryIdAsync(int CountryId);
    }
}
