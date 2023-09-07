using Microsoft.Extensions.DependencyInjection;

namespace UserDataProcessor;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserDataProcessor(this IServiceCollection services)
    {
        services.AddScoped<IUserDataProcessor, UserDataProcessor>();

        return services;
        
    }
}
