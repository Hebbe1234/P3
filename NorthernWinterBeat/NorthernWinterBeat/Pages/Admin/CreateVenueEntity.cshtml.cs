using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthernWinterBeat.Data;
using NorthernWinterBeat.Models;

namespace NorthernWinterBeat.Pages.Admin
{
    public class CreateVenueEntityModel : PageModel
    {
        private readonly NorthernWinterBeat.Data.NorthernWinterBeatConcertContext _context;

        public CreateVenueEntityModel(NorthernWinterBeat.Data.NorthernWinterBeatConcertContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Venue Venue { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Venue.Add(Venue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./IndexAdmin");
        }
    }
}
