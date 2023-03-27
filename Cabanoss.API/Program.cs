using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.BussinessLogicService.Impl;
using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.MIddleware;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Model.Validators;
using Cabanoss.Core.Repositories;
using Cabanoss.Core.Repositories.Impl;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CabanossDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CabanossDbConnection"));
});

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddAutoMapper(typeof(CabanossMappingProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//base repo Services
builder.Services.AddScoped<IUserBaseRepository, UserBaseRepository>();
builder.Services.AddScoped<IWorkspaceBaserepository, WorkspaceBaserepository>();
//Bussiness Logic Services
builder.Services.AddScoped<IUserBussinessLogicService, UserBussinessLogicService>();
builder.Services.AddScoped<IWorkspaceBussinessLogicService,  WorkspaceBussinessLogicService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
