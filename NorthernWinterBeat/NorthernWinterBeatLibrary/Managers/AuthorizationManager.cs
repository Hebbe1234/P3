using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {

        private NorthernWinterBeatConcertContext context { get; set; }            

        public AuthorizationManager(NorthernWinterBeatConcertContext _context)
        {
            context = _context;
        }

        public ApplicationUser GetUser(string username)
        {
            return context.ApplicationUser.Where(u => u.Username == username)?.FirstOrDefault();
        }

        public bool VerifyTicket(string TicketInput)
        {
            bool IsLegalTicket = context.LegalTickets.Find(TicketInput)?.TicketNumber == TicketInput;
            bool DoesTicketNotExist = context.Ticket.Where(t => t.TicketNumber == TicketInput).ToList().Count() == 0;
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
    }
}

