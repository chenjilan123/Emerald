using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPg.Models;

namespace RazorPg.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPg.Models.RazorPgContext _context;

        public CreateModel(RazorPg.Models.RazorPgContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SuperSomething SuperSomething { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SuperSomething.Add(SuperSomething);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}