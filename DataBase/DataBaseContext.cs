using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataBase.Model;

namespace WebApi.DataBase
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        //entities
        //public DbSet<Contact> Contact { get; set; }
        public DbSet<Produs> Produs { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Poza> Poza { get; set; }
    
    }
}
