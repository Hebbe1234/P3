using Moq;
using NorthernWinterBeat.Pages.Admin.ParticipantAdmin;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestParticipantOverview
    //public Participant(Ticket _ticket, string name, string email, IDataAccess dataAccess) : this(_ticket, dataAccess)
    {

        public List<Ticket> Generate4Tickets()
        {
            return new List<Ticket>()
            {
                new Ticket("1") {ID = 1},
                new Ticket("2") {ID = 2},
                new Ticket("3") {ID = 3},
                new Ticket("4") {ID = 4}
            };
        }
        [Fact]
        public void SortParticipants_SortParticipantsCorrectlyBasedOnName()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestival = new Mock<IFestivalManager>();
            var Tickets = Generate4Tickets();
            List<Participant> ListParticipant = new List<Participant>()
            {
                new Participant(Tickets[0], "2Name", "3Email", mock.Object) {ID =0 },
                new Participant(Tickets[1], "1Name", "2Email", mock.Object) {ID =1 },
                new Participant(Tickets[2], "3Name", "1Email", mock.Object) {ID =2 },
                new Participant(Tickets[3], "1Name", "4Email", mock.Object) {ID =3 }
            };
            ParticipantOverviewModel Model = new ParticipantOverviewModel(mockFestival.Object) { SortBy = "Name" };

            List<Participant> expected = new List<Participant>()
            {
                new Participant(Tickets[1], "1Name", "2Email", mock.Object){ID =1 },
                new Participant(Tickets[3], "1Name", "4Email", mock.Object){ID =3 },
                new Participant(Tickets[0], "2Name", "3Email", mock.Object){ID =0 },
                new Participant(Tickets[2], "3Name", "1Email", mock.Object){ID =2 }
            };

            //Act
            List<Participant> result = Model.SortParticipants(ListParticipant);

            //Assert
            Assert.Equal(expected[0].ID, result[0].ID);
            Assert.Equal(expected[1].ID, result[1].ID);
            Assert.Equal(expected[2].ID, result[2].ID);
            Assert.Equal(expected[3].ID, result[3].ID);
        }
        [Fact]
        public void SortParticipants_SortParticipantsCorrectlyBasedOnTicketNr()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestival = new Mock<IFestivalManager>();
            var Tickets = Generate4Tickets();
            List<Participant> ListParticipant = new List<Participant>()
            {
                new Participant(Tickets[3], "1Name", "4Email", mock.Object) {ID =3 },
                new Participant(Tickets[1], "1Name", "2Email", mock.Object) {ID =1 },
                new Participant(Tickets[0], "2Name", "3Email", mock.Object) {ID =0 },
                new Participant(Tickets[2], "3Name", "1Email", mock.Object) {ID =2 }
            };
            ParticipantOverviewModel Model = new ParticipantOverviewModel(mockFestival.Object) { SortBy = "TicketNr" };

            List<Participant> expected = new List<Participant>()
            {
                new Participant(Tickets[0], "2Name", "3Email", mock.Object) {ID =0 },
                new Participant(Tickets[1], "1Name", "2Email", mock.Object) {ID =1 },
                new Participant(Tickets[2], "3Name", "1Email", mock.Object) {ID =2 },
                new Participant(Tickets[3], "1Name", "4Email", mock.Object) {ID =3 }
            };

            //Act
            List<Participant> result = Model.SortParticipants(ListParticipant);

            //Assert
            Assert.Equal(expected[0].ID, result[0].ID);
            Assert.Equal(expected[1].ID, result[1].ID);
            Assert.Equal(expected[2].ID, result[2].ID);
            Assert.Equal(expected[3].ID, result[3].ID);
        }
        [Fact]
        public void SortParticipants_SortParticipantsCorrectlyBasedOnEmail()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestival = new Mock<IFestivalManager>();
            var Tickets = Generate4Tickets();
            List<Participant> ListParticipant = new List<Participant>()
            {
                new Participant(Tickets[3], "1Name", "4Email", mock.Object) {ID =3 },
                new Participant(Tickets[1], "1Name", "2Email", mock.Object) {ID =1 },
                new Participant(Tickets[0], "2Name", "3Email", mock.Object) {ID =0 },
                new Participant(Tickets[2], "3Name", "1Email", mock.Object) {ID =2 }
            };
            ParticipantOverviewModel Model = new ParticipantOverviewModel(mockFestival.Object) { SortBy = "E-mail" };

            List<Participant> expected = new List<Participant>()
            {
                new Participant(Tickets[2], "3Name", "1Email", mock.Object) {ID =2 },
                new Participant(Tickets[1], "1Name", "2Email", mock.Object) {ID =1 },
                new Participant(Tickets[0], "2Name", "3Email", mock.Object) {ID =0 },
                new Participant(Tickets[3], "1Name", "4Email", mock.Object) {ID =3 }
            };

            //Act
            List<Participant> result = Model.SortParticipants(ListParticipant);

            //Assert
            Assert.Equal(expected[0].ID, result[0].ID);
            Assert.Equal(expected[1].ID, result[1].ID);
            Assert.Equal(expected[2].ID, result[2].ID);
            Assert.Equal(expected[3].ID, result[3].ID);
        }
    }
}
