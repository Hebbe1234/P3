using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.ComponentModel;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditConcertModel : PageModel
    {
        public Concert concert { get; set; }

        public void OnGet(int id)
        {
            concert = FestivalManager.instance._calendar.GetConcert(id);
        }
        public IActionResult OnPostEditConcert(int id)
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
            string Image = Request.Form["ImageEntered"];   // Dette virker lidt fjollet umiddelbart. 
            string Venue = Request.Form["VenueEntered"];
            string Date = Request.Form["DateEntered"];
            string StartTime = Request.Form["StartTimeEntered"];
            string EndTime = Request.Form["EndTimeEntered"];

            int Year = int.Parse(Date.Substring(0, 4));
            int Month = int.Parse(Date.Substring(5, 2));
            int Day = int.Parse(Date.Substring(8, 2));
            int StartHour = int.Parse(StartTime.Substring(0, 2));
            int StartMinute = int.Parse(StartTime.Substring(3, 2));
            int EndHour = int.Parse(EndTime.Substring(0, 2));
            int EndMinute = int.Parse(EndTime.Substring(3, 2));

            DateTime Start = new DateTime(Year, Month, Day, StartHour, StartMinute, 0);
            DateTime End = new DateTime(Year, Month, Day, EndHour, EndMinute, 0);
            Concert NewConcertInfo = new Concert(Start, End, Artist, Description);

            FestivalManager.instance._calendar.EditConcert(id, NewConcertInfo, Venue);
            return RedirectToPage("./Calendar");
        }
        public string FindStartTime()
        {
            string Hour;
            if(concert.Start.Hour == 0)
            {
                Hour = "00"; 
            } else
            {
                Hour = concert.Start.Hour.ToString(); 
            }
            if (concert.Start.Minute == 0)
            {
                return Hour + ":" + "00";
            }
            else
            {
                return Hour + ":" + concert.Start.Minute;
            }
        }
        public string FindEndTime()
        {
            string Hour;
            if (concert.End.Hour == 0)
            {
                Hour = "00";
            }
            else
            {
                Hour = concert.End.Hour.ToString();
            }
            if (concert.End.Minute == 0)
            {
                return Hour + ":" + "00";
            }
            else
            {
                return Hour + ":" + concert.End.Minute;
            }
        }
        public string FindDate()
        {
            string str1, str2;
            if(concert.Start.Month <= 9)
            {
                str1 = "0" + concert.Start.Month.ToString(); 
            } else
            {
                str1 = concert.Start.Month.ToString(); 
            }
            if(concert.Start.Day <= 9)
            {
                str2 = str1 + "-0" + concert.Start.Day; 
            } else
            {
                str2 = str1 + "-" + concert.Start.Day;
            }
            return concert.Start.Year + "-" + str2;
        }
    }


    public class ImageConverter : System.ComponentModel.TypeConverter
    {

    }
}
