using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class CreateConcertEntityModel : PageModel
    {
        private readonly NorthernWinterBeatConcertContext _context;

        public CreateConcertEntityModel(NorthernWinterBeatConcertContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Concert Concert { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //FestivalManager.instance._calendar.AddConcert(Conc)
            //To be moved down to a function in Model.????? 
            _context.Concert.Add(Concert);
            await _context.SaveChangesAsync();

            return RedirectToPage("./IndexAdmin");
        }
    }
}
