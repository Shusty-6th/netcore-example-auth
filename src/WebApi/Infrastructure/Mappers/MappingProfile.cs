using AutoMapper;
using NetCoreExampleAuth.Entities.Models;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreExampleAuth.Infrastructure.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationContract, User>();
        }
    }

}
