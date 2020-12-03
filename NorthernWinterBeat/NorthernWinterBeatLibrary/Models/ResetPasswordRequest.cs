using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class ResetPasswordRequest
    {

        public ResetPasswordRequest()
        {
        }
        public ResetPasswordRequest(string _resetCode, string _email)
        {
            ResetCode = _resetCode;
            Email = _email;
            ExpirationDate = DateTime.Now.AddMinutes(20);
        }

        [Key] 
        public int ID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ResetCode { get; set; }
        public string Email { get; set; }
    }
}
