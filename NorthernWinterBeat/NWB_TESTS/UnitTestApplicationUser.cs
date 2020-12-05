using Moq;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestApplicationUser
    {
        [Fact]
        public void Update_CheckThatPasswordGetsUpdated()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            ApplicationUser applicationuser = new ApplicationUser("Username", "OldPassword", ApplicationUser.Roles.PARTICIPANT, mock.Object);
            string result = "NewPassword";
            ApplicationUser newApplicationUser = new ApplicationUser("Username", result, ApplicationUser.Roles.PARTICIPANT, mock.Object);

            //Act
            applicationuser.Update(newApplicationUser);
            string expected = applicationuser.Password; 

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
