using API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
   
    private IUserService UserService { get; set; }
    
    public UserController(IUserService userService)
    {
        UserService = userService;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( UserCreationDto dto)
    {
       var res= await UserService.CreateAsync(dto);
        return new ResponseModelBase(res);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( UserDto dto)
    {
        var user =await UserService.UpdateAsync(dto);
        return new ResponseModelBase(user);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res =await UserService.DeleteAsync(id);
        return new ResponseModelBase(res);
    }
   
    [HttpGet]
    public async Task<ResponseModelBase> GetAsync(long id)
    {
        var res =await UserService.GetByIdAsync(id);
        return new ResponseModelBase(res);
    }
    
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =await UserService.GetAllUsers();
        return new ResponseModelBase(res);
    }
}