using MvcWebApp.Entities;

namespace MvcWebApp.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> AddAsync(Country country);

        Task<Country> DeleteAsync(int id);

        Task<Country> GetByIdAsync(int id);

        Task<Country> UpdateAsync(int id, Country country);
    }
}
