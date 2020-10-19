using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NorthernWinterBeat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            string TicketInput = Request.Form["TicketEntered"];
            string EmailInput = Request.Form["EmailEntered"];
            string PasswordInput = Request.Form["PasswordEntered"];
            Encrypt(PasswordInput);
            //Her testes hvorvidt en billet er indtastet, og valideringen skal ske her. 
            if (TicketInput != "")
            {
                return RedirectToPage("./MakeUserLogin");
            }

            //Her ender vi når en billet ikke er indtastet, hvilket betyder der skal logges ind. 
            if (PasswordInput == "Admin")
            {
                return RedirectToPage("./Admin/IndexAdmin");
            }
            else if (PasswordInput == "Participant")
            {
                return RedirectToPage("./ParticipantRazor/ParticipantConcertOverview");
            }
            else if (PasswordInput == "Venue")
            {
                return RedirectToPage("./Venue/IndexVenue");
            }

            //Her endes der hvis der er indtastet noget forkert eller intet, derfor reloades der. 
            return RedirectToPage("./Index");

        }

        public void Encrypt(string password)
        {

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {

                // Encrypt the string to an array of bytes.
                Console.WriteLine("Length:   {0}", myAes.Key.Length);

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
                Console.WriteLine("Key and IV:   {0} and {1}", Encoding.Default.GetString(myAes.Key), Encoding.Default.GetString(myAes.IV));

                Console.WriteLine("Original:   {0}", password);
                Console.WriteLine("Encrypted:   {0}", encryptString);

            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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
