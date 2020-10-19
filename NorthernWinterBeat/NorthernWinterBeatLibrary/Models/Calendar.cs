﻿using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;

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
            new Venue("Studenterhuset", "Borgergade 3, Aalborg, 9000", 35){ID = 0 },
            new Venue("Martinshust", "Borgergade 4, Aalborg, 9000", 2){ID = 1 },
            new Venue("JoachimsHus", "Borgergade 5, Aalborg, 9000", 8){ID = 2 },
            new Venue("RasmusHus", "Borgergade 6, Aalborg, 9000", 40){ID = 3 },
            new Venue("StinesHus", "Borgergade 7, Aalborg, 9000", 20){ID = 4 },
        };
    }

    private List<Concert> concerts = new List<Concert>();

    private List<Venue> venues = new List<Venue>(); 

    public CalendarState State { get; set; }

    public void AddConcert(Concert concert, string VenueName = "")
    {
        if(VenueName != "")
        {
            concert.Venue = venues.Find(v => v.Name == VenueName);
        }
        concerts.Add(concert);
    }
    public void AddVenue(Venue venue)
    {
        venues.Add(venue); 
    }

    public void RemoveConcert(Concert concert)
    {
        concerts.Remove(concert);
    }

    public Concert GetConcert(int id)
    {
        return concerts.Find(c => c.ID == id);
    }
    public Venue GetVenue(int id)
    {
        return venues.Find(v => v.ID == id); 
    }
    public List<Venue> GetVenues()
    {
        return venues; 
    }
    public List<Concert> GetConcertsAtVenue(string name)
    {
        return concerts.FindAll(c => c.Venue.Name == name); 
    }
    public List<Concert> GetConcerts()
    {
        return concerts;
    }
}


