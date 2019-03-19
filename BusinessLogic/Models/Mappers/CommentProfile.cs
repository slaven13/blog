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
                .ForMember(d => d.PostInfo, s => s.MapFrom(src => src.Post))
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))
                .ForMember(d => d.ParentCommentId, s => s.MapFrom(src => src.ParentComment.Id))
                .AfterMap((s, d) => d.PostInfo.Id = s.PostId)
                .AfterMap((s, d) => d.UserInfo.Id = s.UserId)
                .ReverseMap()                
                .AfterMap((s, d) => d.CreationDate = DateTime.UtcNow)
                .AfterMap((s, d) => d.User = null)
                .AfterMap((s, d) => d.Post = null)
                .AfterMap((s, d) => d.Replies = null)
                .AfterMap((s, d) => d.ParentComment = null)
                .AfterMap((s, d) => d.UserId = s.UserInfo.Id)
                .AfterMap((s, d) => d.PostId = s.PostInfo.Id)                
                .AfterMap((s, d) => d.ParentCommentId = s.ParentCommentId != null ? s.ParentCommentId : (long?)null);

            CreateMap<DataBaseAccessLayer.Data.Entities.Comment, CommentPreview>()
                .ForMember(d => d.ContentPreview, s => s.MapFrom(src => src.Content))
                .ForMember(d => d.PostInfo, s => s.MapFrom(src => src.Post))
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<DataBaseAccessLayer.Data.Entities.Comment, CommentInfo>()
                .ForMember(d => d.ContentPreview, s => s.MapFrom(src => src.Content))
                .ForMember(d => d.UserInfo, s => s.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
