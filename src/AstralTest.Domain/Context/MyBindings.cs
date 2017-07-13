using AstralTest.Domain.Model.RealizeInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Domain.Context
{
    public static class MyBindings
    {
          public static IServiceCollection AddMyBindings(this IServiceCollection service)
        {
            service.AddScoped<IUser, UserControl>();
            return service;
        }
    }
}
