﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseworkkk.Model
{
    public class User
    {
        [Key]
        public int id_user { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
