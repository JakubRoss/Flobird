using API.Swagger;
using Application.Extension;
using Infrastructure;
using Infrastructure.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Servicess to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure(builder.Environment,builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SwaggerConf
SwaggerControllerOrder<ControllerBase> swaggerControllerOrder = new(Assembly.GetEntryAssembly()!);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "1.0",
        Title = "Flobird API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        Contact = new OpenApiContact
        {
            Name = "Jakub Ross",
            Email = "jakub.rosploch@gmail.com",
            Url = new Uri("https://github.com/JakubRoss")
        }
    });

    c.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"]!)}");

    //komentarze przy akcjach
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;
    c.IncludeXmlComments(xmlPath);

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policy =>
    policy.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseCors("FrontEndClient");

app.UseSwagger();
app.UseSwaggerUI();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
