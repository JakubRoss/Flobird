using Application.Model.Validators;
using Application.Service;
using Application.Service.Impl;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Application.Common;
using Application.Authorization;
using Application.Middleware;
using Domain.Extension;
using Microsoft.AspNetCore.Authorization;

namespace Application.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDomain(configuration);

            #region FluentValidations
            service.AddFluentValidationAutoValidation();
            service.AddFluentValidationClientsideAdapters();
            #endregion
            #region Azure
            var azureProps = new AzureProps();
            configuration.GetSection("Azure").Bind(azureProps);
            service.AddSingleton(azureProps);
            #endregion

            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            service.AddScoped<ErrorHandlingMiddleware>();
            service.AddScoped<IAuthorizationHandler, ResourceOperationRequirementsHandler>();
            service.AddScoped<IHttpUserContextService, HttpUserContextService>();
            //Business Logic Services
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IBoardService, BoardService>();
            service.AddScoped<IListService, ListService>();
            service.AddScoped<ICardService, CardService>();
            service.AddScoped<ITasksService, TasksService>();
            service.AddScoped<ICommentServices, CommentServices>();
            service.AddScoped<IAttachmentService, AttachmentService>();
            service.AddScoped<IElementService, ElementService>();
            service.AddScoped<IFileService, FileService>();

            //Validation Services
            service.AddValidatorsFromAssemblyContaining<AttachmentDtoValidator>()   //nie potrzeba za kazdym razem rejestrowac validatorow - wystarczy w parametrze generycznym dodac tylko jeden dowonlny validator, reszta zostanie dodana automatycznie
                .AddFluentValidationAutoValidation()                //domyslna walidacja z frameworka ASP.NET zostanie zastapiona walidacja z fluentValidation
                .AddFluentValidationClientsideAdapters();           //dodanie regul walidacji po stronie frontendu - client side
        }
    }
}
