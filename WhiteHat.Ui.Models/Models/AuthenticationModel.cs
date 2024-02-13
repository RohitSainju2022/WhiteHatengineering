using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteHat.Ui.Models.Models
{
    public class AuthenticationModel
    {
        public bool IsActivate { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpireDate { get; set; }

        public UserInfoModel User { get; set; }

    }
}
