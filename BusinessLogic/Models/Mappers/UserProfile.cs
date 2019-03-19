using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DataBaseAccessLayer.Data.Entities.User, UserInfo>()
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.User, User>()
                .ReverseMap()
                .AfterMap((s, d) => d.Comments = null)
                .AfterMap((s, d) => d.Posts = null);
        }
    }
}
