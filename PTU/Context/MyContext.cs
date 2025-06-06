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
        // Eğer tablo olarak tutacaksan:
         public DbSet<Adliye> Adliyeler { get; set; }
    }
}
