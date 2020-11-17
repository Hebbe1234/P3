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
        private IAuthorizationManager AuthorizationManager { get; }
        public IndexModel(IAuthorizationManager authorizationManager)
        {
            AuthorizationManager = authorizationManager;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPostRegister()
        {
            string TicketInput = Request.Form["TicketEntered"];

            if(AuthorizationManager.VerifyTicket(TicketInput))
            {
                return RedirectToPage("./MakeUserLogin", new { ticketNumber = TicketInput });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            string EmailInput = Request.Form["EmailEntered"];
            string PasswordInput = Request.Form["PasswordEntered"];

            if (PasswordInput == "") 
            { 
                return Page(); 
            }

            var user = AuthorizationManager.GetUser(EmailInput);
           
            if (user != null && AuthorizationManager.Encrypt(PasswordInput) == user?.Password)
            {
                var (claimsIdentity, authProperties) = AuthorizationManager.CreateClaim(user);

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
                        return RedirectToPage("./VenueRazor/IndexVenue", new { id = user.VenueID });
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
