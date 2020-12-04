using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestFestivalManager
    {

        [Fact]
        public void GetParticipants_ReturnsTheCorrectNumberOfParticipants()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Participant>()).Returns(new List<Participant>()
            {
                new Participant(new Ticket("1"), mock.Object),
                new Participant(new Ticket("1"), mock.Object),
                new Participant(new Ticket("1"), mock.Object),
                new Participant(new Ticket("1"), mock.Object)
            });
            int expected = 4;

            FestivalManager festivalManager = new FestivalManager(mock.Object);

            //Act
            int result = festivalManager.GetParticipants().Count;

            //Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void AddParticipant_AddsOneParticipant()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<Participant>()).Returns(new List<Participant>());
            FestivalManager festivalManager = new FestivalManager(mock.Object);
            Participant p1 = new Participant(new Ticket("1"), mock.Object);
            int expected = 1;

            //Act
            festivalManager.AddParticipant(p1);
            int result = festivalManager.GetParticipants().Count;

            //Assert
            Assert.Equal(expected, result); 
        }
    }
}
