using NorthernWinterBeatLibrary.DataAccess;
using System;
using Xunit;
using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using System.Collections.Generic;

namespace NWB_TESTS
{
    public class UnitTestConcert
    {
        private Concert GetConcertAtMaxCapacity()
        {
            var mock = new Mock<IDataAccess>();
            Venue venue = new Venue(mock.Object);
            venue.Capacity = 1;

            Concert concert = new Concert(mock.Object, null);
            concert.Venue = venue;

            concert.Bookings.Add(new Booking());

            return concert;

        }
        private Concert GetConcertWithSpaceForTwo()
        {
            var mock = new Mock<IDataAccess>();
            Venue venue = new Venue(mock.Object);
            venue.Capacity = 2;

            Concert concert = new Concert(mock.Object, null);
            concert.Venue = venue;

            return concert;

        }



        [Fact]
        public void Concert_BookingsStartEmpty()
        {
            //Arrange
            Concert concert = new Concert();
            int expected = 0;

            //Act 
            int result = concert.Bookings.Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveBooking_RemovesKnownBooking()
        {
            //Arrange
            var dataAccess = new Mock<IDataAccess>();
            Concert concert = new Concert(dataAccess.Object, null);
            Booking booking = new Booking();
            int expected = 0;
            concert.Bookings.Add(booking);

            //Act 
            concert.RemoveBooking(booking); 
            int result = concert.Bookings.Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveBooking_DoesNotRemoveBookingNotInBookings()
        {
            //Arrange
            var dataAccess = new Mock<IDataAccess>();
            Concert concert = new Concert(dataAccess.Object, null);
            Booking booking1 = new Booking();
            Booking booking2 = new Booking();
            int expected = 1;
            concert.Bookings.Add(booking1);

            //Act 
            concert.RemoveBooking(booking2); 
            int result = concert.Bookings.Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
         public void IsAtMaxCapacity_ReturnsTrueWhenAtMaxCapacity()
        {
            //Arrange
            Concert concert = GetConcertAtMaxCapacity();
            bool expected = true;

            //Act
            bool result = concert.IsAtMaxCapacity;

            //Assert
            Assert.Equal(expected, result); 
        }

        [Fact]
        public void IsAtMaxCapacity_ReturnsFalseWhenNotAtMaxCapacity()
        {
            //Arrange
            Concert concert = GetConcertWithSpaceForTwo();
            bool expected = false;

            //Act
            bool result = concert.IsAtMaxCapacity;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsAtMaxCapacity_ReturnsTrueWithNoVenue()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            Concert concert = new Concert(mock.Object, null);
            bool expected = true;

            //Act
            bool result = concert.IsAtMaxCapacity;

            //Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void MakeBooking_CannotMakeBookingAtMaxCapacity()
        {
            //Arrange 
            Concert concert = GetConcertAtMaxCapacity();
            Booking expected = null;
            var mock = new Mock<Participant>();
            mock.Setup(p => p.CanMakeBookingAt(concert)).Returns(true); //Defining other argument in if-statement as always true

            //Act
            Booking result = concert.MakeBooking(mock.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MakeBooking_CanMakeBookingWhenNotAtMaxCapacity()
        {
            //Arrange 
            Concert concert = GetConcertWithSpaceForTwo();
            var mock = new Mock<Participant>();
            mock.Setup(p => p.CanMakeBookingAt(concert)).Returns(true); //Defining other argument in if-statement as always true
            var expected = (mock.Object, concert);

            //Act
            Booking resultBooking = concert.MakeBooking(mock.Object);
            var result = (resultBooking.Participant, resultBooking.Concert);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MakeBooking_CanMakeBookingWhenConcertHasAnotherBooking()
        {
            //Arrange 
            Concert concert = GetConcertWithSpaceForTwo();
            concert.Bookings.Add(new Booking());
            var mock = new Mock<Participant>();
            mock.Setup(p => p.CanMakeBookingAt(concert)).Returns(true); //Defining other argument in if-statement as always true
            var expected = (mock.Object, concert);

            //Act
            Booking resultBooking = concert.MakeBooking(mock.Object);
            var result = (resultBooking.Participant, resultBooking.Concert);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MakeBooking_CannotMakeBookingWhenParticipantCannotMakeBooking()
        {
            //Arrange 
            Concert concert = GetConcertAtMaxCapacity();
            Booking expected = null;
            var mock = new Mock<Participant>();
            mock.Setup(p => p.CanMakeBookingAt(concert)).Returns(false); //Defining other argument in if-statement as always false

            //Act
            Booking result = concert.MakeBooking(mock.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01-02-2020", "01-02")]
        [InlineData("09-12-2020", "09-12")]
        [InlineData("02-06-2020", "02-06")]
        [InlineData("24-12-2020", "24-12")]
        public void FormatDate_ReturnsCorrectFormat(string input, string expected)
        {
            //Arrange
            Concert concert = new Concert();
            DateTime dateTime = DateTime.Parse(input);

            //Act
            string result = concert.FormatDate(dateTime);

            //Assert
            Assert.Equal(expected, result); 
        }


        [Theory]
        [InlineData("2020-01-02T10:14:22", "10:14")]
        [InlineData("2020-01-02T01:01:01", "01:01")]
        [InlineData("2020-01-02T02:02:43", "02:02")]
        [InlineData("2020-01-02T02:43:02", "02:43")]
        public void FormatTime_ReturnsCorrectFormat(string input, string expected)
        {
            //Arrange
            Concert concert = new Concert();
            DateTime dateTime = DateTime.Parse(input);

            //Act
            string result = concert.FormatTime(dateTime);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Update_UpdatesInformation()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(d => d.Retrieve<Venue>()).Returns(new List<Venue>());
            var festivalManager = new FestivalManager(mock.Object);
            Concert NewConcertInfo = new Concert(mock.Object, null)
            {
                Artist = "Bobby",
                ArtistDescription = "Epix",
                Start = DateTime.Parse("2020-02-02"),
                End = DateTime.Parse("2020-03-03"),
                Venue = new Venue(mock.Object) { Name = "Studenterhuset"}
                 
            };

            Venue otherVenue = new Venue(mock.Object) { Name = "1000 Fryd" };
            festivalManager.Calendar.AddVenue(otherVenue);
            Concert otherConcert = new Concert(mock.Object, festivalManager);

            //Act
            otherConcert.Update(NewConcertInfo, otherVenue.Name);

            //Assert
            Assert.Equal(otherConcert.Artist, NewConcertInfo.Artist);
            Assert.Equal(otherConcert.Venue.Name, otherVenue.Name);
            Assert.Equal(otherConcert.Start, NewConcertInfo.Start);
            Assert.Equal(otherConcert.End, NewConcertInfo.End);
            Assert.Equal(otherConcert.ArtistDescription, NewConcertInfo.ArtistDescription);
        }
    }
}
