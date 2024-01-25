using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;

namespace WhiteHat.Data.DefaultSeed
{
    public class UserSeed
    {
        public static List<UserInfo> DefaultUserInfoSeed()
        {
            List<UserInfo> userInfoes = new List<UserInfo>  {
            new UserInfo()
            {
                    UserId = new Guid("9668DC45-23AF-4CAE-A85B-3711BB1B4247"),
                    FirstName = "Test",
                    LastName = "User",
                    GenderId = 1,
                    Mobile = "+9808311151",
            }};
            return userInfoes;
        }
    }
}
