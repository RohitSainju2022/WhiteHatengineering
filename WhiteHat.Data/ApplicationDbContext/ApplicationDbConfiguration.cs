using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Data.DefaultSeed;
using WhiteHat.Models;

namespace WhiteHat.Data.WhiteHatDbContext
{
    public class ApplicationDbConfiguration
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasData(UserSeed.DefaultUserInfoSeed());
        }
    }
}
