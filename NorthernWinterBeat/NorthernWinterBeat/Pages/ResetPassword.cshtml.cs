using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using System.Net.Mail;

namespace NorthernWinterBeat.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private IAuthorizationManager AuthorizationManager { get; }
        private IFestivalManager FestivalManager { get; }
        public ResetPasswordModel(IAuthorizationManager authorizationManager, IFestivalManager festivalManager)
        {
            AuthorizationManager = authorizationManager;
            FestivalManager = festivalManager; 
        }
        [BindProperty(SupportsGet = true)]
        public string Alert { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPostGoBack()
        {
            return RedirectToPage("./Index");
        }

        public void OnPostEmailEntered()
        {
            Alert = "Email Send";
            string UserEmail = Request.Form["EmailEntered1"].ToString().ToLower();
            Participant p = FestivalManager.GetParticipants().Find(p => p.Email == UserEmail);
            if (p != null) 
            {
                AuthorizationManager.SendEmail(UserEmail);
            }
        }
        public IActionResult OnPostResetPassword()
        {
            bool succses; 
            string SecretCode = Request.Form["CodeEntered"];
            string UserEmail = Request.Form["EmailEntered2"].ToString().ToLower();
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];
            if(Password1Entered == Password2Entered)
            {
                succses = AuthorizationManager.ChangePassword(SecretCode, UserEmail, Password1Entered);
                if(succses == true)
                {
                    return RedirectToPage("./Index", new { Alert = "Password Changed" });
                } else
                {
                    return RedirectToPage("./ResetPassword", new { Alert = "Wrong Input" });
                }
            }
            return RedirectToPage("./ResetPassword", new { Alert = "Password Different" });
        }
    }
}
