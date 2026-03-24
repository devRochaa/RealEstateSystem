using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.Services;
using HomeExpenses.Infrasctructure.Persistence;
using HomeExpenses.Infrasctructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            ValidationException validationException => (StatusCodes.Status400BadRequest, validationException.Message),
            KeyNotFoundException keyNotFoundException => (StatusCodes.Status404NotFound, keyNotFoundException.Message),
            InvalidOperationException invalidOperationException => (StatusCodes.Status400BadRequest, invalidOperationException.Message),
            _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro interno ao processar a requisiÃ§Ã£o.")
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new { message });
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
