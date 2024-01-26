using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Data.EntityMap;
using WhiteHat.Models;

namespace WhiteHat.Data.WhiteHatDbContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
                
        }
        public ApplicationDbContext()
        {

        }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserInfoMap());
            ApplicationDbConfiguration.Seed(modelBuilder);
        }
    }
}
