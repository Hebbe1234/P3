using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IPasswordEmailCreator
    {
        MailMessage CreateMail(string recipientEmailAddress);
        string ResetCodeGenerator();
        void SendEmail(string recipientEmailAddress);
    }
}
