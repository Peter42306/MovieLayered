using MovieLayered.DAL.EF;
using MovieLayered.DAL.Entities;
using MovieLayered.DAL.Interfaces;

namespace MovieLayered.DAL.Repositories
{

    public class EFUnitOfWork : IUnitOfWork
    {
        private MovieContext _movieContext;
        private MovieRepository _movieRepository;

        public EFUnitOfWork(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public IRepository<Movie> Movies
        {
            get
            {
                if (_movieRepository == null)
                {
                    _movieRepository = new MovieRepository(_movieContext);
                }
                return _movieRepository;
            }
        }

        public async Task Save()
        {
            await _movieContext.SaveChangesAsync();
        }
    }
}
