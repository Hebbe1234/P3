using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Data;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditConcertEntityModel : PageModel
    {
        private readonly NorthernWinterBeat.Data.NorthernWinterBeatConcertContext _context;

        public EditConcertEntityModel(NorthernWinterBeat.Data.NorthernWinterBeatConcertContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Concert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcertExists(Concert.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./IndexAdmin");
        }

        private bool ConcertExists(int id)
        {
            return _context.Concert.Any(e => e.ID == id);
        }
    }
}
