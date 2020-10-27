using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
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

    public CalendarState State { get; set; }
    [Key]
    public int ID { get; set; }

    public Calendar()
    {
        //NorthernWinterBeatConcertDatabaseManager.context DatabaseManager.context = new NorthernWinterBeatConcertDatabaseManager.context();
        State = CalendarState.INITIALIZING;
        concerts = DatabaseManager.context.Concert.Include(c => c.Bookings).ToList();
        venues = DatabaseManager.context.Venue.ToList(); 
    }



    public async Task AddConcert(Concert concert, string VenueName = "")
    {
        if(VenueName != "")
        {
            concert.Venue = venues.Find(v => v.Name == VenueName);
        }

        DatabaseManager.context.Concert.Add(concert);
        await DatabaseManager.context.SaveChangesAsync();
        concerts.Add(concert);
    }
    public async Task AddVenue(Venue venue)
    {
        venues.Add(venue);

        DatabaseManager.context.Venue.Add(venue);
        await DatabaseManager.context.SaveChangesAsync();
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
        return concerts.FindAll(c => c.Venue?.Name == name); 
    }
    public List<Concert> GetConcerts()
    {
        return concerts;
    }

    public async Task EditVenue(int id, Venue NewVenueInfo)
    {
        Venue venue = venues.Find(v => v.ID == id);
        venue.Capacity = NewVenueInfo.Capacity;
        venue.Address = NewVenueInfo.Address;
        venue.Name = NewVenueInfo.Name;

        try
        {
            await DatabaseManager.context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    public async Task EditConcert(int id, Concert NewConcertInfo, string VenueName)
    {
        if (VenueName != "")
        {
            NewConcertInfo.Venue = venues.Find(v => v.Name == VenueName);
        }
        Concert concert = concerts.Find(c => c.ID == id);
        concert.Start = NewConcertInfo.Start;
        concert.End = NewConcertInfo.End;
        concert.Venue = NewConcertInfo.Venue;
        concert.Artist = NewConcertInfo.Artist;
        concert.ArtistDescription = NewConcertInfo.ArtistDescription;
        try
        {
            await DatabaseManager.context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
}


