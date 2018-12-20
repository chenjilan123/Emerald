using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aurora.Models;

namespace Aurora.Pages.Bears
{
    public class EditModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;

        public EditModel(Aurora.Models.AuroraContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PolarBear).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolarBearExists(PolarBear.ID))
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

        private bool PolarBearExists(int id)
        {
            return _context.PolarBear.Any(e => e.ID == id);
        }
    }
}
