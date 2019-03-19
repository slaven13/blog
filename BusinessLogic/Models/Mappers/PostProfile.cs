using AutoMapper;
using System;

namespace BusinessLogic.Models.Mappers
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<DataBaseAccessLayer.Data.Entities.Post, Post>()
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))
                .AfterMap((s, d) => d.UserInfo.Id = s.UserId)
                .ReverseMap()
                .AfterMap((s, d) => d.CreationDate = DateTime.UtcNow)
                .AfterMap((s, d) => d.User = null)
                .AfterMap((s, d) => d.UserId = s.UserInfo.Id);

            CreateMap<DataBaseAccessLayer.Data.Entities.Post, PostPreview>()
                .ForMember(d => d.CommentsPreview, s => s.MapFrom(src => src.Comments))
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))
                .ForMember(d => d.ContentPreview, s => s.MapFrom(src => src.Content))
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.Post, PostInfo>()
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))                
                .ReverseMap();
        }
    }
}
