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
    public class DetailsModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;

        public DetailsModel(Aurora.Models.AuroraContext context)
        {
            _context = context;
        }

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
    }
}
