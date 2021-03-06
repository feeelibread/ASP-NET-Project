using System.Linq;
using System.Collections.Generic;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? maxDate, DateTime? minDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(x => x.Date <= minDate.Value);
            }

            if(maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }

            return await result.Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}
