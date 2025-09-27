using System.Threading.Tasks;
using Services.Dtos;
using Services.Dtos.Email;

namespace Services.Interfaces;

public interface IAuthService
{
    public Task<UserDto> Login(UserLoginDto dto);
    public Task<ClientDto> ClientLogin(ClientLoginDto dto);
    public  Task<bool> LogOut(long id);
    public  Task<bool> ClientLogOut(long id);
    public Task<UserDto> GetUser(long id);
    public Task<bool> ConfirmEmail(string email, string code);
    public Task<ClientDto> Registration(RegisterDto dto);
    public Task<bool> ResendOtp(ClientDto dto);

}