using MovieLayered.DAL.Interfaces;
using MovieLayered.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MovieLayered.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,EFUnitOfWork>();
        }
    }
}
