using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Domain.Context
{
    public static class MyBindings
    {
          public static IServiceCollection AddMyBindings(this IServiceCollection service)
        {
            service.AddScoped<IUser, UserService>();
            service.AddScoped<INote, NoteService>();
            return service;
        }
    }
}
