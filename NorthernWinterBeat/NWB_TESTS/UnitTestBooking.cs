using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestBooking
    {
        [Fact]
        public void Disable_CheckABookingGetsDisabled()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            Booking booking = new Booking(mock.Object);
            Booking.BookingState result = Booking.BookingState.INACTIVE;

            //Act
            booking.Disable();
            Booking.BookingState expected = booking.State;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
