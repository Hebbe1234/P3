using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.ObjectPool;
using MockQueryable.Moq;
using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
        public List<ResetPasswordRequest> GenerateResetPasswordRequests(string resetCode, string email)
        {
            return new List<ResetPasswordRequest>()
            {
                new ResetPasswordRequest(resetCode, email),
                new ResetPasswordRequest(resetCode, email),
                new ResetPasswordRequest(resetCode + "1", "WrongEMail"),
                new ResetPasswordRequest(resetCode + "2", "AnotherWrongEmail")
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
            bool result = passwordResetEmailCreator.ResetCodeGenerator().All(c => (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9' || c == '_'));

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
            mock.Setup(a => a.Retrieve<ResetPasswordRequest>()).Returns(GenerateResetPasswordRequests(ResetCode, EmailReciever));
            var test = new Mock<ResetPasswordRequest>();
            var passwordResetEmailCreator = new Mock<PasswordResetEmailCreator>(mock.Object, GetConfigurationMock());
            passwordResetEmailCreator.Setup(a => a.SendEmail());
            passwordResetEmailCreator.Object.Mail = new MailMessage(); 
            passwordResetEmailCreator.Object.SmtpServer = new SmtpClient("smtp.gmail.com");

            //Act
            passwordResetEmailCreator.Object.CreateMail(EmailReciever);
            string actualEmailReciever = passwordResetEmailCreator.Object.Mail.To.ToString();
            string actualEmailFrom = passwordResetEmailCreator.Object.Mail.From.ToString(); 
            //Assert
            Assert.Equal(EmailReciever, actualEmailReciever);
            Assert.Equal(EmailFrom, actualEmailFrom);
        }
        //[Fact]
        //public void CreateMail_RemovesResetPasswordRequests()
        //{
        //    //Arrange
        //    string EmailReciever = "M@e123.com";
        //    string EmailFrom = "nwb.reset@gmail.com";
        //    string ResetCode = "Reset123";
        //    int times = 2;
        //    int count = 0;
        //    var mock = new Mock<IDataAccess>();
        //    mock.Setup(a => a.Retrieve<ResetPasswordRequest>()).Returns(GenerateResetPasswordRequests(ResetCode, EmailReciever));
        //    var test = new Mock<ResetPasswordRequest>();
        //    mock.Setup(a => a.Remove<ResetPasswordRequest>(test.Object)).Callback(() =>
        //    {
        //        count++;
        //    });
        //    var passwordResetEmailCreator = new Mock<PasswordResetEmailCreator>(mock.Object, GetConfigurationMock());
        //    passwordResetEmailCreator.Setup(a => a.SendEmail());
        //    passwordResetEmailCreator.Object.Mail = new MailMessage();
        //    passwordResetEmailCreator.Object.SmtpServer = new SmtpClient("smtp.gmail.com");

        //    //Act
        //    passwordResetEmailCreator.Object.CreateMail(EmailReciever);
        //    //Assert
        //    Assert.Equal(times, count);
        //}
    }
}
