using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Dto.products
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal Price { get; set; }
        public int GroupId { get; set; }
        public int CountryId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}