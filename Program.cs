using System.Reflection;
using _netcore_2.Application.Interface;
using _netcore_2.Application.Service;
using _netcore_2.Application.Validators;
using _netcore_2.Infrastructure.Persistence;
using _netcore_2.Infrastructure.Persistence.Repositories;
using _netcore_2.MIddlewares;
using _netcore_2.Presentation.Endpoints;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("PersonDb");

builder.Services.AddSqlite<PersonContext>(connString);

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdatePersonDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MigrateDb();

app.MapPersonEndpoints();
app.Run();
