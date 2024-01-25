using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHat.Models
{
    public  class UserInfo
    {
        public Guid UserId { get; set; }

        public int? GenderId { get; set; }

        public DateTime? DOB { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }
    }
}
