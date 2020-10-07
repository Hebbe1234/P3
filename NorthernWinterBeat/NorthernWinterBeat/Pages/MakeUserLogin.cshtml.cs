using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages
{
    public class MakeUserLoginModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            string UsernameEntered = Request.Form["UsernameEntered"];
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];

            if(Password1Entered != Password2Entered)
            {
                return RedirectToPage("./MakeUserLogin");
            }

            if (UsernameEntered == "")
            {
                return RedirectToPage("./MakeUserLogin");
            }

            return RedirectToPage("./MakeUserLogin");
        }
    }
}
