using MovieLayered.BLL.DTO;

namespace MovieLayered.BLL.Interfaces
{
    public interface IMovieService
    {
        Task CreateMovie(MovieDTO movieDto);
        Task UpdateMovie(MovieDTO movieDto);
        Task DeleteMovie(int id);
        Task<MovieDTO> GetMovie(int id);
        Task<IEnumerable<MovieDTO>> GetMovies();
    }
}
