using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services.Interaces;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OfficeControlSystemApi.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Configuration;
using OfficeControlSystemApi.Middlewares;
using Microsoft.Extensions.Logging;
using OfficeControlSystemApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthOptions>(
    builder.Configuration.GetSection(nameof(AuthOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorPolicy",
         policy => policy.RequireRole("Administrator"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", 
        policy => policy.RequireRole("User"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorOrUserPolicy", 
        policy => policy.RequireRole("Administrator", "User"));
});

builder.Services.AddScoped<IVisitHistoryService, VisitHistoryService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccessCardRepository, AccessCardRepository>();
builder.Services.AddScoped<IVisitHistoryRepository, VisitHistoryRepository>();

builder.Services.AddScoped<ICreateEmployeeCommand, CreateEmployeeCommand>();
builder.Services.AddScoped<ICreateVisitHistoryCommand, CreateVisitHistoryCommand>();
builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();

builder.Services.AddLogging();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfficeControlSystem"));
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();
