using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dto.products;
using server.Models;

namespace server.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(CreateProductDto createProductDto);
        Task<Product> UpdateAsync(UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(int id);
        Task<List<Product>> GetProductsByGroupIdAsync(int? groupId);
    }
}