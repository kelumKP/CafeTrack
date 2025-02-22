using CafeTrack.Application.DTOs;
using CafeTrack.Application.Features.Cafes.Commands;
using CafeTrack.Application.Features.Cafes.Queries;
using CafeTrack.Application.Features.Employees.Commands;
using CafeTrack.Application.Features.Employees.Queries;
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
            // Register Infrastructure layer dependencies
            services.AddInfrastructure(configuration);

            // Register MediatR handlers (you can use one call if all handlers are in one assembly)
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateCafeCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<GetEmployeesByCafeQueryHandler>();
            });

            // Register FluentValidation validators
            services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CafeDtoValidator>();

            // Optionally, explicitly register command & query handlers
            services.AddScoped<IRequestHandler<CreateCafeCommand, CafeDto>, CreateCafeCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCafeCommand, bool>, DeleteCafeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCafeCommand, bool>, UpdateCafeCommandHandler>();
            services.AddScoped<IRequestHandler<GetCafesByLocationQuery, List<CafeDto>>, GetCafesByLocationQueryHandler>();
            services.AddScoped<IRequestHandler<GetCafeByIdQuery, CafeDto>, GetCafeByIdQueryHandler>();

            services.AddScoped<IRequestHandler<CreateEmployeeCommand, EmployeeDto>, CreateEmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmployeeCommand, EmployeeDto>, UpdateEmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<GetEmployeesByCafeQuery, List<EmployeeDto>>, GetEmployeesByCafeQueryHandler>();
            services.AddScoped<IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>, GetEmployeesQueryHandler>();

            return services;
        }
    }
}
