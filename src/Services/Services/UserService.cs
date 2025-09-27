using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Email = oldUser.Email,
            Password = oldUser.Password,
            Role = oldUser.Role.ToString(),
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
        var oldUser = await _userRepository.GetByIdAsync(dto.Id);
        if (dto.Email is not null&&dto.Email!="string")
            oldUser.Email = dto.Email;
        
        if (dto.Password is not null&&dto.Password!="string")
            oldUser.Password = dto.Password; 
        
        if (dto.UserName is not null&&dto.UserName!="string")
            oldUser.FullName = dto.UserName; 
     
        //oldUser.UpdatedAt=DateTime.Now;
        await _userRepository.UpdateAsync(oldUser);
        return dto;
    }
    
    
    public async Task<UserDto> CreateAsync(UserCreationDto dto)
    {
        var user = new User
        {
            FullName= dto.UserName,
            Email = dto.Email,
            Password = dto.Password,
            Role = Enum.Parse<Role>(dto.Role),
            IsSigned = false
        };
        var resDto = new UserDto
        {
            Id = user.Id,
            UserName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role.ToString(),
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
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString(),
                IsSigned = user.IsSigned,
                Token = "null"
            });
        }

        return userDtos;
    }
}