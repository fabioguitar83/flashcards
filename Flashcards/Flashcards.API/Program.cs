using Flashcards.Application.Handlers;
using Flashcards.Domain.Commands;
using Flashcards.Infrastructure.IOC;
using Flashcards.Infrastructure.Mapper;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MAPPER
builder.Services.AddAutoMapper(typeof(MapperProfile));

//MEDIATOR
builder.Services.AddMediatR(typeof(UserAddCommandHandler));

builder.Services.AddDependenciesProject(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
