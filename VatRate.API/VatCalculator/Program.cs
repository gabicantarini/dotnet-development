using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VatCalculator.Business.Implementations;
using VatCalculator.Business.Interfaces;
using VatCalculator.Business.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<VatCalculatorUseCaseHandler>());
builder.Services.AddTransient<IVatCalculatorService, VatCalculatorService>();
builder.Services.AddValidatorsFromAssemblyContaining<VatCalculatorUseCasetValidator>();

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
