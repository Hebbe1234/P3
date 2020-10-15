﻿using System;
using NorthernWinterBeat.Models;

public class Booking
{

    public int ID { get; set; }
    public Booking(Participant _participant, Concert _concert)
	{
		Participant = _participant;
		Concert = _concert;
		State = "Active"; 
	}
    public string State { get; set; }
    public Participant Participant { get; set; }
	public Concert Concert { get; set; }
}
