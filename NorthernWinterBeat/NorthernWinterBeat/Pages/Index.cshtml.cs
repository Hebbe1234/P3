using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NorthernWinterBeatLibrary.Managers;
using static NorthernWinterBeatLibrary.Models.ApplicationUser;

namespace NorthernWinterBeat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {

        }
        public IActionResult OnPostRegister()
        {
            string TicketInput = Request.Form["TicketEntered"];
            string EmailInput = Request.Form["EmailEntered"];
            string PasswordInput = Request.Form["PasswordEntered"];
            //Her testes hvorvidt en billet er indtastet, og valideringen skal ske her. 
            if (TicketInput != "")
            {
                return RedirectToPage("./MakeUserLogin", new { ticketNumber = TicketInput });
            }

            //Her endes der hvis der er indtastet noget forkert eller intet, derfor reloades der. 
            return Page();

        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            string EmailInput = Request.Form["EmailEntered"];
            string PasswordInput = Request.Form["PasswordEntered"];

            if (PasswordInput == "") { return Page(); }

            var user = DatabaseManager.GetUser(EmailInput);

            if (AuthorizationManager.instance.Encrypt(PasswordInput) == user?.Password)
            {

                var (claimsIdentity, authProperties) = AuthorizationManager.instance.CreateClaim(user);

                await HttpContext.SignOutAsync();

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties);

                var role = claimsIdentity.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).First();

                switch (role)
                {
                    case "PARTICIPANT":
                        return RedirectToPage("./ParticipantRazor/ParticipantConcertOverview");
                    case "ADMIN":
                        return RedirectToPage("./Admin/IndexAdmin");
                    case "VENUE":
                        return RedirectToPage("./VenueRazor/IndexVenue");
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("WRONG PASSWORD");
            }

            //Her endes der hvis der er indtastet noget forkert eller intet, derfor reloades der. 
            return RedirectToPage("./Index");

        }
    }
}
