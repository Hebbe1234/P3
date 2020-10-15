using System;
using System.Collections.Generic;

public class Calendar
{
    public int ID { get; set; }

    public Calendar()
	{
        State = "Initializing"; 
	}

    private List<Concert> Concerts = new List<Concert>();
    public string State { get; set; }

    public void AddConcert(Concert concert)
    {
        Concerts.Add(concert);
    }

    public void RemoveConcert(Concert concert)
    {
        Concerts.Remove(concert);
    }
}


