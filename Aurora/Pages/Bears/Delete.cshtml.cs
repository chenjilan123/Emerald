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
    public class DeleteModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;

        public DeleteModel(Aurora.Models.AuroraContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PolarBear PolarBear { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PolarBear = await _context.PolarBear.FirstOrDefaultAsync(m => m.ID == id);

            if (PolarBear == null)
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

            PolarBear = await _context.PolarBear.FindAsync(id);

            if (PolarBear != null)
            {
                _context.PolarBear.Remove(PolarBear);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
