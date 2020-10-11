using NetCoreExampleAuth.Domain.Core.Model;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationContract userForAuth);
        Task<string> CreateToken(User user);
    }
}
