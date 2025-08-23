using KayraExport.Application.Abstractions.Services;
using KayraExport.Application.Dtos.User;
using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Exceptions.User;

namespace KayraExport.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly IUserReadRepository _userReadRepository;
        readonly IUserWriteRepository _userWriteRepository;

        public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }


        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await _userReadRepository.AnyAsync(u => u.Email == createUserDto.Email))
                throw new UserAlreadyExistsException();


            await _userWriteRepository.AddAsync(new()
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                CreateDate = DateTime.UtcNow,
            });
            
            await _userWriteRepository.SaveChangesAsync();
        }



        public async Task<User> FindByEmailAsync(string email)
        {
            User? user = await _userReadRepository
                .GetSingleReadOnlyAsync(u => u.Email.Equals(email))
                ?? 
                throw new UserNotFoundException();

            return user;
        }
    }
}
