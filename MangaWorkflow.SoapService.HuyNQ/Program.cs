using MangaWorkflow.Repositories.HuyNQ;
using MangaWorkflow.Services.HuyNQ;
using MangaWorkflow.SoapService.HuyNQ.Authentication;
using MangaWorkflow.SoapService.HuyNQ.SoapServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SoapCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSoapCore();
builder.Services.AddScoped<IChapterHuyNqService, ChapterHuyNqService>();
builder.Services.AddScoped<ChapterHuyNqRepository>();
builder.Services.AddScoped<IChapterHuyNqSoapService, ChapterHuyNqSoapService>();

builder.Services.AddScoped<ISystemUserAccountService, SystemUserAccountService>();
builder.Services.AddScoped<SystemUserAccountRepository>();
builder.Services.AddScoped<IUserAccountHuyNqSoapService, UserAccountHuyNqSoapService>();

// JWT authentication
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Chặn các SOAP endpoint được bảo vệ khi chưa có JWT hợp lệ (phải sau UseAuthentication).
app.UseMiddleware<SoapJwtAuthMiddleware>();

app.MapControllers();

app.UseSoapEndpoint<IChapterHuyNqSoapService>("/Chapter.asmx", new SoapEncoderOptions());
app.UseSoapEndpoint<IUserAccountHuyNqSoapService>("/User.asmx", new SoapEncoderOptions());

app.Run();
