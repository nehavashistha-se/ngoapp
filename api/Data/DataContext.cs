using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext:DbContext
    {
         public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
        } 
        public DbSet<Appusers> Users { get; set; }
          public DbSet<AppUserDetail> UsersDetail { get; set; }
           public DbSet<AppUserDetailHistory> UsersDetailHistory { get; set; }
    }
}