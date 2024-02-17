using Domain.Repositories;
using Infrastructure.Repositories.Impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IWebHostEnvironment app, ConfigurationManager configuration)
        {
            service.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(app.IsDevelopment()
                    ? configuration.GetConnectionString("LocalDbConnection")
                    : configuration.GetConnectionString("AzureDbConnection"));
            });

            service.AddScoped<DatabaseSeeder>();

            //base repo Services
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IBoardRepository, BoardRepository>();
            service.AddScoped<IBoardUsersRepository, BoardUsersRepository>();
            service.AddScoped<IListRepository, ListRepository>();
            service.AddScoped<ICardRepository, CardRepository>();
            service.AddScoped<ITasksRepository, TasksRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<IAttachmentRepository, AttachmentRepository>();
            service.AddScoped<IElementRepository, ElementRepository>();
            service.AddScoped<IElementUsersRepository, ElementUsersRepository>();
            service.AddScoped<ICardUserRepository, CardUserRepository>();
        }
    }
}
