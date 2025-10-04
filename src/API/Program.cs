using System.Net;
using System.Net.Mail;
using System.Text;
using API.Extension;
using API.Middlewares;
using DatabaseBroker;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContextPool<DataContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    optionsBuilder.UseLazyLoadingProxies();
    //optionsBuilder.UseChangeTrackingProxies();
});


builder.WebHost.ConfigureKestrel(x => x.ListenAnyIP(8080));

builder
    .Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        string key = builder.Configuration.GetSection("Jwt")["SecurityKey"];
        string issuer = builder.Configuration.GetSection("Jwt")["Issuer"];
        string audience = builder.Configuration.GetSection("Jwt")["Audience"];
        int expiresInMinutes = builder.Configuration.GetSection("Jwt").GetValue<int>("ExpireAtInMinutes");
        
        
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
        
    });


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "https://foragedialog.uz", Version = "v1" });

  
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy
            .AllowAnyOrigin()      
            .AllowAnyHeader()      
            .AllowAnyMethod();     
    });
});






builder.Services
    .AddAuthorization(options =>
    {
        // options.
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureRepositories();
builder.Services.AddMemoryCache();

// Smtp sozlamalarni o‘qish
var smtpConfig = builder.Configuration.GetSection("Smtp");

// SmtpClient ni DI ga qo‘shish
builder.Services.AddScoped<SmtpClient>(sp =>
{
    return new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]))
    {
        Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Password"]),
        EnableSsl = true
    };
});

builder.Services.ConfigureServicesFromTypeAssembly<OtpService>();
builder.Services.ConfigureServicesFromTypeAssembly<UserService>();
builder.Services.ConfigureServicesFromTypeAssembly<AuthService>();
builder.Services.ConfigureServicesFromTypeAssembly<FileService>();
builder.Services.ConfigureServicesFromTypeAssembly<TranslationService>();
builder.Services.ConfigureServicesFromTypeAssembly<TokenService>();
builder.Services.ConfigureServicesFromTypeAssembly<EmailNotificationService>();

// Global xato boshqaruv middleware ni qo'shish
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

// Xato boshqaruv middleware ni qo'shish
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// HTTP so'rovlarini konfiguratsiya qilish
app.UseHttpsRedirection();
app.UseRouting();


app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}




app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

// Dasturni ishga tushirish
app.Run();
