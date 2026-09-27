using WebApplication1.Interfaces.StudentsInterfaces;

namespace WebApplication1.ServiceExtentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService , StudentService>();
            return services;
        }
    }
}
