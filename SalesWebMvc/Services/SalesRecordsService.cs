using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate , DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(sale => sale.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sale => sale.Date <= maxDate.Value);
            }
            return await result
                .Include(sale => sale.Seller)
                .Include(sale=> sale.Seller.Department)
                .OrderByDescending(sale => sale.Date)
                .ToListAsync();
        }
    }
}
