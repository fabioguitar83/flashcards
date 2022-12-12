using Flashcards.API.Middlewares;
using Flashcards.API.SnakeCase;
using Flashcards.Application.Handlers;
using Flashcards.Domain.Configuration;
using Flashcards.Infrastructure.IOC;
using Flashcards.Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(
    options =>
    {
        //SNAKE CASE IN QUERY PARAMETERS, para não precisar ficar colocando [FromQuery(Name="teste_teste")]
        options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
    }
    ).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
        options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
        options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
    });
   

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//SWAGGER
builder.Services.AddSwaggerGen(c =>
{
    //SNAKE CASE IN QUERY PARAMETERS
    c.OperationFilter<SnakecasingParameOperationFilter>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flashcards API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @$"JWT Authorization header using the Bearer scheme.<br />
                         Enter 'Bearer'[space] and then your token in the text input below.<br />
                         Example: \'Bearer 12345abcdef\'",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

//FOR USE SNAKECASE
builder.Services.AddSwaggerGenNewtonsoftSupport();

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
    }
);

RegisterMappings.Register();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
