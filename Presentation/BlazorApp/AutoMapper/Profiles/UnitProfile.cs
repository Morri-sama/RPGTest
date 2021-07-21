using AutoMapper;
using BlazorApp.FormEditors;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.AutoMapper.Profiles
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitFormContext, UnitDto>();
            CreateMap<UnitDto, UnitFormContext>();
        }
    }
}
