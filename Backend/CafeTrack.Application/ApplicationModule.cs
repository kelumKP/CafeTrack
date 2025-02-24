using Autofac;
using AutoMapper;
using CafeTrack.Application.Features.Cafes.Commands;
using CafeTrack.Application.Features.Employees.Queries;
using CafeTrack.Application.Mappings;
using CafeTrack.Application.Validators;
using CafeTrack.Infrastructure.Data;
using MediatR;
using System.Reflection;

namespace CafeTrack.Application
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register MediatR
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all command and query handlers
            builder.RegisterAssemblyTypes(typeof(CreateCafeCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(GetEmployeesByCafeQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register validators
            builder.RegisterAssemblyTypes(typeof(EmployeeDtoValidator).Assembly)
                .Where(t => t.Name.EndsWith("Validator")) // Register all validators
                .AsImplementedInterfaces();

            // Register DbContext
            builder.RegisterType<AppDbContext>()
                .InstancePerLifetimeScope();

            // Register AutoMapper
            builder.RegisterAssemblyTypes(typeof(EmployeeProfile).Assembly)
                .As<Profile>(); // Register all AutoMapper profiles

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
