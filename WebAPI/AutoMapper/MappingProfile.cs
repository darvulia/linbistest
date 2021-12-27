using AutoMapper;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Developer, DeveloperViewModel>().ReverseMap();
        }
    }
}
