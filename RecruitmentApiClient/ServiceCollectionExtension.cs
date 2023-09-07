using Microsoft.Extensions.DependencyInjection;

namespace RecruitmentApiClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {
            services.AddHttpClient<IRecruitmentApiClient, RecruitmentApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://recruitment.highfieldqualifications.com/");
            });

            return services;
        }
    }
}
