using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Data;
using SupportSecondApp.Repositories;
using FluentValidation.Validators;
using Microsoft.IdentityModel.Tokens;
using SupportSecondApp.DTOs;
using SupportSecondApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(x => 
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISupportTaskRepository, SupportTaskRepository>();
builder.Services.AddScoped<IValidator<ProjectCreateDto>, ProjectCreateValidator>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowlocalhost4200", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allowlocalhost4200");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();