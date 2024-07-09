using Microsoft.EntityFrameworkCore;
using MovieLayered.DAL.EF;
using MovieLayered.DAL.Entities;
using MovieLayered.DAL.Interfaces;

namespace MovieLayered.DAL.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private MovieContext _movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }


        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        public async Task<Movie> Get(int id)
        {
            Movie? movie=await _movieContext.Movies.FindAsync(id);
            return movie;
        }

        public async Task<Movie> Get(string title)
        {
            var movies=await _movieContext.Movies.Where(m=>m.Title==title).ToListAsync();
            Movie? movie=movies?.FirstOrDefault();
            return movie;
        }

        public async Task Create(Movie movie)
        {
            await _movieContext.Movies.AddAsync(movie);
            await _movieContext.SaveChangesAsync();

            //await _movieContext.Movies.AddAsync(movie);
        }

        public async void Update(Movie movie)
        {
            _movieContext.Entry(movie).State= EntityState.Modified;
            await _movieContext.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            Movie? movie = await _movieContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie != null)
            {
                _movieContext.Movies.Remove(movie);
                await _movieContext.SaveChangesAsync();

                //_movieContext.Movies.Remove(movie);
            }

            //Movie? movie = await _movieContext.Movies.FirstAsync(id);
            //if (movie!=null)
            //{
            //    _movieContext.Movies.Remove(movie);
            //}
        }
    }
}
