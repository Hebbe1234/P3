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
        public ResetPasswordRequest(string _secretCode, string _email)
        {
            SecretCode = _secretCode;
            Email = _email;
            ExpirationDate = DateTime.Now.AddMinutes(20);
        }

        [Key] 
        public int ID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecretCode { get; set; }
        public string Email { get; set; }
    }
}
