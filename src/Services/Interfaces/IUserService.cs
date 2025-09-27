using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Dtos;

namespace Services.Interfaces;

public interface IUserService
{
    public  Task<UserDto> UpdateAsync(UserDto dto);
    public  Task<long> DeleteAsync(long id);
    public Task<UserDto> GetByIdAsync(long id);
    public  Task<UserDto> CreateAsync(UserCreationDto dto);
    public  Task<List<UserDto>> GetAllUsers();
}