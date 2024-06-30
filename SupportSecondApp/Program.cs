using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SupportSecondApp.Data;
using SupportSecondApp.Repositories;
using FluentValidation;
using SupportSecondApp.DTOs;
using SupportSecondApp.Validators;
using Microsoft.AspNetCore.Identity;
using SupportSecondApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using SupportSecondApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar los servicios de la aplicación
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISupportTaskRepository, SupportTaskRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IValidator<ProjectCreateDto>, ProjectCreateValidator>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allowlocalhost4200", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://127.0.0.1:5500")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Configuración de autenticación e identidad
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configuración de opciones de Identity si es necesario
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>() // Registrar SignInManager para ApplicationUser
    .AddRoleManager<RoleManager<IdentityRole>>() // Registrar RoleManager para IdentityRole
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la cookie
    options.SlidingExpiration = true; // Renovar el tiempo de expiración en cada solicitud
});

// Configuración de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));
});

// Configuración adicional de servicios
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Inicialización de datos si es necesario
    await SeedData.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allowlocalhost4200");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
