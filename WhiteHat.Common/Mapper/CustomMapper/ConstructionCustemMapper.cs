using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Common.Mapper.CustomMapper
{
    public static class ConstructionCustemMapper
    {
        public static Construction ToEntity(Construction model, ConstructionModel self)
        {
            model.Title = self.Title;
            model.Image = self.Image;
            model.Description = self.Description;
            model.ReadMore = self.ReadMore;
            return model;
        }
    }
}
