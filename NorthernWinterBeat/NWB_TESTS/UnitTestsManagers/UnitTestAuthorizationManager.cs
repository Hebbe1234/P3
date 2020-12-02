using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.ObjectPool;
using MockQueryable.Moq;
using Moq;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace NWB_TESTS.UnitTestsManagers
{
    public class TestConfig
    {
        [Required]
        public string EmailPassword { get; set; }
        
    }

    public class UnitTestAuthorizationManager
    {
        
        
        private IConfiguration GetConfigurationMock()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Email:EmailPassword", "1234"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                 .AddInMemoryCollection(inMemorySettings)
                 .Build();

            return configuration;

        }

        [Theory]
        [InlineData("legalTicket1", false)]
        [InlineData("legalTicket2", false)]
        [InlineData("legalTicket3", true)]
        [InlineData("legalTicket4", false)]
        [InlineData("legalTicket5", false)]
        public void VerifyTicket_WorksProperly(string ticket, bool expected)
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            mock.Setup(D => D.Retrieve<LegalTicket>()).Returns(new List<LegalTicket>()
            {
                new LegalTicket("legalTicket1"),
                new LegalTicket("legalTicket2"),
                new LegalTicket("legalTicket3")
            }); ;

            mock.Setup(D => D.Retrieve<LegalTicket>()).Returns(new List<LegalTicket>()
            {
                new LegalTicket("legalTicket1"),
                new LegalTicket("legalTicket2"),
                new LegalTicket("legalTicket3")
            }); ;

            mock.Setup(D => D.Retrieve<Ticket>()).Returns(new List<Ticket>()
            {
                new Ticket("legalTicket1"),
                new Ticket("legalTicket2"),
                new Ticket("legalTicket4")
            }); ;


            

            var mockFestivalManager = new Mock<IFestivalManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());


            //Act
            bool result = authorizationManager.VerifyTicket(ticket);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateClaim_CreateAClaim()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());

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
            var mock = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());

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
            var mock = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());


            string expected = "�o#�H�OH�!ќ ���";

            //Act
            string result = authorizationManager.Encrypt("Hejsa1234");

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
