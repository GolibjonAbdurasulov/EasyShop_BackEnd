using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;

namespace Services.Services;
[Injectable]
public class TokenService:ITokenService
{
    private readonly IConfiguration _configurationManager;

    public TokenService(IConfiguration configurationManager)
    {
        _configurationManager = configurationManager;
    }
    public string GetToken()
    {
        
        string key = _configurationManager.GetSection("Jwt")["SecurityKey"];
        string issuer = _configurationManager.GetSection("Jwt")["Issuer"];
        string audience = _configurationManager.GetSection("Jwt")["Audience"];
        int expiresInMinutes = _configurationManager.GetSection("Jwt").GetValue<int>("ExpireAtInMinutes");
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Obid23"),
            new Claim("gender", "m")
        };

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(expiresInMinutes)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
