using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestCalendar
    {

        private Concert GetEmptyConcert()
        {

            var mockDataAccess = new Mock<IDataAccess>();
            return new Concert(mockDataAccess.Object);
        }

        private Calendar GetCalendarWithTwoConcertsAndTwoVenues()
        {
            var mock = new Mock<IDataAccess>();
            var mockDataAccess = new Mock<IDataAccess>();

            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>()
            {
                new Concert(mockDataAccess.Object) {ID = 0, Artist = "Bøge", Venue = new Venue(mock.Object) {ID = 2, Name = "MartinsHus"}},
                new Concert(mockDataAccess.Object) {ID = 1, Artist = "Nielsen"}
            });
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>()
            {
                new Venue(mock.Object) {ID = 2, Name = "MartinsHus"},
                new Venue(mock.Object) {ID = 3, Name = "JakobsHus"}
            });
            Calendar calendar = new Calendar(mock.Object);
            return calendar;
        }





        [Fact]
        public void AddConcert_AddsAConcertToACalendar()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns( new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns( new List<Venue>());

            Calendar calendar = new Calendar(mock.Object);

            Concert concert = GetEmptyConcert();
            int expected = 1;

            //Act
            calendar.AddConcert(concert);
            int result = calendar.GetConcerts().Count;

            //Assert
            Assert.Equal(expected, result); 
        }

        [Fact]
        public void AddConcert_AddsTheCorrectVenueBasedOnGivenVenueName()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(
                new List<Venue>() 
                {
                    new Venue(mock.Object) {Name = "test1" },
                    new Venue(mock.Object) {Name = "test2" }
                });

            Concert concert = GetEmptyConcert();
            Calendar calendar = new Calendar(mock.Object);
            string expected = "test1";

            //Act
            calendar.AddConcert(concert, "test1");
            string result = calendar.GetConcerts()[0].Venue.Name;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddVenue_AddsAVenueToACalendar()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>());
            mock.Setup(D => D.Retrieve<Venue>()).Returns(new List<Venue>());

            Calendar calendar = new Calendar(mock.Object);
            Venue venue = new Venue(mock.Object);
            int expected = 1;

            //Act
            calendar.AddVenue(venue);
            int result = calendar.GetVenues().Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetConcert_ReturnsTheCorrectConcertBasedOnID()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 1; 

            //Act
            Concert concertResult = calendar.GetConcert(1);
            int result = concertResult.ID;

            //Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void GetVenue_ReturnsTheCorrectVenueBasedOnID()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 3;

            //Act
            Venue venueResult = calendar.GetVenue(3);
            int result = venueResult.ID;

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void GetConcertsAtVenue_ReturnsTheCorrectVenues()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 1;

            //Act
            int result = calendar.GetConcertsAtVenue(2).Count;

            //Assert
            Assert.Equal(expected, result); 
        }

        [Fact]
        public void DeleteConcert_DeletesAConcert()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 1;
            Concert concert = calendar.GetConcerts()[0]; 

            //Act
            calendar.DeleteConcert(concert);
            int result = calendar.GetConcerts().Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteConcert_DeletesTheGivenConcert()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            bool expected = false;
            Concert concert = calendar.GetConcerts().First();

            //Act
            calendar.DeleteConcert(concert);
            bool result = calendar.GetConcerts().Contains(concert);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteConcert_DoesNotDeleteANonExistingConcert()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 2;
            var mockContext = new Mock<NorthernWinterBeatConcertContext>();
            Concert concert = new Concert(mockContext.Object);

            //Act
            calendar.DeleteConcert(concert);
            int result = calendar.GetConcerts().Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteVenue_DeletesTheGivenVenue()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 1;
            Venue venue = calendar.GetVenues()[0];

            //Act
            calendar.DeleteVenue(venue);
            int result = calendar.GetVenues().Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteVenue_DoesNotDeleteNotExisitingVenue()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 2;
            Venue venue = new Venue(mock.Object); 

            //Act
            calendar.DeleteVenue(venue);
            int result = calendar.GetVenues().Count;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
