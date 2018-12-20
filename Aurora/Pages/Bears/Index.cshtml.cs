using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aurora.Models;

namespace Aurora.Pages.Bears
{
    public class IndexModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;

        public IndexModel(Aurora.Models.AuroraContext context)
        {
            _context = context;
        }

        public IList<PolarBear> PolarBear { get;set; }

        public async Task OnGetAsync()
        {
            PolarBear = await _context.PolarBear.ToListAsync();
        }
    }
}
