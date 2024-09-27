using ContactsPortal.Data;
using ContactsPortal.Models;
using ContactsPortal.Repository;
using ContactsPortal.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
    options=>options.EnableRetryOnFailure()));
builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<ContactGroupRepository>();

//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Contact>, ContactValidator>();
builder.Services.AddScoped<IValidator<ContactGroup>, ContactGroupValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
