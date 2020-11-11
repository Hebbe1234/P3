using Microsoft.EntityFrameworkCore;
using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS.UnitTestsManagers
{
    public class UnitTestDatabaseManager
    {

        [Fact]
        public void GetUser_ReturnsCorrectUser()
        {
            //Arrange
            var mock = new Mock<DbSet<ApplicationUser>>();


            //DbSet<ApplicationUser> DbSett = new DbSet<ApplicationUser>();
            var mocked = new Mock<NorthernWinterBeatConcertContext>();
            mocked.Setup(c => c.ApplicationUser).Returns(mock.Object);

            //Act

            //Assert
            Assert.True(false);

        }
    }
}
