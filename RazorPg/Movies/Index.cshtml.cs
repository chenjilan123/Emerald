using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPg.Models;

namespace RazorPg.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPg.Models.RazorPgContext _context;

        public IndexModel(RazorPg.Models.RazorPgContext context)
        {
            _context = context;
        }

        public IList<SuperSomething> SuperSomething { get;set; }

        public async Task OnGetAsync()
        {
            SuperSomething = await _context.SuperSomething.ToListAsync();
        }
    }
}
