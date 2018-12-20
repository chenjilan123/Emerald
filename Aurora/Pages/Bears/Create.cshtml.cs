using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aurora.Models;

namespace Aurora.Pages.Bears
{
    public class CreateModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;

        public CreateModel(Aurora.Models.AuroraContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PolarBear PolarBear { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PolarBear.Add(PolarBear);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}