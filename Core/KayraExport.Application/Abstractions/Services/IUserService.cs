using KayraExport.Application.Dtos.User;
using KayraExport.Application.Features.Commands.Register;
using KayraExport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserDto createUserDto);

        Task<User> FindByEmailAsync(string email);
    }
}
