using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeat.Pages.Admin;
using NorthernWinterBeat.Pages.Admin.ParticipantAdmin;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestRazorpageCalendar
    {
        public List<Concert> GenerateConcerts()
        {
            var mock = new Mock<IDataAccess>();
            return new List<Concert>()
            {
                new Concert(new DateTime(2020, 01, 10), new DateTime(2020, 01, 10), "4Artist", "1ArtistDescrition", mock.Object){ID = 0, Venue = new Venue("a", "address", 5, mock.Object),  Bookings = new List<Booking>() { new Booking(mock.Object), new Booking(mock.Object)} },
                new Concert(new DateTime(2020, 01, 10), new DateTime(2020, 01, 10), "5Artist", "2ArtistDescrition", mock.Object){ID = 1, Venue = new Venue("d", "address", 10, mock.Object), Bookings = new List<Booking>() { new Booking(mock.Object), new Booking(mock.Object), new Booking(mock.Object), new Booking(mock.Object) } },
                new Concert(new DateTime(2020, 01, 12), new DateTime(2020, 01, 10), "1Artist", "3ArtistDescrition", mock.Object){ID = 2, Venue = new Venue("c", "address", 55, mock.Object), Bookings = new List<Booking>() { new Booking(mock.Object), new Booking(mock.Object), new Booking(mock.Object) } },
                new Concert(new DateTime(2020, 01, 12), new DateTime(2020, 01, 10), "3Artist", "4ArtistDescrition", mock.Object){ID = 3, Venue = new Venue("b", "address", 1, mock.Object)},
                new Concert(new DateTime(2020, 01, 14), new DateTime(2020, 01, 10), "2Artist", "5ArtistDescrition", mock.Object){ID = 4, Venue = new Venue("e", "address", 55, mock.Object), Bookings = new List<Booking>() { new Booking(mock.Object)} }
            };
        }

        [Fact]
        public void SortConcertsBy_CorrectlySortByArtist()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockDataAcces = new Mock<IDataAccess>(); 
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            CalendarModel Model = new CalendarModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Artist"
            };

            //Act
            var resultConcerts = Model.SortConcertBy(Model.Concerts);

            //Assert
            Assert.Equal("1Artist", resultConcerts[0].Artist);
            Assert.Equal("2Artist", resultConcerts[1].Artist);
            Assert.Equal("3Artist", resultConcerts[2].Artist);
            Assert.Equal("4Artist", resultConcerts[3].Artist);
            Assert.Equal("5Artist", resultConcerts[4].Artist);
        }
        [Fact]
        public void SortConcertsBy_CorrectlySortByTime()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockDataAcces = new Mock<IDataAccess>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            CalendarModel Model = new CalendarModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Time"
            };
            Model.Concerts.Reverse();

            //Act
            var resultConcerts = Model.SortConcertBy(Model.Concerts);

            //Assert
            Assert.Equal(0, resultConcerts[0].ID);
            Assert.Equal(1, resultConcerts[1].ID);
            Assert.Equal(2, resultConcerts[2].ID);
            Assert.Equal(3, resultConcerts[3].ID);
            Assert.Equal(4, resultConcerts[4].ID);
        }

        [Fact]
        public void SortConcertsBy_CorrectlySortByVenue()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockDataAcces = new Mock<IDataAccess>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            CalendarModel Model = new CalendarModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Venue"
            };
            Model.Concerts.Reverse();

            //Act
            var resultConcerts = Model.SortConcertBy(Model.Concerts);


            //Assert
            Assert.Equal(0, resultConcerts[0].ID);
            Assert.Equal(3, resultConcerts[1].ID);
            Assert.Equal(2, resultConcerts[2].ID);
            Assert.Equal(1, resultConcerts[3].ID);
            Assert.Equal(4, resultConcerts[4].ID);
        }
        [Fact]
        public void SortConcertsBy_CorrectlySortByCapacity()
        {
            //Arrange
            var mockFestivalManager = new Mock<IFestivalManager>();
            var mockDataAcces = new Mock<IDataAccess>();
            mockFestivalManager.Setup(f => f.Calendar).Returns(new Calendar(mockDataAcces.Object));
            CalendarModel Model = new CalendarModel(mockFestivalManager.Object)
            {
                Concerts = GenerateConcerts(),
                SortBy = "Capacity"
            };

            //Act
            var resultConcerts = Model.SortConcertBy(Model.Concerts);


            //Assert
            Assert.Equal(3, resultConcerts[0].ID);
            Assert.Equal(0, resultConcerts[1].ID);
            Assert.Equal(1, resultConcerts[2].ID);
            Assert.Equal(2, resultConcerts[3].ID);
            Assert.Equal(4, resultConcerts[4].ID);
        }
    }
}

