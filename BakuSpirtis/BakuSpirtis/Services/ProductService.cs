using BakuSpirtis.Data;
using BakuSpirtis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(int take)
        {
            try
            {
                IEnumerable<Product> products = await _context.Products.Where(p => p.IsDeleted == false)
                      .Include(m => m.Category)
                      .Include(m => m.ProductImages)
                      .OrderByDescending(m => m.Id)
                      .Take(take)
                      .ToListAsync();
                return products;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
