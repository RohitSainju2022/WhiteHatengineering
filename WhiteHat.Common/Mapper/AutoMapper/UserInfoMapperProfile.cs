using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Models;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Common.Mapper.AutoMapper
{
    public class UserInfoMapperProfile : Profile
    {
        public UserInfoMapperProfile()
        {
            CreateMap<UserInfo, UserInfoModel>().ReverseMap();
        }
    }
}
