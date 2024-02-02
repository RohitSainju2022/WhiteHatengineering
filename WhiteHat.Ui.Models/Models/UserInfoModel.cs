using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHat.Ui.Models.Models
{
    public class UserInfoModel
    {
        public Guid UserId { get; set; }

        //public string Username { get; set; } = default!;

        //public string Email { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = default!;

        public int? GenderId { get; set; }

        public DateTime? DOB { get; set; }

        public string? Image { get; set; }

        public string? Mobile { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

    }
}
