using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
