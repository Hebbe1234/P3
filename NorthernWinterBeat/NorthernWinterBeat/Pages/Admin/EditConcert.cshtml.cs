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
using NorthernWinterBeatLibrary.DataAccess;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditConcertModel : PageModel
    {
        public EditConcertModel(IDataAccess dataAccess, IFestivalManager festivalManager, IImageManager blobStorageManager)
        {
            FestivalManager = festivalManager; 
            DataAccess = dataAccess;
            BlobStorageManager = blobStorageManager;
        }
        private IFestivalManager FestivalManager { get; }

        public Concert concert { get; set; }
        public List<Venue> venues { get; set; } = new List<Venue>();
        public IDataAccess DataAccess { get; set; }
        public IImageManager BlobStorageManager { get; }

        public void OnGet(int id)
        {
            concert = FestivalManager.Calendar.GetConcert(id);
            venues = FestivalManager.Calendar.GetVenues(); 
        }
        public IActionResult OnPostEditConcert(int id)
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
            string Image = "";
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                Image = file.FileName;
                BlobStorageManager.Upload(Image, file.OpenReadStream());
            }
            string Venue = Request.Form["VenueEntered"];
            string Date = Request.Form["DateEntered"];
            string StartTime = Request.Form["StartTimeEntered"];
            string EndTime = Request.Form["EndTimeEntered"];

            int Year = 0001, Month = 01, StartDay = 01, EndDay = 01;
            int StartHour = 0, StartMinute = 0, EndHour = 0, EndMinute = 0;
            if (Date != "")
            {
                Year = int.Parse(Date.Substring(0, 4));
                Month = int.Parse(Date.Substring(5, 2));
                StartDay = EndDay = int.Parse(Date.Substring(8, 2));
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
            if (StartHour > EndHour || ((StartHour == EndHour) && (StartMinute > EndMinute)))
            {
                EndDay++;
            }

            DateTime Start = new DateTime(Year, Month, StartDay, StartHour, StartMinute, 0);
            DateTime End = new DateTime(Year, Month, EndDay, EndHour, EndMinute, 0);
            Concert NewConcertInfo = new Concert(Start, End, Artist, Description,Image, DataAccess);

            FestivalManager.Calendar.GetConcert(id).Update(NewConcertInfo, Venue, FestivalManager);
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
        public IActionResult OnPostCancel(int id)
        {
            return RedirectToPage("./ConcertPage", new { id = id });
        }
    }
}
