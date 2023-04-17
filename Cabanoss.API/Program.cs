using Cabanoss.Core.Authorization;
using Cabanoss.Core.Common;
using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.MIddleware;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Model.Validators;
using Cabanoss.Core.Repositories;
using Cabanoss.Core.Repositories.Impl;
using Cabanoss.Core.Service;
using Cabanoss.Core.Service.Impl;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Authetictaion
var AuthenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(AuthenticationSettings);
builder.Services.AddSingleton(AuthenticationSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";

    options.DefaultScheme = "Bearer";

    options.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = true;

    cfg.SaveToken = true;

    cfg.TokenValidationParameters = new TokenValidationParameters
    {

        ValidIssuer = AuthenticationSettings.JwtIssuer,

        ValidAudience = AuthenticationSettings.JwtIssuer,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationSettings.JwtKey))
    };
});
#endregion

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthorizationHandler,BelongToRequirementsHandler>();

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SwaggerConf
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CabanossAPI", Version = "v1" });

    // Dodanie opisu autoryzacji JWT
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Specify the authorization token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    // Dodanie wymagań autoryzacji JWT dla całego API
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion

builder.Services.AddDbContext<CabanossDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CabanossDbConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//base repo Services
builder.Services.AddScoped<IUserBaseRepository, UserBaseRepository>();
builder.Services.AddScoped<IWorkspaceBaserepository, WorkspaceBaserepository>();
builder.Services.AddScoped<IBoardBaseRepository, BoardBaseRepository>();
builder.Services.AddScoped<IBoardUsersBaseRepository, BoardUsersBaseRepository>();
//Bussiness Logic Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkspaceService,  WorkspaceService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
//Validation Services
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
