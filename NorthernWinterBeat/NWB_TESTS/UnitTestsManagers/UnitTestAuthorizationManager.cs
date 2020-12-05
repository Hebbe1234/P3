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
using System.Net.Mail;
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

            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.PARTICIPANT, mock.Object);

            bool expected = true;

            //Act
            var (claimsIdentity, authProperties) = authorizationManager.CreateClaim(ParticipantUser);
            bool result = claimsIdentity.IsAuthenticated;

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

            ApplicationUser ParticipantUser = new ApplicationUser("martin123", "Hejsa1234", ApplicationUser.Roles.ADMIN, mock.Object);
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

        [Fact]
        public void Encrypt_ThrowsArgumentNullException_WhenPasswordIsNull()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());

            //Assert
            Assert.Throws<ArgumentNullException>(() => authorizationManager.Encrypt(null));
        }

        [Fact]
        public void Encrypt_ThrowsArgumentNullException_WhenPasswordIsEmpty()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());

            //Assert
            Assert.Throws<ArgumentNullException>(() => authorizationManager.Encrypt(""));
        }

        [Fact]
        public void ChangePassword_NoResetPasswordRequest()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            string resetCode = "123Reset";
            string email = "Email@123";
            string newPassword = "Password";
            mock.Setup(D => D.Retrieve<PasswordRequest>()).Returns(new List<PasswordRequest>());
            mock.Setup(D => D.Retrieve<ApplicationUser>()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser(email, "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("More Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
            });
            var mockFestivalManager = new Mock<IFestivalManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            bool expected = false;

            //Act
            bool result = authorizationManager.ChangePassword(resetCode, email, newPassword);

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void ChangePassword_WrongResetCode()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            string resetCode = "123Reset";
            string WrongResetCode = "WrongCode";
            string email = "Email@123";
            string newPassword = "Password";
            mock.Setup(D => D.Retrieve<PasswordRequest>()).Returns(new List<PasswordRequest>()
            {
                new PasswordRequest(resetCode, email),
                new PasswordRequest(resetCode + "1", "WrongEMail"),
                new PasswordRequest(resetCode + "2", "AnotherWrongEmail")

            });
            mock.Setup(D => D.Retrieve<ApplicationUser>()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser(email, "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("More Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
            });
            var mockFestivalManager = new Mock<IFestivalManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            bool expected = false;

            //Act
            bool result = authorizationManager.ChangePassword(WrongResetCode, email, newPassword);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ChangePassword_ExpiredResetPasswordRequest()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            string resetCode = "123Reset";
            string email = "Email@123";
            string newPassword = "Password";
            mock.Setup(D => D.Retrieve<PasswordRequest>()).Returns(new List<PasswordRequest>()
            {
                new PasswordRequest(resetCode, email) { ExpirationDate = DateTime.Now.AddHours(-1)},
                new PasswordRequest(resetCode + "1", "WrongEMail"),
                new PasswordRequest(resetCode + "2", "AnotherWrongEmail")

            });
            mock.Setup(D => D.Retrieve<ApplicationUser>()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser(email, "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("Wrong@Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("More@WrongEmail", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
            });
            var mockFestivalManager = new Mock<IFestivalManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            bool expected = false;

            //Act
            bool result = authorizationManager.ChangePassword(resetCode, email, newPassword);

            //Assert
            Assert.Equal(expected, result);
        }

        public List<ApplicationUser> GenerateApplicationUsers(string email)
        {
            var mock = new Mock<IDataAccess>();
            return new List<ApplicationUser>()
            {
                new ApplicationUser(email, "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                new ApplicationUser("More Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object)
            };
        }


        [Fact]
        public void ChangePassword_CorrectlyChangesPassword()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            string resetCode = "123Reset";
            string email = "Email@123";
            string newPassword = "Password";
            mock.Setup(D => D.Retrieve<PasswordRequest>()).Returns(new List<PasswordRequest>()
            {
                new PasswordRequest(resetCode, email),
                new PasswordRequest(resetCode + "1", "WrongEMail"),
                new PasswordRequest(resetCode + "2", "AnotherWrongEmail")
            });
            var ApplicationsUsers = GenerateApplicationUsers(email);
            mock.Setup(D => D.Retrieve<ApplicationUser>()).Returns(ApplicationsUsers);
            var mockFestivalManager = new Mock<IFestivalManager>();
            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            string expectedPassword = authorizationManager.Encrypt(newPassword);

            //Act
            authorizationManager.ChangePassword(resetCode, email, newPassword);
            string actualPassword = ApplicationsUsers[0].Password;

            //Assert
            Assert.Equal(expectedPassword, actualPassword);
        }

        [Fact()]
        public void GetUser_ReturnsCorrectUser()
        {

            var mock = new Mock<IDataAccess>();
            var realUser = new ApplicationUser("Email@123", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object);


            mock.Setup(D => D.Retrieve<ApplicationUser>()).Returns(
                new List<ApplicationUser>()
                {
                    realUser,
                    new ApplicationUser("Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object),
                    new ApplicationUser("More Wrong Email", "Password", ApplicationUser.Roles.PARTICIPANT, mock.Object)
                }
            );

            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());

            var res = authorizationManager.GetUser(realUser.Username);

            Assert.Equal(realUser, res);

        }

        [Fact]
        public void CreateParticipantUser_CreatesAUserAndParticipant()
        {
            var mock = new Mock<IDataAccess>();

            var name = "name";
            var email = "email";
            var ticketnumber = "ticket";
            var password = "password";

            List<Participant> participants = new List<Participant>();

            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            authorizationManager.CreateParticipantUser(name, email, "password", ticketnumber);

            mockFestivalManager.Verify(f => f.AddParticipant(It.Is<Participant>(p => 
                p.Email == email && 
                p.Name == name && 
                p.Ticket.TicketNumber == ticketnumber
            )));
            
            mock.Verify(d => d.Add(It.Is<ApplicationUser>(a => 
                a.Username == email && 
                a.Password == authorizationManager.Encrypt(password) &&
                a.Role == ApplicationUser.Roles.PARTICIPANT &&
                a.TicketID == ticketnumber
            )));

        }

        [Fact()]
        public void CreateVenueUser_CreatesAUser()
        {
            var mock = new Mock<IDataAccess>();

            int id = 1;
            string name = "name";
            string password = "password";

            List<Participant> participants = new List<Participant>();

            var mockFestivalManager = new Mock<IFestivalManager>();

            AuthorizationManager authorizationManager = new AuthorizationManager(mock.Object, mockFestivalManager.Object, GetConfigurationMock());
            authorizationManager.CreateVenueUser(id, name, "password");

            mock.Verify(d => d.Add(It.Is<ApplicationUser>(a => 
                a.Username == name && 
                a.Password == authorizationManager.Encrypt(password) && 
                a.Role == ApplicationUser.Roles.VENUE &&
                a.VenueID == id
            )));

        }
    }
}
