using FluentValidation;
using VatRate.Business.Interfaces;
using VatRate.Business.Implementations;
using VatRate.Business.UseCases.VatCalculationUseCase;
using VatRate.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<VatCalculationUseCaseHandler>());
builder.Services.AddScoped<IVatCalculatorService, VatCalculatorService>();
//builder.Services.AddScoped<IValidator<VatCalculationRequestDto>, VatCalculationUseCasetValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VatCalculationUseCasetValidator>();

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
