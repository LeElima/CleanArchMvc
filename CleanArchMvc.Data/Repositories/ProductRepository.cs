﻿using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interface;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<Product> CreateAsync(Product category)
        {
            _productContext.Add(category);
            await _productContext.SaveChangesAsync();
            return category;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //return await _productContext.Products.FindAsync(id);
            return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductCategoryAsync(int? id)
        {
            //Eager loading
            return await _productContext.Products.Include(c=>c.Category).SingleOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> RemoveAsync(Product category)
        {
            _productContext.Remove(category);
            await _productContext.SaveChangesAsync();
            return category;
        }

        public async Task<Product> UpdateAsync(Product category)
        {
            _productContext.Update(category);
            await _productContext.SaveChangesAsync();
            return category;
        }
    }
}
