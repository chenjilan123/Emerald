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
    public class DetailsModel : PageModel
    {
        private readonly RazorPg.Models.RazorPgContext _context;

        public DetailsModel(RazorPg.Models.RazorPgContext context)
        {
            _context = context;
        }

        public SuperSomething SuperSomething { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperSomething = await _context.SuperSomething.FirstOrDefaultAsync(m => m.Id == id);

            if (SuperSomething == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
