using FluentValidation;
using VatRate.Business.Interfaces;
using VatRate.Business.Implementations;
using MediatR;
using System.Reflection;
using VatRate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVatCalculatorService, VatCalculatorService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//opcao 1
//builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//builder.Services.AddMediatR(config =>
//{
//    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
//    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
//});

//opcao 2
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


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

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400; // Bad Request

        var errorResponse = new
        {
            Title = "Validation Error",
            Status = 400,
            Errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(e => e.ErrorMessage).ToList()
                )
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
    catch (Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500; // Internal Server Error

        var errorResponse = new
        {
            Title = "An unexpected error occurred.",
            Status = 500,
            Detail = ex.Message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
});

app.Run();
