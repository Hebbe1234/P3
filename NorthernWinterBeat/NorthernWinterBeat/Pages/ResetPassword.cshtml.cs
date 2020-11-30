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
        public ResetPasswordModel(IAuthorizationManager authorvizationManager, IFestivalManager festivalManager)
        {
            AuthorizationManager = authorvizationManager;
            FestivalManager = festivalManager; 
        }
        [BindProperty(SupportsGet = true)]
        public string Alert { get; set; }

        public void OnGet()
        {
        }

        public void OnPostEmailEntered()
        {
            Alert = "Email Send";
            string UserEmail = Request.Form["EmailEntered"];
            Participant p = FestivalManager.GetParticipants().Find(p => p.Email == UserEmail);
            if (p != null) 
            {
                AuthorizationManager.SendEmail(UserEmail, p);
            }
        }
        public void OnPostResetPassword()
        {
            string SecretCode = Request.Form["CodeEntered"];
            string UserEmail = Request.Form["EmailEntered"];
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];
            if(Password1Entered != Password2Entered)
            {
                AuthorizationManager.ChangePassword(SecretCode, UserEmail, Password1Entered);
            }
        }
    }
}
