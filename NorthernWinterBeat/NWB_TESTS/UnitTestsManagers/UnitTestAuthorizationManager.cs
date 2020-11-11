using Microsoft.EntityFrameworkCore;
using Moq;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS.UnitTestsManagers
{
    public class UnitTestAuthorizationManager
    {
        [Fact]
        public void VerifyTicket_Test()
        {

            //cant mock DBset<>
        }

        [Fact]
        public void CreateClaim_CreateAClaim()
        {
            //Arrange
            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.PARTICIPANT);
            AuthorizationManager authorizationManager = new AuthorizationManager();
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
            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.ADMIN);
            AuthorizationManager authorizationManager = new AuthorizationManager();
            bool expected = true;

            //Act
            var (claimsIdentity, authProperties) = authorizationManager.CreateClaim(ParticipantUser);
            bool result = claimsIdentity.IsAuthenticated;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
