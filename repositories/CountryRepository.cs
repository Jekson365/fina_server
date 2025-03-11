using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dto.countries;
using server.Interfaces;
using server.Models;

namespace server.repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Country> CreateAsync(CreateCountryDto countryDto)
        {
            var country = new Country
            {
                Name = countryDto.Name
            };

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }
    }
}