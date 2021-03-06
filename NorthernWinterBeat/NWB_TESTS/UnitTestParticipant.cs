﻿using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestParticipant
    {
        [Fact]
        public void CanMakeBookingAt_ParticipantCanMakeLegalBooking()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();
           

            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() 
            {
                 new Concert(mock.Object) 
                 {
                    Start = new DateTime(2020, 10, 10, 20, 30, 00), 
                    End = new DateTime(2020, 10, 10, 21, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 0}
                    }
                 },
                 new Concert(mock.Object) 
                 {
                    Start = new DateTime(2020, 10, 10, 21, 30, 00), 
                    End = new DateTime(2020, 10, 10, 22, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 1}
                    }
                 }
            };
            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);

            Concert concert = new Concert(mock.Object) { Start = new DateTime(2020, 10, 10, 19, 30, 00), End = new DateTime(2020, 10, 10, 20, 25, 00) };
            bool expected = true;

            //Act
            bool result = participant.CanMakeBookingAt(concert, mockFestivalManager.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CanMakeBookingAt_ParticipantcannotBookWhenALreadyHaveConcertAtTheSameTime1()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();


            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() {
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 20, 30, 00), End = new DateTime(2020, 10, 10, 21, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 0}
                    }
                 },
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 21, 30, 00), End = new DateTime(2020, 10, 10, 22, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 1}
                    }}
            };
            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);

            Concert concert = new Concert(mock.Object) { Start = new DateTime(2020, 10, 10, 19, 30, 00), End = new DateTime(2020, 10, 10, 20, 31, 00) };
            bool expected = false;

            //Act
            bool result = participant.CanMakeBookingAt(concert, mockFestivalManager.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2020-10-10T20:30:00", "2020-10-10T21:30:00", false)]
        [InlineData("2020-10-10T22:30:00", "2020-10-10T23:30:00", true)]
        [InlineData("2020-10-10T19:30:00", "2020-10-10T23:30:00", false)]
        [InlineData("2020-10-10T20:30:00", "2020-10-10T20:45:00", false)]

        [InlineData("2020-11-10T20:30:00", "2020-11-10T20:45:00", true)]
        [InlineData("2020-10-11T20:30:00", "2020-10-11T20:45:00", true)]

        [InlineData("2020-10-10T22:30:00", "2020-10-10T23:45:00", true)]
        [InlineData("2020-10-10T22:29:59", "2020-10-10T23:45:00", false)]

        [InlineData("2020-10-10T16:30:00", "2020-10-10T20:30:00", true)]
        [InlineData("2020-10-10T16:30:00", "2020-10-10T20:31:00", false)]
        public void CanMakeBookingAt_ParticipantCannotMakeBookingWhenOverlap(string inputStart, string inputEnd, bool expected)
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();

            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() {
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 20, 30, 00), End = new DateTime(2020, 10, 10, 21, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 0}
                    }
                 },
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 21, 30, 00), End = new DateTime(2020, 10, 10, 22, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 1}
                    }}
            };

            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);

            Concert concert = new Concert(mock.Object) { Start = DateTime.Parse(inputStart), End = DateTime.Parse(inputEnd) };

            //Act
            bool result = participant.CanMakeBookingAt(concert, mockFestivalManager.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2020-10-10T21:30:00", "2020-10-10T23:30:00", true)]
        [InlineData("2020-10-10T21:30:00", "2020-10-10T23:31:00", false)]
        [InlineData("2020-10-10T21:29:00", "2020-10-10T23:30:00", false)]

        [InlineData("2020-10-11T00:00:00", "2020-10-11T01:30:00", false)]
        [InlineData("2020-10-11T00:30:00", "2020-10-11T01:30:00", true)]
        public void CanMakeBookingAt_ParticipantCannotMakeBookingWhenOverlap_WhenConcertExpandsOvernight(string inputStart, string inputEnd, bool expected)
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();

            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() {
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 20, 30, 00), End = new DateTime(2020, 10, 10, 21, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 0}
                    }
                 },
                 new Concert(mock.Object) {Start = new DateTime(2020, 10, 10, 23, 30, 00), End = new DateTime(2020, 10, 11, 00, 30, 00),
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 1}
                    }}
            };

            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);

            Concert concert = new Concert(mock.Object) { Start = DateTime.Parse(inputStart), End = DateTime.Parse(inputEnd) };

            //Act
            bool result = participant.CanMakeBookingAt(concert, mockFestivalManager.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetParticipantBookings_FindTheBookings()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();
            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() {
                 new Concert(mock.Object) {
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 0}
                    }
                 },
                 new Concert(mock.Object) {
                    Bookings = new List<Booking>()
                    {
                        new Booking(mock.Object) {Participant = participant, ID = 1}
                    }},
                new Concert(mock.Object) {
                    Bookings = new List<Booking>()}
            };

            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);
            int expected = 2; 

            //Act
            int result = participant.GetParticipantBookings(mockFestivalManager.Object).Count;

            //Assert
            Assert.Equal(expected, result); 
        }


        [Fact]
        public void GetParticipantBookings_FindNoBookings()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            var mockFestivalManager = new Mock<IFestivalManager>();
            Participant participant = new Participant(new Ticket("test"), mock.Object);

            List<Concert> concertsList = new List<Concert>() {
                 new Concert(mock.Object) {
                    Bookings = new List<Booking>()
                    {
                    }
                 },
                 new Concert(mock.Object) {
                    Bookings = new List<Booking>()
                    {
                    }},
                new Concert(mock.Object) {
                    Bookings = new List<Booking>()}
            };

            Calendar calendar = new Calendar(mock.Object);
            calendar.AddConcert(concertsList[0]);
            calendar.AddConcert(concertsList[1]);
            mockFestivalManager.SetupGet(m => m.Calendar).Returns(calendar);
            int expected = 0;

            //Act
            int result = participant.GetParticipantBookings(mockFestivalManager.Object).Count;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
