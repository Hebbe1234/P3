using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class Concert
{
    public enum ConcertState
    {
        CREATION, MAX_CAPACITY, ACTIVE, CONCERT_HELD, INACTIVE
    }

    public Concert()
    {
        State = ConcertState.CREATION; 
    }
    public Concert(DateTime _start, DateTime _end, string _artist, string _artistDescription): 
        this()
    {
        Start = _start;
        End = _end;
        Artist = _artist;
        ArtistDescription = _artistDescription;
    }
    public Concert(DateTime _start, DateTime _end, Venue _venue, string _artist, string _artistDescription):
        this(_start, _end, _artist, _artistDescription)
	{
        Venue = _venue;
	}
    [Key]
    public int ID { get; set; }
    public ConcertState State { get; set; }
    public Venue Venue { get; set; }
    public string Artist { get; set; }
    public string ArtistDescription { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string PicturePath { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>(); 
    public bool IsAtMaxCapacity { get {
            return Bookings.Count()>=(Venue?.Capacity ?? 0);
        } }

    public void AddBooking(Booking booking)
    {
		Bookings.Add(booking);
    }
	public void RemoveBooking(Booking booking)
    {
		Bookings.Remove(booking);
        DatabaseManager.context.SaveChanges();
    }

    public Booking MakeBooking(Participant p)
    {
        Booking booking = null;

        if (!IsAtMaxCapacity && p.CanMakeBookingAt(this))
        {
            booking = new Booking(p, this, DateTime.Now);
            Bookings.Add(booking);
            DatabaseManager.context.SaveChanges();
        }
        return booking;
    }




    public string FormatDate(DateTime time)
    {
        if (time == null)
        {
            return "";
        }
        string days = "00" + time.Date.Day;
        string month = "00" + time.Date.Month;
        return days.Substring(days.Length - 2) + "-" + month.Substring(month.Length - 2);
    }
    public string FormatTime(DateTime time)
    {
        if(time == null)
        {
            return ""; 
        }
        string hour = ("00" + time.Hour);
        string minute = ("00" + time.Minute);
        minute = minute.Substring(minute.Length - 2);
        hour = hour.Substring(hour.Length - 2);
        return hour + ":" + minute;
    }
}
