using Flashcards.API.Middlewares;
using Flashcards.Application.Handlers;
using Flashcards.Domain.Commands;
using Flashcards.Infrastructure.Configuration;
using Flashcards.Infrastructure.IOC;
using Flashcards.Infrastructure.Mapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//APPSETTING
builder.Services.Configure<AppSettings>(builder.Configuration);

//MAPPER
builder.Services.AddAutoMapper(typeof(MapperProfile));

//MEDIATOR
builder.Services.AddMediatR(typeof(UserAddCommandHandler));

//DEPENDENCIES
builder.Services.AddDependenciesProject(builder.Configuration);

//JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Secret").Value);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
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

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
