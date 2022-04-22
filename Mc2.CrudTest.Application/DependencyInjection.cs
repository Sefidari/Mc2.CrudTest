using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;
using FluentValidation;
using Mc2.CrudTest.Application.Common.Behaviours;

namespace Mc2.CrudTest.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
