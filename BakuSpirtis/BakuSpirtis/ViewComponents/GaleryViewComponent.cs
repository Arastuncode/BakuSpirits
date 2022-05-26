using BakuSpirtis.Data;
using BakuSpirtis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewComponents
{
    public class GaleryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public GaleryViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {

            List<Galery> galeries = await _context.Galeries.Where(m => m.IsDeleted == false).Take(take).ToListAsync();
            return (await Task.FromResult(View(galeries)));
        }
    }
}
