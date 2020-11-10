using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestCalendar
    {

        private Calendar GetCalendarWithTwoConcertsAndTwoVenues()
        {
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Concert>()).Returns(new List<Concert>()
            {
                new Concert() {ID = 0, Artist = "Bøge", Venue = new Venue(mock.Object) {ID = 2, Name = "MartinsHus"}},
                new Concert() {ID = 1, Artist = "Nielsen"}
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
            Concert concert = new Concert();
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

            Concert concert = new Concert(); 
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
        public void DeleteConcert_DeletesTheGivenConcert()
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
        public void DeleteConcert_DoesNotDeleteANonExisistingConcert()
        {
            //Arrange
            Calendar calendar = GetCalendarWithTwoConcertsAndTwoVenues();
            int expected = 2;
            Concert concert = new Concert();

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
