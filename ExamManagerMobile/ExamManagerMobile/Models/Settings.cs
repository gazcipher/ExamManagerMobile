﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamManagerMobile.Models
{
    public class Settings
    {
        [PrimaryKey]
        public int Id { get; set; }
        public bool Switch1 { get; set; }
        public bool Switch2 { get; set; }
    }
}
