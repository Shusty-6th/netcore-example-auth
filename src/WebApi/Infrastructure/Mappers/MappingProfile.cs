using AutoMapper;
using NetCoreAxampleAuth.Entities.Models;
using NetCoreAxampleAuth.Patterns.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAxampleAuth.Infrastructure.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationContract, User>();
        }
    }

}
