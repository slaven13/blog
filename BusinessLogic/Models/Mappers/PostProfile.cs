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
            CreateMap<DataBaseAccessLayer.Data.Entities.Post, Post>();
        }
    }
}
