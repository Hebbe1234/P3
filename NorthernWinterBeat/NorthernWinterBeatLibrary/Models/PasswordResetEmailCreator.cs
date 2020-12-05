using Microsoft.Extensions.Configuration;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace NorthernWinterBeatLibrary.Models
{
    public class PasswordResetEmailCreator : IPasswordResetEmailCreator
    {
        private IDataAccess DataAccess { get; set; }
        private IConfiguration Configuration { get; }
        public PasswordResetEmailCreator(IDataAccess dataAccess, IConfiguration conf)
        {
            DataAccess = dataAccess;
            Configuration = conf;
        }
        public virtual void SendEmail(string recipientEmailAddress)
        {
            try
            {
                RemovePreviousRequests(recipientEmailAddress);

                var SmtpServer = new SmtpClient("smtp.gmail.com")
                {
                    UseDefaultCredentials = true,
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("nwb.reset@gmail.com", Configuration.GetValue<string>("EmailPassword")),
                    EnableSsl = true
                };

                SmtpServer.Send(CreateMail(recipientEmailAddress));
            }
            catch (Exception e)
            {
                Console.WriteLine("The mail didnt send \n" + e.Message);
            }
        }

        public MailMessage CreateMail(string recipientEmailAddress)
        {
            var Mail = new MailMessage
            {
                From = new MailAddress("nwb.reset@gmail.com"),
                Subject = "Reset your password for NWB"
            };
            Mail.To.Add(recipientEmailAddress);

            var resetCode = CreateResetCode(recipientEmailAddress);
            Mail.Body = "Hey, \n\nYour reset code is: \n" + resetCode + "\n\nThe code can only be used for the next 20 minutes. Only the newest code sent works. \nWe recommend you change your password as fast as possible for security reasons.";

            return Mail;
        }

        private string CreateResetCode(string recipientEmailAddress)
        {
            string ResetCode = ResetCodeGenerator();
            PasswordRequest NewPasswordRequest = new PasswordRequest(ResetCode, recipientEmailAddress);
            DataAccess.Add(NewPasswordRequest);
            return ResetCode;
        }

        private void RemovePreviousRequests(string recipientEmailAddress)
        {
            List<PasswordRequest> RPR = DataAccess.Retrieve<PasswordRequest>().FindAll(x => x.Email == recipientEmailAddress);
            foreach (PasswordRequest item in RPR)
            {
                DataAccess.Remove(item);
            }
        }

        public string ResetCodeGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }
    }
}
