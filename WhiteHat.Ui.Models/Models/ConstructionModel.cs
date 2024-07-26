using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHat.Ui.Models.Models
{
    public class ConstructionModel
    {
        public int ConstructionId { get; set; }

        public string Title { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? ReadMore { get; set; }

        public bool IsDeleted { get; set; }
    }
}
