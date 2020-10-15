using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.ComponentModel;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditConcertModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    public class ImageConverter : System.ComponentModel.TypeConverter
    {

    }

}
