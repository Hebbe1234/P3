﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
