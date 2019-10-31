using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Engine.Model
{
    public class Produs
    {
        public string Id { get; set; }
        public double Pret { get; set; }
        public double? PretRedus { get; set; }
        public string Descriere { get; set; }
        public string Marime { get; set; }
        public string Titlu { get; set; }
        public string Culoare { get; set; }
        public string Categorie { get; set; }
        public string CaleFolder { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
