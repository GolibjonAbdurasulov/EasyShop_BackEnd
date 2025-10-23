using System;
using System.Threading.Tasks;
using DatabaseBroker.Repositories.ClientRepository;
using DatabaseBroker.Repositories.UserRepository;
using Entity.Attributes;
using Entity.Exceptions;
using Entity.Models;
using Entity.Models.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Services.Dtos;
using Services.Dtos.Email;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class AuthService : IAuthService
{
    private readonly IMemoryCache _cache;

    private  IUserRepository UserRepository { get; set; }
    private IClientRepository  ClientRepository { get; set; }
    private readonly ITokenService _tokenService;
    private IEmailNotificationService _emailService;
    private IOtpService _otpService;
    
    public AuthService(IUserRepository repository, ITokenService tokenService, IClientRepository clientRepository, IEmailNotificationService emailSender, IOtpService otpService, IMemoryCache cache)
    {
        UserRepository = repository;
        _tokenService = tokenService;
        ClientRepository = clientRepository;
        _emailService = emailSender;
        _otpService = otpService;
        _cache = cache;
    }


    public async Task<UserDto> Login(UserLoginDto dto)
    {
        var user = await UserRepository.FirstOrDefaultAsync(user => user.PhoneNumber == dto.PhoneNumber && user.Password == dto.Password);
        if (user is  null)
            throw new NotFoundException("Foydalanuvchi topilmadi");
        string token =  _tokenService.GetToken();
        var resUser = new UserDto()
        {
            Id=user.Id,
            UserName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Role = user.Role.ToString(),
            IsSigned = true,
            Token = token
        };
        
        
        user.IsSigned = true;
        await UserRepository.UpdateAsync(user);
        return resUser;
    }
    
    
    public async Task<ClientDto> ClientLogin(ClientLoginDto dto)
    {
        var client = await ClientRepository.
            FirstOrDefaultAsync(user => user.Email == dto.PhoneNumber && user.Password == dto.Password);
        if (client is  null)
            throw new NotFoundException("Foydalanuvchi topilmadi");
        string token =  _tokenService.GetToken();
        var resUser = new ClientDto()
        {
            Id=client.Id,
            FullName = client.FullName,
            PhoneNumber = client.Email,
            Password = client.Password,
            IsSigned = true,
            Token = token
        };

        client.IsSigned = true;
        await ClientRepository.UpdateAsync(client);
        return resUser;
    }
   
    public async Task<bool> LogOut(long id)
    {
        var user =await UserRepository.GetByIdAsync(id);
        if (user is  null)
            throw new NullReferenceException();
        
        user.IsSigned = false;
        await UserRepository.UpdateAsync(user);
        return true;
    } public async Task<bool> ClientLogOut(long id)
    {
        var client =await ClientRepository.GetByIdAsync(id);
        if (client is  null)
            throw new NullReferenceException();
        
        client.IsSigned = false;
        await ClientRepository.UpdateAsync(client);
        return true;
    }
    
    public async Task<UserDto> GetUser(long id)
    {
        var user=await UserRepository.GetByIdAsync(id);
        return new UserDto
        {
            UserName=user.FullName
            ,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Role = user.Role.ToString()
        };
    }

    public async Task<ClientDto> Registration(RegisterDto dto)
    {
        // 1. Foydalanuvchini bazaga yozish (lekin hali tasdiqlanmagan)
        var client = new Client
        {
            FullName = dto.FullName,
            Email = dto.PhoneNumber,
            Password = dto.Password, 
            IsSigned = true
        };

        await ClientRepository.AddAsync(client);

        // // 2. Tasdiqlash kodi yaratish
        // var confirmationCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // 6 xonali kod
        //
        // _otpService.SaveOtp(client.Email, confirmationCode);
        //
        // // 3. Email yuborish
        // var emailDto = new SendEmailDto
        // {
        //     Email = dto.PhoneNumber,
        //     Subject = "Ro‘yxatdan o‘tishni tasdiqlash",
        //     Message = $"Assalomu alaykum {dto.FullName}!<br/>" +
        //               $"Ro‘yxatdan o‘tishni yakunlash uchun quyidagi kodni kiriting: <b>{confirmationCode}</b>"
        // };
        //
        // await _emailService.SendEmail(emailDto);
        //
        // // 4. ClientDto qaytarish
        return new ClientDto
        {
            Id = client.Id,
            PhoneNumber = client.Email,
            FullName = client.FullName,
            IsSigned = client.IsSigned,
        };
    }
    
    public async Task<bool> ConfirmEmail(string email, string code)
    {
        // 1. OTP ni MemoryCache dan tekshirish
        var isValid = _otpService.ValidateOtp(email, code);
        if (!isValid)
            return false;

        // 2. DB dagi foydalanuvchini tasdiqlash
        var client = await ClientRepository.GetAllAsQueryable().FirstOrDefaultAsync(c => c.Email == email);
        if (client == null) return false;

        client.IsSigned = true;
        ClientRepository.UpdateAsync(client);

        return true;
    }

    public async Task<bool> ResendOtp(ClientDto dto)
    {
        try
        {
            var confirmationCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // 6 xonali kod

            _otpService.SaveOtp(dto.PhoneNumber, confirmationCode);

            // 3. Email yuborish
            var emailDto = new SendEmailDto
            {
                Email = dto.PhoneNumber,
                Subject = "Ro‘yxatdan o‘tishni tasdiqlash",
                Message = $"Assalomu alaykum {dto.FullName}!<br/>" +
                          $"Ro‘yxatdan o‘tishni yakunlash uchun quyidagi kodni kiriting: <b>{confirmationCode}</b>"
            };
            
            await _emailService.SendEmail(emailDto);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Resend OTP error",e);
            throw;
        }
    }
}