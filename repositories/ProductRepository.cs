using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dto.products;
using server.Interfaces;
using server.Models;

namespace server.repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(CreateProductDto createProductDto)
        {
            var productModel = new Product
            {
                Name = createProductDto.Name,
                Code = createProductDto.Code,
                GroupId = createProductDto.GroupId,
                Price = createProductDto.Price,
                CountryId = createProductDto.CountryId,
                StartDate = createProductDto.StartDate,
                EndDate = createProductDto.EndDate
            };

            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var currentProduct = await _context.Products.FindAsync(updateProductDto.Id);
            if (currentProduct == null)
            {
                return null;
            }

            currentProduct.Name = updateProductDto.Name;
            currentProduct.Code = updateProductDto.Code;
            currentProduct.Price = updateProductDto.Price;
            currentProduct.GroupId = updateProductDto.GroupId;
            currentProduct.CountryId = updateProductDto.CountryId;
            currentProduct.StartDate = updateProductDto.StartDate;
            currentProduct.EndDate = updateProductDto.EndDate;

            _context.Products.Update(currentProduct);
            await _context.SaveChangesAsync();

            return currentProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct == null)
            {
                return false;
            }

            _context.Products.Remove(currentProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetProductsByGroupIdAsync(int? groupId)
        {
            if (groupId == null)
            {
                return await _context.Products.ToListAsync();
            }

            var allGroupIds = await GetAllSubGroupIds(groupId.Value);
            allGroupIds.Add(groupId.Value);

            return await _context.Products
                .Where(p => allGroupIds.Contains(p.GroupId))
                .ToListAsync();
        }

        private async Task<List<int>> GetAllSubGroupIds(int parentId)
        {
            var subGroupIds = await _context.Groups
                .Where(g => g.ParentId == parentId)
                .Select(g => g.Id)
                .ToListAsync();

            var allIds = new List<int>(subGroupIds);

            foreach (var id in subGroupIds)
            {
                allIds.AddRange(await GetAllSubGroupIds(id));
            }

            return allIds;
        }
    }
}