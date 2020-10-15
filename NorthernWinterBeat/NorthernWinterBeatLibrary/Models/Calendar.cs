using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;

public class Calendar
{
    public int ID { get; set; }

    public Calendar()
    {
        State = "Initializing";
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
    }

    private List<Concert> concerts = new List<Concert>();

    private List<Concert> Concerts = new List<Concert>();
    public string State { get; set; }

    public void AddConcert(Concert concert)
    {
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

    public List<Concert> GetConcerts()
    {
        return concerts;

    }
}


