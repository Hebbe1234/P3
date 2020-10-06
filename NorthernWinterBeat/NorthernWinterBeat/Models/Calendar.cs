using System;
using System.Collections.Generic;

public class Calendar
{
	public Calendar()
	{
	}

    private List<Concert> Concerts = new List<Concert>(); 
        

	public void AddConcert(Concert concert)
    {
        Concerts.Add(concert);
    }

    public void RemoveConcert(Concert concert)
    {
        Concerts.Remove(concert);
    }
}


