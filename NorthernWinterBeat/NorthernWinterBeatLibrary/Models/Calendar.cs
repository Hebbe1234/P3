using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class Calendar
{
    public enum CalendarState
    {
        INITIALIZING, ACTIVE, INACTIVE
    }
    private List<Concert> concerts = new List<Concert>();

    private List<Venue> venues = new List<Venue>();

    private IDataAccess DataAccess { get; set; }

    public CalendarState State { get; set; }
    [Key]
    public int ID { get; set; }

    public Calendar(IDataAccess dataAccess)
    {
        DataAccess = dataAccess;
        LoadData();
        State = CalendarState.INITIALIZING;
    }

    

    private void LoadData()
    {
        concerts = DataAccess.Retrieve<Concert>();
        venues = DataAccess.Retrieve<Venue>();
    }

    public void AddConcert(Concert concert, string VenueName = "")
    {
        if(VenueName != "")
        {
            concert.Venue = venues.Find(v => v.Name == VenueName);
        }

        concerts.Add(concert);
        DataAccess.Add(concert);
    }
    public void AddVenue(Venue venue)
    {
        venues.Add(venue);
        DataAccess.Add(venue);
    }

    public Concert GetConcert(int id)
    {
        return concerts.Find(c => c.ID == id);
    }
    /// <summary>
    /// Returns the venue with corrosponding Id
    /// </summary>
    /// <param name="venueId"></param>
    /// <returns></returns>
    public Venue GetVenue(int venueId)
    {
        return venues.Find(v => v.ID == venueId); 
    }
    public List<Venue> GetVenues()
    {
        return venues; 
    }
    /// <summary>
    /// Takes a VenueID, and returns all the concert happening at the venue
    /// </summary>
    /// <param name="venueId"></param>
    /// <returns></returns>
    public List<Concert> GetConcertsAtVenue(int venueId)
    {
        return concerts.FindAll(c => c.Venue?.ID == venueId); 
    }
    public List<Concert> GetConcerts()
    {
        return concerts;
    }
    
    public void DeleteConcert(Concert concert)
    {
        if (concert != null)
        {
            concerts.Remove(concert);
            DataAccess.Remove(concert);
        }
    }
    public void DeleteVenue(Venue venue)
    {
        if(venue != null)
        {
            venues.Remove(venue);
            DataAccess.Remove(venue);
        }
    }
}


