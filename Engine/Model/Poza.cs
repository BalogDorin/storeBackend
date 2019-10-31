using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Engine.Model
{
    public class Poza
    {
        public string Id { get; set; }
        public string IdProdus { get; set; }
        public string CalePoza { get; set; }
        public bool IsProduct { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}
