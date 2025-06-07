using Microsoft.EntityFrameworkCore;
using PTU.Models;

namespace PTU.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options) { }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<TayinTalebi> TayinTalepleri { get; set; }
       
        public DbSet<Adliye> Adliyeler { get; set; }
        public DbSet<Log> Loglar { get; set; }
    }
}
