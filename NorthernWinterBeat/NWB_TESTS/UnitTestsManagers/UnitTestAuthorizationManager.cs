using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using MockQueryable.Moq;
using Moq;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NWB_TESTS.UnitTestsManagers
{
    public class UnitTestAuthorizationManager
    {
        [Theory]
        [InlineData("legalTicket1", false)]
        [InlineData("legalTicket2", false)]
        [InlineData("legalTicket3", true)]
        [InlineData("legalTicket4", false)]
        [InlineData("legalTicket5", false)]
        public void VerifyTicket_WorksProperly(string ticket, bool expected)
        {
            //Arrange
            List<LegalTicket> empty = new List<LegalTicket>();
            empty.Add(new LegalTicket("legalTicket1"));
            empty.Add(new LegalTicket("legalTicket2"));
            empty.Add(new LegalTicket("legalTicket3"));
            var mockLegalTicketDbSet = empty.AsQueryable().BuildMockDbSet();
                // stackoverflow: spicy one-liner with mini-implementation of .Find
            mockLegalTicketDbSet.Setup(
                m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(
                    ids => empty.FirstOrDefault(
                        l => l.TicketNumber == (string)ids[0])
                );


            List<Ticket> notEmpty = new List<Ticket>();
            notEmpty.Add(new Ticket("legalTicket1"));
            notEmpty.Add(new Ticket("legalTicket2"));
            notEmpty.Add(new Ticket("legalTicket4"));
            var mockTicketDbSet = notEmpty.AsQueryable().BuildMockDbSet();



            var mock = new Mock<IDatabaseManager>();
            var mockContext = new Mock<NorthernWinterBeatConcertContext>();
            mockContext.SetupGet(c => c.LegalTickets).Returns(mockLegalTicketDbSet.Object);
            mockContext.SetupGet(c => c.Ticket).Returns(mockTicketDbSet.Object);
            mock.SetupGet(d => d.context).Returns(mockContext.Object);
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object);


            //Act
            bool result = authorizationManager.VerifyTicket(ticket);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateClaim_CreateAClaim()
        {
            //Arrange
            var mock = new Mock<IDatabaseManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object);

            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.PARTICIPANT);
           
            bool expected = true; 

            //Act
            var (claimsIdentity, authProperties) = authorizationManager.CreateClaim(ParticipantUser);
            bool  result = claimsIdentity.IsAuthenticated;

            //Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void CreateClaim_CreateAClaimAdmin()
        {
            //Arrange

            var mock = new Mock<IDatabaseManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object);
            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.ADMIN);
            bool expected = true;

            //Act
            var (claimsIdentity, authProperties) = authorizationManager.CreateClaim(ParticipantUser);
            bool result = claimsIdentity.IsAuthenticated;

            //Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Encrypt_EncryptsProperly()
        {
            //Arrange
            var mock = new Mock<IDatabaseManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object);

            string expected = "�o#�H�OH�!ќ ���";

            //Act
            string result = authorizationManager.Encrypt("Hejsa1234");

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
