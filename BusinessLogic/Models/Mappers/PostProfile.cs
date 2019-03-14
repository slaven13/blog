using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models.Mappers
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<DataBaseAccessLayer.Data.Entities.Post, Post>()
                .BeforeMap((x, s) => s.CreationDate = DateTime.UtcNow)
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ReverseMap();


            CreateMap<DataBaseAccessLayer.Data.Entities.Post, PostPreview>()
                .ForMember(x => x.CommentsPreview, dst => dst.MapFrom(src => src.Comments))
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ForMember(x => x.ContentPreview, dst => dst.MapFrom(src => src.Content))
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.Post, PostInfo>()
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
