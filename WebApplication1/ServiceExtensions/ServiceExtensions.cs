using WebApplication1.Interfaces.StudentsInterfaces;

namespace WebApplication1.ServiceExtentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService , StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            return services;
        }
    }
}
