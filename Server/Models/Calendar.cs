using System;
using System.Collections.Generic;

public class Calendar
{
	public Calendar()
	{

        var start = new DateTime(2020, 9, 18, 20, 0, 0);
        var end = start.AddMinutes(30);
        int capacity = 75;


        Concert newConcert = new Concert(start, end, capacity, new ConcertHost() { Name = "Studenterhuset", Address = "A place" });

	}

    private List<Concert> concerts = new List<Concert>(); 
        

	public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
    }

    public void RemoveConcert(Concert concert)
    {
        concerts.Remove(concert);
    }
}


