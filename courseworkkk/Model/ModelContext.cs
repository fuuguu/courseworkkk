using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace courseworkkk.Model
{
    [Keyless]
    public class ModelContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Salesman> Salesman { get; set; } = null;
        public DbSet<Cars> Cars { get; set; } = null;
        public DbSet<User> User { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WEBWorkshop.db");
        }
    }
}