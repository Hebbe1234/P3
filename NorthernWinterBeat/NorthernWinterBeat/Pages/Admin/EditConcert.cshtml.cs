using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.ComponentModel;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeat.Models;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditConcertModel : PageModel
    {
        public Concert concert { get; set; }
        public List<Venue> venues { get; set; } = new List<Venue>();

        public void OnGet(int id)
        {
            concert = FestivalManager.instance._calendar.GetConcert(id);
            venues = FestivalManager.instance._calendar.GetVenues(); 
        }
        public async Task<IActionResult> OnPostEditConcertAsync(int id)
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
            //string Image = Request.Form["ImageEntered"];   // Dette virker lidt fjollet umiddelbart. 
            string Venue = Request.Form["VenueEntered"];
            string Date = Request.Form["DateEntered"];
            string StartTime = Request.Form["StartTimeEntered"];
            string EndTime = Request.Form["EndTimeEntered"];

            int Year = 0001, Month = 01, Day = 01;
            int StartHour = 0, StartMinute = 0, EndHour = 0, EndMinute = 0;
            if (Date != "")
            {
                Year = int.Parse(Date.Substring(0, 4));
                Month = int.Parse(Date.Substring(5, 2));
                Day = int.Parse(Date.Substring(8, 2));
            }
            if (StartTime != "")
            {
                StartHour = int.Parse(StartTime.Substring(0, 2));
                StartMinute = int.Parse(StartTime.Substring(3, 2));
            }
            if (EndTime != "")
            {
                EndHour = int.Parse(EndTime.Substring(0, 2));
                EndMinute = int.Parse(EndTime.Substring(3, 2));
            }

            DateTime Start = new DateTime(Year, Month, Day, StartHour, StartMinute, 0);
            DateTime End = new DateTime(Year, Month, Day, EndHour, EndMinute, 0);
            Concert NewConcertInfo = new Concert(Start, End, Artist, Description);

            await FestivalManager.instance._calendar.EditConcert(id, NewConcertInfo, Venue);
            return RedirectToPage("./Calendar");
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
