using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPg.Models;

namespace RazorPg.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPg.Models.RazorPgContext _context;

        public EditModel(RazorPg.Models.RazorPgContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SuperSomething).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperSomethingExists(SuperSomething.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SuperSomethingExists(int id)
        {
            return _context.SuperSomething.Any(e => e.Id == id);
        }
    }
}
