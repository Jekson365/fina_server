using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dto.countries;
using server.Interfaces;
using server.Models;

namespace server.Controller
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCountryDto country)
        {
            var countryModel = await _countryRepository.CreateAsync(country);
            return Ok(new { message = "created", status = 200, country = countryModel });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _countryRepository.GetAllAsync();
            return Ok(result);
        }
    }
}