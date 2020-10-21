using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Data;

namespace NorthernWinterBeat.Pages.Admin
{
    public class DeleteConcertEntityModel : PageModel
    {
        private readonly NorthernWinterBeat.Data.NorthernWinterBeatConcertContext _context;

        public DeleteConcertEntityModel(NorthernWinterBeat.Data.NorthernWinterBeatConcertContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Concert Concert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Concert = await _context.Concert.FirstOrDefaultAsync(m => m.ID == id);

            if (Concert == null)
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

            Concert = await _context.Concert.FindAsync(id);

            if (Concert != null)
            {
                _context.Concert.Remove(Concert);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./IndexAdmin");
        }
    }
}
