using System.Reflection;
using DatabaseBroker.Repositories.Common;
using Entity.Attributes;

namespace API.Extension;

public static class ServiceCollectionExtensions
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        foreach (Type type in Assembly.GetAssembly(typeof(RepositoryBase<,>))!.GetTypes()
                     .Where(x => x.BaseType is not null && x.BaseType.IsGenericType &&
                                 x.BaseType.GetGenericTypeDefinition() == (typeof(RepositoryBase<,>)) &&
                                 x.GetCustomAttribute<InjectableAttribute>() is not null))
        {
            serviceCollection.AddScoped(type.GetInterfaces().LastOrDefault()!, type);
        }
    }

    public static void ConfigureServicesFromTypeAssembly<T>(this IServiceCollection serviceCollection)
    {
        foreach (var type in Assembly.GetAssembly(typeof(T))!.GetTypes()
                     .Where(x => x.GetCustomAttribute<InjectableAttribute>() != null))
        {
            var attr = type.GetCustomAttribute<InjectableAttribute>()!;

            if (type.GetInterfaces().Length != 0 && !attr.WithoutInterface)
                serviceCollection.Add(new ServiceDescriptor(type.GetInterfaces().LastOrDefault()!, type,
                    attr.IsSingleton ? ServiceLifetime.Singleton : ServiceLifetime.Scoped));
            else
                serviceCollection.Add(new ServiceDescriptor(type, type,
                    attr.IsSingleton ? ServiceLifetime.Singleton : ServiceLifetime.Scoped));

        }
    }
    
    
    
    // ConfigureJwtAuthentication metodining o'zgartirilgan varianti

//     public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
// {
//     services.AddAuthentication(options =>
//     {
//         options.DefaultScheme = "CustomBearer";
//         options.DefaultAuthenticateScheme = "CustomBearer";
//         options.DefaultChallengeScheme = "CustomBearer";
//     })
//     .AddJwtBearer("CustomBearer", options =>
//     {
//         var authSection = configuration.GetSection("Jwt");
//         string issuer = authSection["Issuer"];
//         string audience = authSection["Audience"];
//         string secretKey = authSection["SecretKey"];
//
//         if (string.IsNullOrEmpty(issuer))
//         {
//             throw new ArgumentNullException(nameof(issuer), "Issuer configuration is missing.");
//         }
//
//         if (string.IsNullOrEmpty(audience))
//         {
//             throw new ArgumentNullException(nameof(audience), "Audience configuration is missing.");
//         }
//
//         if (string.IsNullOrEmpty(secretKey))
//         {
//             throw new ArgumentNullException(nameof(secretKey), "SecretKey configuration is missing.");
//         }
//
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
//             ValidateLifetime = true,
//             ValidIssuer = issuer,
//             ValidAudience = audience
//         };
//     });
//
//     services.Configure<SwaggerGenOptions>(options =>
//     {
//         var securityScheme = new OpenApiSecurityScheme
//         {
//             Type = SecuritySchemeType.ApiKey,
//             Description = "JWT Bearer Authentication",
//             Name = "Authorization",
//             In = ParameterLocation.Header,
//             Scheme = "CustomBearer",
//             Reference = new OpenApiReference
//             {
//                 Type = ReferenceType.SecurityScheme,
//                 Id = "CustomBearer"
//             }
//         };
//
//         options.AddSecurityDefinition("CustomBearer", securityScheme);
//         options.AddSecurityRequirement(new OpenApiSecurityRequirement
//         {
//             { securityScheme, new List<string>() }
//         });
//     });
// }
//     
    

    //  public static void ConfigureJwtAuthentication(this IServiceCollection serviceCollection,
    //     IConfiguration configuration)
    // {
    //     serviceCollection
    //         .AddAuthentication(options =>
    //         {
    //             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //         })
    //         .AddJwtBearer(options =>
    //         {
    //            
    //             var authSection = AppData.AuthConfig;
    //             string? issuer = authSection.Issuer;
    //             string? audience = authSection.Audience;
    //             string secretKey = authSection.SecretKey;
    //             options.TokenValidationParameters = new TokenValidationParameters();
    //
    //             if (!issuer.IsNullOrEmpty())
    //             {
    //                 options.TokenValidationParameters.ValidateIssuer = true;
    //                 options.TokenValidationParameters.ValidIssuer = issuer;
    //             }
    //             else
    //             {
    //                 options.TokenValidationParameters.ValidateIssuer = false;
    //             }
    //
    //             if (!audience.IsNullOrEmpty())
    //             {
    //                 options.TokenValidationParameters.ValidateAudience = true;
    //                 options.TokenValidationParameters.ValidAudience = audience;
    //             }
    //             else
    //             {
    //                 options.TokenValidationParameters.ValidateAudience = false;
    //             }
    //
    //             options.TokenValidationParameters.ValidateIssuerSigningKey = true;
    //             options.TokenValidationParameters.IssuerSigningKey =
    //                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    //
    //             options.TokenValidationParameters.ValidateLifetime = true;
    //         });
    //
    //
    //     serviceCollection.Configure<SwaggerGenOptions>(options =>
    //     {
    //         var schema = new OpenApiSecurityScheme()
    //         {
    //             Type = SecuritySchemeType.ApiKey,
    //             Description = "JWT Bearer Security",
    //             Name = "Authorization",
    //             In = ParameterLocation.Header,
    //             Scheme = JwtBearerDefaults.AuthenticationScheme,
    //             Reference = new OpenApiReference()
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             }
    //         };
    //
    //         options.SwaggerGeneratorOptions
    //             .SecuritySchemes
    //             .Add(new KeyValuePair<string, OpenApiSecurityScheme>("Bearer", schema));
    //
    //         options.SwaggerGeneratorOptions.SecurityRequirements
    //             .Add(new OpenApiSecurityRequirement()
    //             {
    //                 { schema, new List<string>() }
    //             });
    //     });
    // }
}