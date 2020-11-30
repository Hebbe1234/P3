using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private IDataAccess DataAccess { get; set; }
        private IFestivalManager FestivalManager { get; set; }
        public AuthorizationManager(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            DataAccess = dataAccess;
            FestivalManager = festivalManager;
        }

        public ApplicationUser GetUser(string username)
        {
            return DataAccess.Retrieve<ApplicationUser>().Where(u => u.Username == username)?.FirstOrDefault();
        }

        public void CreateParticipantUser(string NameEntered, string EmailEntered, string Password1Entered, string ticketNumber)
        {
            var newUser = new ApplicationUser(EmailEntered, Encrypt(Password1Entered), ApplicationUser.Roles.PARTICIPANT)
            {
                TicketID = ticketNumber
            };

            DataAccess.Add(newUser);
            Participant newParticipant = new Participant(new Ticket(ticketNumber), NameEntered, EmailEntered, DataAccess);
            FestivalManager.AddParticipant(newParticipant);
        }

        public void CreateVenueUser(int id, string Username, string Password1Entered)
        {
            var newUser = new ApplicationUser(Username, Encrypt(Password1Entered), ApplicationUser.Roles.VENUE)
            {
                VenueID = id
            };

            DataAccess.Add(newUser);
        }

        public bool VerifyTicket(string TicketInput)
        {
            bool IsLegalTicket = DataAccess.Retrieve<LegalTicket>().Where(t => t.TicketNumber == TicketInput).ToList().Count() > 0;
            bool DoesTicketNotExist = DataAccess.Retrieve<Ticket>().Where(t => t.TicketNumber == TicketInput).ToList().Count() == 0;
            return IsLegalTicket && DoesTicketNotExist;
        }

        public (ClaimsIdentity, AuthenticationProperties) CreateClaim(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("VenueID", user.VenueID.ToString()),
                new Claim("TicketID", user.TicketID ?? "")
            };


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                
            };

            return (claimsIdentity, authProperties);

        }

        //FROM https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
        public string Encrypt(string password)
        {

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {

                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(password, new byte[]
                {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x76,
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x76,
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x76,
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x76,

                }, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x76,
                    0x61, 0x6e, 0x20, 0x4d, 0x45, 0x32, 0x58, 0x19 });
                var encryptString = System.Text.Encoding.Default.GetString(encrypted);
                //Display the original data and the decrypted data.

                return encryptString;

            }
        }

        //FROM https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0
        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public void SendEmail(string UserEmail, Participant p)
        {
            string SecretCode = SecretCodeGenerator();  

            ResetPasswordRequest NewResetPasswordRequest = new ResetPasswordRequest(SecretCode, UserEmail);
            DataAccess.Add(NewResetPasswordRequest); 

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("nwb.reset@gmail.com");
            mail.To.Add(UserEmail);
            mail.Subject = "Reset your password for NWB";


            mail.Body = "Reset Email. Here is the code " + SecretCode;

            SmtpServer.UseDefaultCredentials = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nwb.reset@gmail.com", "Hejsa12345678");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        } 
        public string SecretCodeGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public void ChangePassword(string SecretCode, string email, string Password)
        {
            var resetPasswordRequest = DataAccess.Retrieve<ResetPasswordRequest>().Find(p => p.Email == email);
            if(resetPasswordRequest.SecretCode == SecretCode && resetPasswordRequest.ExpirationDate > DateTime.Now)
            {
                ApplicationUser p = DataAccess.Retrieve<ApplicationUser>().Find(p => p.Username == email);

                
            }
        }
    }
}

