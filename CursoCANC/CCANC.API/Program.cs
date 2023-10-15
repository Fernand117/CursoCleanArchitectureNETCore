using Application;
using CCANC.API;
using CCANC.API.Extensions;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// SE SUPLANTAN LOS SERVICES CONFIGURADOS EN LA DEPENDENCYINJECTION
// Add services to the container.

builder.Services.AddPresentation()
       .AddInfrastructure(builder.Configuration)
       .AddAplication();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();