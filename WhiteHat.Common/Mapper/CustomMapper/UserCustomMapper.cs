using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Common.Mapper.CustomMapper
{
    public static  class UserCustomMapper
    {
        public static UserInfo ToEntity( UserInfo model, UserInfoModel self)
        {
            model.UserId = new Guid();
            model.GenderId = self.GenderId;
            model.DOB = self.DOB;
            model.FirstName = self.FirstName;
            model.MiddleName = self.MiddleName;
            model.LastName = self.LastName;
            model.Mobile = self.Mobile;
            return model;
        }

        public static UserInfoModel ToModel(UserInfo self)
        {
            UserInfoModel model = new UserInfoModel();
            model.UserId = self.UserId;
            model.GenderId = self.GenderId;
            model.DOB = self.DOB;
            model.Mobile = self.Mobile;
            model.FirstName = self.FirstName;
            model.MiddleName = self.MiddleName;
            model.LastName = self.LastName;
            return model;
        }
    }
}
