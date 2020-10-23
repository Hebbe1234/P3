using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Calendar
{
    public enum CalendarState
    {
        INITIALIZING, ACTIVE, INACTIVE
    }

    public int ID { get; set; }

    public Calendar()
    {
        State = CalendarState.INITIALIZING;
        //Dummy Concerts, which is to get retrieved from the DB
        concerts = new List<Concert>() {
                new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ) {ID =1 },
                new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ){ID =2 },
                new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ){ID =3 },

                new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ){ID =4 },
                new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ){ID =5 },
                new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ){ID =6 },

                new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" ){ID =7 },
                new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" ){ID =8 },
                new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" ){ID =9 }
            };
        //Dummy Venues, which is to get retrieced from the DB
        venues = new List<Venue>()
        {
            new Venue("Studenterhuset", "Borgergade 3, Aalborg, 9000", 35),
            new Venue("Martinshust", "Borgergade 4, Aalborg, 9000", 2),
            new Venue("JoachimsHus", "Borgergade 5, Aalborg, 9000", 8),
            new Venue("RasmusHus", "Borgergade 6, Aalborg, 9000", 40),
            new Venue("StinesHus", "Borgergade 7, Aalborg, 9000", 20),
        };
    }

    private List<Concert> concerts = new List<Concert>();

    private List<Venue> venues = new List<Venue>(); 

    public CalendarState State { get; set; }

    public async Task AddConcert(Concert concert, NorthernWinterBeatConcertContext context, string VenueName = "")
    {
        if(VenueName != "")
        {
            concert.Venue = venues.Find(v => v.Name == VenueName);
        }

        context.Concert.Add(concert);
        await context.SaveChangesAsync();
        concerts.Add(concert);
    }

    public void RemoveConcert(Concert concert)
    {
        concerts.Remove(concert);
    }

    public Concert GetConcert(int id)
    {
        return concerts.Find(c => c.ID == id);
    }
    public List<Venue> GetVenues()
    {
        return venues; 
    }
    public List<Concert> GetConcerts()
    {
        return concerts;

    }
}


