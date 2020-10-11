using AutoMapper;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreExampleAuth.Domain.Core.Model;

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
