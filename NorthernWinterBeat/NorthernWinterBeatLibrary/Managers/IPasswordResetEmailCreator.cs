using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IPasswordResetEmailCreator
    {
        public void CreateMail(string recipientEmailAddress);
        public string ResetCodeGenerator(); 
    }
}
