using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseBroker.Repositories.ClientRepository;
using DatabaseBroker.Repositories.UserRepository;
using Entity.Attributes;
using Entity.Enums;
using Entity.Models.Users;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    
    
    public async Task<UserDto> GetByIdAsync(long id)
    {
        var oldUser = await _userRepository.GetByIdAsync(id);
        var dto = new UserDto
        {
            Id = oldUser.Id,
            UserName=oldUser.FullName,
            PhoneNumber = oldUser.PhoneNumber,
            Password = oldUser.Password,
            Role = oldUser.Role,
            IsSigned = oldUser.IsSigned
            
            
        };
        return dto;
    }
    public async Task<long> DeleteAsync(long id)
    {
        var oldUser = await _userRepository.GetByIdAsync(id); 
        await _userRepository.RemoveAsync(oldUser);
        return id;
    }
   

    public async Task<UserDto> UpdateAsync(UserDto dto)
    {
        var userFromDb = await _userRepository.GetByIdAsync(dto.Id);
        if (userFromDb == null)
            throw new Exception("Client not found");
        userFromDb.FullName = dto.UserName;
        userFromDb.PhoneNumber = dto.PhoneNumber;
        userFromDb.IsSigned = dto.IsSigned;
        userFromDb.Role = dto.Role;


        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            userFromDb.Password = dto.Password; 
        }


        var updatedClient = await _userRepository.UpdateAsync(userFromDb);

        return new UserDto()
        {
            UserName = updatedClient.FullName,
            PhoneNumber = updatedClient.PhoneNumber,
            Role = updatedClient.Role,
            IsSigned = updatedClient.IsSigned,
            Password = updatedClient.Password,
        };
    }
    
    
    public async Task<UserDto> CreateAsync(UserCreationDto dto)
    {
        var user = new User
        {
            FullName= dto.UserName,
            PhoneNumber = dto.Email,
            Password = dto.Password,
            Role = Enum.Parse<Role>(dto.Role),
            IsSigned = false
        };
        var resDto = new UserDto
        {
            Id = user.Id,
            UserName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Role = user.Role,
            IsSigned = user.IsSigned
        };
        await _userRepository.AddAsync(user);
        return resDto;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        List<UserDto> userDtos = new List<UserDto>();
        List<User> users = _userRepository.GetAllAsQueryable().ToList();
        foreach (User user in users)
        {
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                UserName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                Role = user.Role,
                IsSigned = user.IsSigned,
                Token = "null"
            });
        }

        return userDtos;
    }
}