using AutoMapper;
using Dto;
using RPGTest.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AutoMapper.Profiles
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDto, Unit>();
        }
    }
}
