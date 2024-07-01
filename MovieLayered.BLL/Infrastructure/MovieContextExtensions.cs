using Microsoft.EntityFrameworkCore;
using MovieLayered.DAL.EF;
using Microsoft.Extensions.DependencyInjection;

namespace MovieLayered.BLL.Infrastructure
{
    public static class MovieContextExtensions
    {
        public static void AddMovieContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MovieContext>(options => options.UseSqlServer(connection));
        }
    }
}
