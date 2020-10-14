using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.Participant
{
    public class ParticipantArtistModel : PageModel
    {

        public string ArtistName { get; private set; }
        public string Description { get; private set; }

        public void OnGet(int? id)
        {

            ArtistName = "Artist Name";
            Description = "Joe & The Shitboys brings the perfect combination of humorous lyrics and old school punk gimmicks without getting tacky."
                + " The average song length is around 1:30 minutes, and that is all they need to make you feel like a shitty person."
                + "Which they manage to do with songs like “Life Is Great You Suck” and “If You Believe in Eating Meat, Start With Your Dog”."
                + "It’s painfully easy to ignore the roasting and enjoy the show, caused by the high energy the band delivers, and their charming and flamboyant frontman, Joe. "
                + "They have been able to land slots at Iceland Airwaves, Spot Festival and on the NME 100 Essential new artists for 2020 list in spite of their short existence as a band, and now they are hitting NWB21! "
                + "“Joe and The Shitboys were the best thing we saw at Iceland Airwaves … No - one is safe from their cheeky tongue and no - bullshit approach.” – NME"
                + "“The band are truly causing mayhem and we’re so here for it.” – The Line of Best Fit"
                + "“Chaotic” – Dork";      

        }
    }
}
