using AutoMapper;
using BlazorApp.FormEditors;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.AutoMapper.Profiles
{
    public class UnitClassProfile : Profile
    {
        public UnitClassProfile()
        {
            CreateMap<UnitClassFormContext, UnitClassDto>();
            CreateMap<UnitClassDto, UnitClassProfile>();
        }
    }
}
