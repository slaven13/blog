using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models.Mappers
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<DataBaseAccessLayer.Data.Entities.Comment, Comment>()
                .BeforeMap((x, s) => s.CreationDate = DateTime.UtcNow)
                .ForMember(x => x.PostInfo, dst => dst.MapFrom(src => src.Post))
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ForMember(x => x.ParentCommentInfo, dst => dst.MapFrom(src => src.ParentComment))
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.Comment, CommentPreview>()
                .ForMember(x => x.ContentPreview, dst => dst.MapFrom(src => src.Content))
                .ForMember(x => x.PostInfo, dst => dst.MapFrom(src => src.Post))
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.Comment, CommentInfo>()
                .ForMember(x => x.ContentPreview, dst => dst.MapFrom(src => src.Content))
                .ForMember(x => x.UserInfo, dst => dst.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
