using AstralTest.Domain.Interface;
using AstralTest.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Domain.ContextDb
{
    public static class MyBinMyServicesBindingdings
    {
          public static IServiceCollection AddMyBindings(this IServiceCollection service)
        {
            service.AddScoped<IUser, UserService>();
            service.AddScoped<INote, NoteService>();
            return service;
        }
    }
}
