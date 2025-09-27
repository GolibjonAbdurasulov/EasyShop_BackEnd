using API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ResponseModelBase> Login([FromBody]UserLoginDto dto)
    {
        var res = await _authService.Login(dto);
        return new ResponseModelBase(res);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ResponseModelBase> LogOut(long id)
    {
        var res =await _authService.LogOut(id);
        
        return new ResponseModelBase(res);
    }
}
