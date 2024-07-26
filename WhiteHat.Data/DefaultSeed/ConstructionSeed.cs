using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;

namespace WhiteHat.Data.DefaultSeed
{
    public class ConstructionSeed
    {
        public static List<Construction> DefaultConstructionSeed()
        {
            List<Construction> constructions = new List<Construction>  {
            new Construction()
            {
                    ConstructionId = 1,
                    Title = "Upvc",
                    Image = "",
                    Description = "I am Upvc Description check",
                    ReadMore = "I am Upvc Readmore Check",
                    IsDeleted = false
            },
            new Construction()
            {
                    ConstructionId = 2,
                    Title = "Upvc2",
                    Image = "",
                    Description = "I am Upvc Description check 2",
                    ReadMore = "I am Upvc Readmore Check 2",
                    IsDeleted = false
            } };
            return constructions;
        }
    }
}
