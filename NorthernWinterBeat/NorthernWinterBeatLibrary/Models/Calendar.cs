using System;
using System.Collections.Generic;

public class Calendar
{
    public int ID { get; set; }

    public Calendar()
	{
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

    public Concert GetConcert(int id)
    {
        return concerts.Find(c => c.ID == id);
    }
}


