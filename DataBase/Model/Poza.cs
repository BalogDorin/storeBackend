using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataBase.Model
{
    public class Poza
    {
        public Guid Id { get; set; }
        public Guid IdProdus { get; set; }
        public string CalePoza { get; set; }
        public bool IsProduct { get; set; }
        //public List<IFormFile> Files { get; set; }

    }
}
