using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;

namespace MvcWebApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CountryRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Country> AddAsync(Country country)
        {
            await _dbContext.Countries.AddAsync(country);
            await _dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<Country> DeleteAsync(int id)
        {
            var country = await _dbContext.Countries.FindAsync(id);

            if (country == null)
            {
                return null;
            }

            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var list = await _dbContext.Countries.ToListAsync();
            return list;
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            var country = await _dbContext.Countries.FindAsync(id);
            return country;
        }

        public async Task<Country> UpdateAsync(int id, Country country)
        {
            var existingCountry = await _dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCountry == null)
            {
                return null;
            }

            existingCountry.Name = country.Name;
            existingCountry.imageUrl = country.imageUrl;


            await _dbContext.SaveChangesAsync();

            return existingCountry;
        }
    }
}
