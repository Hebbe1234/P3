using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class PasswordResetEmailCreator : IPasswordResetEmailCreator
    {
        private IDataAccess DataAccess { get; set; }
        private IConfiguration Configuration { get; }
        public MailMessage Mail { get; set; }
        public SmtpClient SmtpServer { get; set; }
        public PasswordResetEmailCreator(IDataAccess dataAccess, IConfiguration conf)
        {
            DataAccess = dataAccess;
            Configuration = conf;
            Mail = new MailMessage();
            SmtpServer = new SmtpClient("smtp.gmail.com");
        }
        public void CreateMail(string recipientEmailAddress)
        {
            List<ResetPasswordRequest> RPR = DataAccess.Retrieve<ResetPasswordRequest>().FindAll(x => x.Email == recipientEmailAddress);
            foreach (ResetPasswordRequest item in RPR)
            {
                DataAccess.Remove<ResetPasswordRequest>(item);
            }

            string ResetCode = ResetCodeGenerator();
            ResetPasswordRequest NewResetPasswordRequest = new ResetPasswordRequest(ResetCode, recipientEmailAddress);
            DataAccess.Add(NewResetPasswordRequest);



            Mail.From = new MailAddress("nwb.reset@gmail.com");
            Mail.To.Add(recipientEmailAddress);
            Mail.Subject = "Reset your password for NWB";


            Mail.Body = "Hey, \n\nYour reset code is: \n" + ResetCode + "\n\nThe code can only be used for the next 20 minutes. Only the newest code sent works. \nWe recommend you change your password as fast as possible for security reasons.";

            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nwb.reset@gmail.com", Configuration.GetValue<string>("EmailPassword"));
            SmtpServer.EnableSsl = true;
            SendEmail(); 
        }
        public virtual void SendEmail()
        {
            try
            {
                SmtpServer.Send(Mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("The mail didnt send \n" + e.Message);
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
            return new String(stringChars);
        }
    }
}
