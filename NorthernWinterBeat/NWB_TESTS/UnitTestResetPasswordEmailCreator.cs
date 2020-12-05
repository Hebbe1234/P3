using Microsoft.Extensions.Configuration;
using Moq;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestResetPasswordEmailCreator
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
        public List<PasswordRequest> GenerateResetPasswordRequests(string resetCode, string email)
        {
            return new List<PasswordRequest>()
            {
                new PasswordRequest(resetCode, email),
                new PasswordRequest(resetCode, email),
                new PasswordRequest(resetCode + "1", "WrongEMail"),
                new PasswordRequest(resetCode + "2", "AnotherWrongEmail")
            };
        }

        [Fact]
        public void ResetCodeGenerator_ReturnsA8CharString()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            IPasswordResetEmailCreator passwordResetEmailCreator = new PasswordResetEmailCreator(mock.Object, GetConfigurationMock());
            int expectedLength = 8;
            //Act
            string result = passwordResetEmailCreator.ResetCodeGenerator();

            //Assert
            Assert.Equal(expectedLength, result.Count());
        }
        [Fact]
        public void ResetCodeGenerator_ReturnsAStringConatingaTo9()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            IPasswordResetEmailCreator passwordResetEmailCreator = new PasswordResetEmailCreator(mock.Object, GetConfigurationMock());
            bool expected = true;
            //Act
            bool result = Regex.IsMatch(passwordResetEmailCreator.ResetCodeGenerator(), "^[a-zA-z1-9_]+$");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreateMail_CorrectlyCreatesAnMail()
        {
            //Arrange
            string EmailReciever = "M@e123.com";
            string EmailFrom = "nwb.reset@gmail.com";
            string ResetCode = "Reset123";
            var mock = new Mock<IDataAccess>();
            mock.Setup(a => a.Retrieve<PasswordRequest>()).Returns(GenerateResetPasswordRequests(ResetCode, EmailReciever));
            
            var test = new Mock<PasswordRequest>();
            
            var passwordResetEmailCreator = new Mock<PasswordResetEmailCreator>(mock.Object, GetConfigurationMock());
            passwordResetEmailCreator.Setup(a => a.SendEmail(It.IsAny<string>()));

            //Act
            var mail = passwordResetEmailCreator.Object.CreateMail(EmailReciever);
            string actualEmailReciever = mail.To.ToString();
            string actualEmailFrom = mail.From.ToString(); 
            //Assert
            Assert.Equal(EmailReciever, actualEmailReciever);
            Assert.Equal(EmailFrom, actualEmailFrom);
        }
    }
}
