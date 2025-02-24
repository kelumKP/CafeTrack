using CafeTrack.Application.DTOs;
using CafeTrack.Application.Features.Cafes.Commands;
using CafeTrack.Application.Features.Cafes.Queries;
using CafeTrack.Application.Features.Employees.Commands;
using CafeTrack.Application.Features.Employees.Queries;
using CafeTrack.Application.Mappings;
using CafeTrack.Application.Validators;
using CafeTrack.Infrastructure.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeTrack.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);

            // Register Infrastructure layer dependencies
            services.AddInfrastructure(configuration);

            // Register MediatR (Autofac will handle the rest)
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateCafeCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GetEmployeesByCafeQueryHandler>();
            });

            // Register FluentValidation validators
            services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CafeDtoValidator>();

            return services;
        }
    }
}
