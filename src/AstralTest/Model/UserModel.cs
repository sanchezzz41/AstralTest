﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Model
{
    public class UserModel
    {
        [Required]
        public string Name { get; set; }
    }
}
