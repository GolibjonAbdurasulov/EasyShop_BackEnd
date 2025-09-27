using System;
using Entity.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Services.Interfaces;

namespace Services.Services;

[Injectable]
public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;

    public OtpService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void SaveOtp(string email, string code)
    {
        _cache.Set(email, code, TimeSpan.FromMinutes(2)); // 2 daqiqa amal qiladi
    }

    public bool ValidateOtp(string email, string code)
    {
        if (_cache.TryGetValue(email, out string savedCode))
        {
            return savedCode == code;
        }
        return false;
    }
}
