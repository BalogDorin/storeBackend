﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Model
{
    public class Contact
    {
        public string Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
