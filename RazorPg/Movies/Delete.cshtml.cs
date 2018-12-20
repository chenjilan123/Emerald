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
    public class DeleteModel : PageModel
    {
        private readonly RazorPg.Models.RazorPgContext _context;

        public DeleteModel(RazorPg.Models.RazorPgContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperSomething = await _context.SuperSomething.FindAsync(id);

            if (SuperSomething != null)
            {
                _context.SuperSomething.Remove(SuperSomething);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
