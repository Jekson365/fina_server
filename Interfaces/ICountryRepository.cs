using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dto.countries;
using server.Models;

namespace server.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> CreateAsync(CreateCountryDto countryDto);
        Task<List<Country>> GetAllAsync();
    }
}