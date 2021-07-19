using AutoMapper;
using Dto;
using RPGTest.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AutoMapper.Profiles
{
    public class UnitClassProfile : Profile
    {
        public UnitClassProfile()
        {
            CreateMap<UnitClass, UnitClassDto>();
            CreateMap<UnitClassDto, UnitClass>();
        }
    }
}
